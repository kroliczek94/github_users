using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Linq;
using System.Reflection;

namespace GithubUsers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddLogging(ConfigureLogging);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var runtime = DependencyContext.Default.Target.Runtime;

            var runtimeAssemblies = DependencyContext.Default.GetRuntimeAssemblyNames(runtime)
                .Where(name => name.Name.StartsWith("GithubUsers."))
                .Distinct()
                .Select(Assembly.Load);

            builder.RegisterAssemblyModules(runtimeAssemblies.ToArray());
            builder.Register(context => Configuration);
        }

        private static void ConfigureLogging(ILoggingBuilder builder)
        {
            CreateLogger(Configuration);
            builder.AddSerilog();
        }

        private static void CreateLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCaching();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
