using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MultiWorkAPI.Configuration;

namespace MultiWorkAPI.Web.Host.Startup
{
    [DependsOn(
       typeof(MultiWorkAPIWebCoreModule))]
    public class MultiWorkAPIWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MultiWorkAPIWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MultiWorkAPIWebHostModule).GetAssembly());
        }
    }
}
