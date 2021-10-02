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

        [HttpGet]
        public List<ProductGroupDto> GetActiveProductGroups()
        {
            var entityList = _productGroupRepository.GetAll().Where(x => x.Status == ProductGroupStatus.Accepted).ToList();
            var listDto = ObjectMapper.Map<List<ProductGroupDto>>(entityList);
            return listDto;
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

        [HttpGet]
        public async Task<PagedResultDto<ProductGroupDto>> Search(string keyword)
        {
            var productGroupQ = _productGroupRepository.GetAll();
            productGroupQ = productGroupQ.Where(x => x.Title.ToLower().Contains(keyword.ToLower()));
            var productGroupListDto = ObjectMapper.Map<List<ProductGroupDto>>(productGroupQ.ToList());
            return new PagedResultDto<ProductGroupDto>()
            {
                Items = productGroupListDto,
                TotalCount = productGroupListDto.Count
            };
        }
    }

}
