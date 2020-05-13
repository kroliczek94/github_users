using Newtonsoft.Json;

namespace GithubUsers.Infrastructure.Models
{
    public class UserRepositoryParseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("stargazers_count")]
        public int StargrazersCount { get; set; }
        [JsonProperty("watchers_count")]
        public int WatchersCount { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
        [JsonProperty("forks_count")]
        public int ForksCount { get; set; }
    }
}
