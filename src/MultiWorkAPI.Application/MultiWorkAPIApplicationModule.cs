using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MultiWorkAPI.Authorization;

namespace MultiWorkAPI
{
    [DependsOn(
        typeof(MultiWorkAPICoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MultiWorkAPIApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MultiWorkAPIAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MultiWorkAPIApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
