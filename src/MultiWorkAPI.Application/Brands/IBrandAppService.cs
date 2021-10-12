using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultiWorkAPI.Brands.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands
{
    public interface IBrandAppService : IAsyncCrudAppService<BrandDto, long, PagedResultRequestDto, BrandDto,BrandDto>
    {
        Task<PagedResultDto<BrandDto>> Filter(BrandFilterRequestDto request);
    }

}
