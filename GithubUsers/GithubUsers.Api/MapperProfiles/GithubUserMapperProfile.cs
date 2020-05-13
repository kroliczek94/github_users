using AutoMapper;
using GithubUsers.Api.Responses;
using GithubUsers.App.Models;
using GithubUsers.App.Services.Interfaces;

namespace GithubUsers.Api.MapperProfiles
{
    public class GithubUserMapperProfile : Profile
    {
        public GithubUserMapperProfile()
        {
            CreateMap<StoredGithubUserInfo, UserRepositoryInfoResponse>();
        }
    }
}
