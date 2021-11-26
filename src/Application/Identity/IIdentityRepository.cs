using Application.Common.Interfaces;
using Application.Identity.Commands.CreateUser;
using Application.Identity.Commands.UserLogin;
using Application.Identity.Responses;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity
{
    public interface IIdentityRepository
    {
        Task<IdentityResult> CreateUser(UserCreateCommand notification);
        Task<IdentityAccess> signIn(UserLoginCommand notification);

    }
    public  class IdentityRepository: IIdentityRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public IdentityRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> CreateUser(UserCreateCommand notification)
        {
            var entry = new ApplicationUser
            {
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                Email = notification.UserName,
                UserName = notification.UserName
            };

            return await _userManager.CreateAsync(entry, notification.Password);
        }

        public async Task<IdentityAccess> signIn(UserLoginCommand notification)
        {
            var result = new IdentityAccess();

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == notification.UserName);
            if(user != null)
            {
                var response = await _signInManager.CheckPasswordSignInAsync(user, notification.Password, false);

                if (response.Succeeded)
                {

                    result.Succeeded = true;
                    await GenerateToken(user, result);
                }
            }           

            return result;
        }
        public async Task GenerateToken(ApplicationUser user, IdentityAccess identity)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var roles = await _context.Roles
                                      .Where(x => x.UserRoles.Any(y => y.UserId == user.Id))
                                      .ToListAsync();

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role.Name)
                );
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_configuration.GetValue<double>("TokenExpiration")),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            identity.AccessToken = tokenHandler.WriteToken(createdToken);
        }
    }
}
