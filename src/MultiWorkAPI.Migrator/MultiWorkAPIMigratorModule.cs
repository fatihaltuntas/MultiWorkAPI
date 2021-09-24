using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MultiWorkAPI.Configuration;
using MultiWorkAPI.EntityFrameworkCore;
using MultiWorkAPI.Migrator.DependencyInjection;

namespace MultiWorkAPI.Migrator
{
    [DependsOn(typeof(MultiWorkAPIEntityFrameworkModule))]
    public class MultiWorkAPIMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public MultiWorkAPIMigratorModule(MultiWorkAPIEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(MultiWorkAPIMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                MultiWorkAPIConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MultiWorkAPIMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
