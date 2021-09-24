using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultiWorkAPI.Brands.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands
{
    public interface IBrandAppService : IApplicationService
    {
        ListResultDto<BrandListDto> GetAll(GetAllBrandsInput input);

    }
}
