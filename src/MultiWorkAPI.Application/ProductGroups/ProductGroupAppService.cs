using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using MultiWorkAPI.ProductGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductGroups
{
    public class ProductGroupAppService : AsyncCrudAppService<ProductGroup, ProductGroupDto, long, PagedResultRequestDto, ProductGroupDto, ProductGroupDto>, IProductGroupAppService
    {

        private readonly IRepository<ProductGroup, long> _productGroupRepository;

        public ProductGroupAppService(IRepository<ProductGroup, long> productGroupRepository) : base(productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }

        [HttpPost]
        public override Task<PagedResultDto<ProductGroupDto>> GetAllAsync(PagedResultRequestDto input)
        {
            return base.GetAllAsync(input);
        }
        

        public override Task<ProductGroupDto> UpdateAsync(ProductGroupDto input)
        {
            var anySameName = _productGroupRepository.GetAll().Any(x => x.Title.ToUpper() == input.Title.ToUpper());

            if (!anySameName)
            {
                return base.UpdateAsync(input);
            }
            else
            {
              throw new UserFriendlyException("Marka Adı Mevcut !");
            }
                
        }

        public override Task<ProductGroupDto> CreateAsync(ProductGroupDto input)
        {
            var anySameName = _productGroupRepository.GetAll().Any(x => x.Title.ToUpper() == input.Title.ToUpper());
            if (!anySameName)
            {
                return base.CreateAsync(input);
            }
            else
            {
                throw new UserFriendlyException("Aynı Marka Kayıt Mevcut !");
            }
        }

    }


}
