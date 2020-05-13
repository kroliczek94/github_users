using GithubUsers.App.Models;
using GithubUsers.App.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GithubUsers.App.Services
{
    internal class UserService : IUserService
    {
        private readonly IGithubApiIntegrationService githubIntegrationService;
        private readonly IConfiguration configuration;
        private readonly IMemoryCache memoryCache;

        public UserService(IGithubApiIntegrationService githubIntegrationService,
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            this.githubIntegrationService = githubIntegrationService;
            this.configuration = configuration;
            this.memoryCache = memoryCache;
        }

        public async Task<(HttpStatusCode Status, StoredGithubUserInfo Info)> GetAsync(string owner)
        {
            StoredGithubUserInfo result = default;

            if (memoryCache.TryGetValue(owner, out var ownerData))
                result = (StoredGithubUserInfo)ownerData;

            var requestHeaders = new Dictionary<string, string>()
            {
                {"Authorization", $"token {configuration["GithubIntegration:AuthorizationToken"]}" },
                {"If-none-match",  $"\"{result?.ETag}\""}
            };

            var apiResponse = await githubIntegrationService.GetInfo(owner, requestHeaders);

            switch (apiResponse.Status)
            {
                case HttpStatusCode.OK:
                    {
                        memoryCache.Set(owner, result);
                        result = apiResponse.Info;
                        return (HttpStatusCode.OK, result);
                    }
                case HttpStatusCode.NotModified:
                    {
                        return (HttpStatusCode.OK, result);
                    }
                default:
                    {
                        return apiResponse;
                    }
            }
        }
    }
}
