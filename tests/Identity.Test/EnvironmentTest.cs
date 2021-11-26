using Application.Identity.Commands.UserLogin;
using Application.Identity.Responses;
using Core.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Test
{
    [TestClass]
    public class EnvironmentTest
    {
        public readonly TestServer _server;
        public readonly HttpClient _client;

        public EnvironmentTest()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseSerilog()
                .UseStartup<Startup>());
            _client = _server.CreateClient();

        }


        [TestInitialize]
        public async Task SetUp()
        {
            var user = new UserLoginCommand();
            user.UserName = "admin";
            user.Password = "123456";

            var body = JsonConvert.SerializeObject(user);

            var response = await _client.PostAsync("api/identity/authentication",
                new StringContent(body, Encoding.UTF8, "application/json"));

            var content = await response.Content.ReadAsStringAsync();

            var tokenAccess = JsonConvert.DeserializeObject<IdentityAccess>(content.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAccess.AccessToken);

        }



    }
}
