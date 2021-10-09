using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultiWorkAPI.Base.Dto;
using MultiWorkAPI.ProductGroups.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductGroups
{
    public interface IProductGroupAppService : IAsyncCrudAppService<ProductGroupDto, long, PagedResultRequestDto, ProductGroupDto, ProductGroupDto>
    {
        //ListResultDto<ProductGroupDto> GetProductGroupByCreatedUser(CreatedUserId createdUser);
        Task<List<ProductGroupDto>> GetActiveProductGroups();
        Task<PagedResultDto<ProductGroupDto>> Filter(BaseFilterRequestDto request);
        Task<List<ProductGroupDto>> GetProductGroupsByBrandId(long brandId);
    }
}
