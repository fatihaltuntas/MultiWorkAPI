using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultiWorkAPI.ProductGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductGroups
{
    public interface IProductGroupAppService :IApplicationService
    {
        ListResultDto<ProductGroupDto> GetAll(GetAllGroupInput input);
        CreateProductGroupDto Create(CreateProductGroupDto createProductGroupDto);
        ProductGroupDto Get(long productGroupId);
        UpdateProductGroupDto Update(UpdateProductGroupDto updateProductGroupDto);

        bool Delete(long productGroupId);
        ListResultDto<ProductGroupDto> GetProductGroupByStatus(ProductGroupStatus status);
        //ListResultDto<ProductGroupDto> GetProductGroupByCreatedUser(CreatedUserId createdUser);
        
    }
}
