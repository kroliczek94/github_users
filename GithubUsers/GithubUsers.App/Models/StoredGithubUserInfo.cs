using System.Collections.Generic;

namespace GithubUsers.App.Models
{
    public class StoredGithubUserInfo
    {
        public string ETag { get; set; }
        public string Owner { get; set; }
        public double AvgStargazers { get; set; }
        public double AvgSize { get; set; }
        public double AvgForks { get; set; }
        public Dictionary<string, int> Letters { get; set; }
    }
}
