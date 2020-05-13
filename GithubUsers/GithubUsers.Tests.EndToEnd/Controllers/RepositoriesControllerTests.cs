using System.Net;
using System.Threading.Tasks;
using GithubUsers.Api.Responses;
using Newtonsoft.Json;
using Xunit;

namespace GithubUsers.Tests.EndToEnd.Controllers
{
    public class RepositoriesControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task Get_NotExistingUser_ReturnsNotFound()
        {
            var response = await Client.GetAsync($"/repositories/{NonExistingUser}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_ValidAccountWithoutRepo_ReturnsEmptyRepoOwnerData()
        {
            var response = await Client.GetAsync($"repositories/{UserWithoutRepositories}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserRepositoryInfoResponse>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(0, user.AvgForks);
            Assert.Equal(0, user.AvgStargazers);
            Assert.Equal(0, user.AvgSize);
            Assert.Empty(user.Letters);
            Assert.Equal(UserWithoutRepositories, user.Owner);
        }

        [Fact]
        public async Task Get_ValidAccount_ReturnsRepoOwnerData()
        {
            var response = await Client.GetAsync($"repositories/{ProperUsername}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserRepositoryInfoResponse>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(0, user.AvgForks);
            Assert.Equal(0, user.AvgStargazers);
            Assert.Equal(0, user.AvgSize);
            Assert.Equal(2, user.Letters["a"]);
            Assert.Equal(2, user.Letters["z"]);
            Assert.Equal(ProperUsername, user.Owner);
        }
    }
}