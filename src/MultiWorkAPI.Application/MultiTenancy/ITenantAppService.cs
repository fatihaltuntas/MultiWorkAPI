using Abp.Application.Services;
using MultiWorkAPI.MultiTenancy.Dto;

namespace MultiWorkAPI.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

