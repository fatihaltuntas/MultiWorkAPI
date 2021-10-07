using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultiWorkAPI.Brands.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiWorkAPI.ProductGroups.Dto;
using MultiWorkAPI.Base.Dto;

namespace MultiWorkAPI.Brands
{
    public interface IBrandAppService : IAsyncCrudAppService<BrandDto, long, PagedResultRequestDto, BrandDto,BrandDto>
    {
        Task<PagedResultDto<BrandDto>> Filter(BaseFilterRequestDto request);
    }

}
