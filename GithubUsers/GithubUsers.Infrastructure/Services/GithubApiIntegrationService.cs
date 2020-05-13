using GithubUsers.App.Models;
using GithubUsers.App.Services.Interfaces;
using GithubUsers.Infrastructure.Models;
using GithubUsers.Infrastructure.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GithubUsers.Infrastructure.Services
{
    internal partial class GithubApiIntegrationService : IGithubApiIntegrationService
    {
        private const string baseUri = "https://api.github.com/";

        private readonly ILogger<GithubApiIntegrationService> logger;
        private readonly string getUsersTemplate = "users/{0}/repos";
        public GithubApiIntegrationService(ILogger<GithubApiIntegrationService> logger)
        {
            this.logger = logger;
        }

        public async Task<(HttpStatusCode Status, StoredGithubUserInfo Info)> GetInfo(string userName, Dictionary<string, string> headers)
        {
            logger.LogInformation($"Fetching for {userName}");
            UserRepositoryParseModel[] repositoryInfos = Array.Empty<UserRepositoryParseModel>();

            var result = await HttpClientUtil.GetAsync($"{baseUri}{String.Format(getUsersTemplate, userName)}", headers);

            if (!result.IsSuccessStatusCode)
                return (result.StatusCode, null);

            var str = await result.Content.ReadAsStringAsync();
            repositoryInfos = JsonConvert.DeserializeObject<UserRepositoryParseModel[]>(str);

            if (!repositoryInfos.Any())
                return (HttpStatusCode.OK, new StoredGithubUserInfo { ETag = result.Headers.ETag.Tag, Owner = userName });

            var response = repositoryInfos.GroupBy(info => true)
                .Select(info => new StoredGithubUserInfo
                {
                    Owner = userName,
                    AvgForks = info.Average(w => w.ForksCount),
                    AvgSize = info.Average(w => w.Size),
                    AvgStargazers = info.Average(w => w.StargrazersCount),
                    Letters = info
                        .SelectMany(repoInfo => repoInfo.Name.ToLowerInvariant())
                        .Where(ch => char.IsLetter(ch))
                        .GroupBy(ch => ch)
                        .OrderBy(ch => ch.Key)
                        .ToDictionary(chGroup => chGroup.Key.ToString(), chGroup => chGroup.Count()),
                    ETag = result.Headers.ETag.Tag
                }).Single();

            return (HttpStatusCode.OK, response);
        }
    }
}
