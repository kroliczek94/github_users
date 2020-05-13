using AutoMapper;
using GithubUsers.Api.Responses;
using GithubUsers.App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GithubUsers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RepositoriesController : ControllerBase
    {
        private readonly ILogger<RepositoriesController> logger;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public RepositoriesController(ILogger<RepositoriesController> logger,
            IUserService userService,
            IMapper mapper)
        {
            this.logger = logger;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet("{owner}")]
        [ResponseCache(Duration = 10)]
        public async Task<IActionResult> Get(string owner)
        {
            try
            {
                var (Status, Info) = await userService.GetAsync(owner);

                if (Status != HttpStatusCode.OK)
                    return NotFound($"Could not find info for user: {owner}, status code : {Status}");

                if (Info == null)
                    return NotFound($"Could not find info for user: {owner}");

                return new ObjectResult(mapper.Map<UserRepositoryInfoResponse>(Info));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error on sending request for user {owner}");
                return NotFound($"Could not find info for user: {owner} due to exception {ex}");
            }
        }
    }
}
