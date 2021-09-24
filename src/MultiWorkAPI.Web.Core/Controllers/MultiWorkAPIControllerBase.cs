using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MultiWorkAPI.Controllers
{
    public abstract class MultiWorkAPIControllerBase: AbpController
    {
        protected MultiWorkAPIControllerBase()
        {
            LocalizationSourceName = MultiWorkAPIConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
