using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace GithubUsers.Tests.Load.Controllers
{
    public class ControllerTestsBase
    {
        protected TestServer Server;
        protected HttpClient Client;

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