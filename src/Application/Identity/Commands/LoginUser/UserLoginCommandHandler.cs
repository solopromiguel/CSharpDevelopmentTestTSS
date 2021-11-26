using Application.Identity.Responses;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using MediatR;
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

namespace Application.Identity.Commands.UserLogin
{
    public class UserLoginCommandHandler :
          IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        private readonly IIdentityRepository _identityRepository;

        public UserLoginCommandHandler(
            IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<IdentityAccess> Handle(UserLoginCommand notification, CancellationToken cancellationToken)
        {
           var result = await  _identityRepository.signIn(notification);          
            return result;
        }

    }
}
