using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GithubUsers.Infrastructure.Utils
{
    public static class HttpClientUtil
    {
        private const string ApplicationJsonMediaType = "application/json";

        public static async Task<HttpResponseMessage> GetAsync(string baseAddress, Dictionary<string, string> headers = null)
        {
            var client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJsonMediaType));
            client.DefaultRequestHeaders.UserAgent.ParseAdd("MyAgent/1.0");

            foreach (var header in headers)
                client.DefaultRequestHeaders.Add(header.Key, header.Value);

            return await client.GetAsync(baseAddress);
        }
    }
}
