using System;
using System.IO;
using System.Net.Http;
using NBomber.Contracts;
using NBomber.CSharp;
using Xunit;

namespace GithubUsers.Tests.Load.Controllers
{
    public class RepositoriesControllerTests : ControllerTestsBase
    {
        private readonly string[] githubUserNames;
        public RepositoriesControllerTests()
        {
            githubUserNames = File.ReadAllLines("Resources/names.txt");
        }

        [Fact]
        public void Get_NotValidEmail_ReturnsNotFound()
        {
            var rand = new Random();

            var pullStep = Step.Create("fetch from API step", execute: async (context) =>
            {
                var index = rand.Next(0, githubUserNames.Length - 1);

                using (var client = new HttpClient())
                {
                    var response = await Client.GetAsync($"/repositories/{githubUserNames[index]}");
                }

                return Response.Ok();
            });

            var scenario = ScenarioBuilder
                .CreateScenario("Github user fetch scenario", new[] { pullStep })
                .WithConcurrentCopies(100)
                .WithDuration(TimeSpan.FromSeconds(60))
                .WithAssertions(Assertion.ForStep("fetch from API step", stat => stat.RPS > 20));

            NBomberRunner.RegisterScenarios(new[] { scenario })
                .RunTest();
        }
    }
}