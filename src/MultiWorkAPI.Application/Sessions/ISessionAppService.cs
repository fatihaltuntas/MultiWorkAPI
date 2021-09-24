using System.Threading.Tasks;
using Abp.Application.Services;
using MultiWorkAPI.Sessions.Dto;

namespace MultiWorkAPI.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
