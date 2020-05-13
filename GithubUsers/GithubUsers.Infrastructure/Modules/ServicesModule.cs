using Autofac;
using GithubUsers.App.Services.Interfaces;
using GithubUsers.Infrastructure.Services;

namespace GithubUsers.Infrastructure.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<GithubApiIntegrationService>()
                .As<IGithubApiIntegrationService>();
        }
    }
}
