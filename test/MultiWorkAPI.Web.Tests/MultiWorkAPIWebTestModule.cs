using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MultiWorkAPI.EntityFrameworkCore;
using MultiWorkAPI.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace MultiWorkAPI.Web.Tests
{
    [DependsOn(
        typeof(MultiWorkAPIWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class MultiWorkAPIWebTestModule : AbpModule
    {
        public MultiWorkAPIWebTestModule(MultiWorkAPIEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MultiWorkAPIWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(MultiWorkAPIWebMvcModule).Assembly);
        }
    }
}