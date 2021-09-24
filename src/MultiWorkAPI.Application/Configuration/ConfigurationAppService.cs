using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MultiWorkAPI.Configuration.Dto;

namespace MultiWorkAPI.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MultiWorkAPIAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
