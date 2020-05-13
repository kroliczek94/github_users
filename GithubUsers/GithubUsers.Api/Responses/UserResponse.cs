using System.Collections.Generic;

namespace GithubUsers.Api.Responses
{
    public class UserRepositoryInfoResponse
    {
        public string Owner { get; set; }
        public double AvgStargazers { get; set; }
        public double AvgSize { get; set; }
        public double AvgForks { get; set; }
        public Dictionary<string, int> Letters { get; set; }
    }
}