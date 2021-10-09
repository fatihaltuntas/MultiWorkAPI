using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultiWorkAPI.Base.Dto;
using MultiWorkAPI.ProductItems.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductItems
{
    public interface IProductItemAppService : IAsyncCrudAppService<ProductItemDto, long, PagedResultRequestDto, ProductItemDto, ProductItemDto>
    {
        Task<PagedResultDto<ProductItemDto>> Filter(BaseFilterRequestDto request);
    }
}
