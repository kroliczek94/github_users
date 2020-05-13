using GithubUsers.App.Models;
using System.Net;
using System.Threading.Tasks;

namespace GithubUsers.App.Services.Interfaces
{
    public interface IUserService
    {
        Task<(HttpStatusCode Status, StoredGithubUserInfo Info)> GetAsync(string owner);
    }
}
