using System.Threading.Tasks;
using MultiWorkAPI.Configuration.Dto;

namespace MultiWorkAPI.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
