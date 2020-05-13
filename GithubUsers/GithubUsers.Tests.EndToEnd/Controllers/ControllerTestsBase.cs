using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using GithubUsers.Api.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GithubUsers.Tests.EndToEnd.Controllers
{
    public class ControllerTestsBase
    {
        protected TestServer Server;
        protected HttpClient Client;

        protected const string ProperUsername = "allegro-test-user-1";
        protected const string UserWithoutRepositories = "allegro-empty-user";
        protected const string NonExistingUser = "non-existing-account";

        public ControllerTestsBase()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json").Build())
                .UseStartup<Startup>());
            Client = Server.CreateClient();
        }
    }
}