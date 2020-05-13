using Autofac;
using GithubUsers.App.Services;
using GithubUsers.App.Services.Interfaces;

namespace GithubUsers.App.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
               .RegisterType<UserService>()
               .As<IUserService>();
        }
    }
}
