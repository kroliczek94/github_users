using GithubUsers.App.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GithubUsers.App.Services.Interfaces
{
    public interface IGithubApiIntegrationService
    {
        Task<(HttpStatusCode Status, StoredGithubUserInfo Info)> GetInfo(string owner, Dictionary<string, string> requestHeaders);
    }
}
