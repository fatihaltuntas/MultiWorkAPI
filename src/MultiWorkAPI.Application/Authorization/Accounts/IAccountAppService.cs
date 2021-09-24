using System.Threading.Tasks;
using Abp.Application.Services;
using MultiWorkAPI.Authorization.Accounts.Dto;

namespace MultiWorkAPI.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
