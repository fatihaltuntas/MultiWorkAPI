using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiWorkAPI.Base.Dto;
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
        private readonly IRepository<ProductGroupBrand, long> _productGroupBrandRepository;

        public ProductGroupAppService(IRepository<ProductGroup, long> productGroupRepository, IRepository<ProductGroupBrand,
            long> productGroupBrandRepository) : base(productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
            _productGroupBrandRepository = productGroupBrandRepository;
        }

        [HttpPost]
        public async override Task<PagedResultDto<ProductGroupDto>> GetAllAsync(PagedResultRequestDto input)
        {
            return await base.GetAllAsync(input);
        }

        public async override Task<ProductGroupDto> UpdateAsync(ProductGroupDto input)
        {
            var existingProductGroup = _productGroupRepository.Get(input.Id);
            var anySameName = _productGroupRepository.GetAll().Any(x => x.Title.ToUpper() == input.Title.ToUpper());
            if (existingProductGroup.Title.ToLower() == input.Title.ToLower() || !anySameName)
            {
                input.EditedUserId = AbpSession.UserId.Value;
                return await base.UpdateAsync(input);
            }
            throw new UserFriendlyException("Marka Adı Mevcut !");

        }

        public async override Task<ProductGroupDto> CreateAsync(ProductGroupDto input)
        {
            var anySameName = _productGroupRepository.GetAll().Any(x => x.Title.ToLower() == input.Title.ToLower());

            if (!anySameName)
            {
                input.CreatedUserId = AbpSession.UserId.Value;
                return await base.CreateAsync(input);
            }
            else
            {
                throw new UserFriendlyException("Girdiğiniz ürün grubu daha önceden kaydedilmiş.");
            }
        }

        [HttpPost]
        public async Task<PagedResultDto<ProductGroupDto>> Filter(BaseFilterRequestDto request)
        {
            var productGroupQ = _productGroupRepository.GetAll();

            if (!string.IsNullOrEmpty(request.SearchWord))
                productGroupQ = productGroupQ.Where(x => x.Title.ToLower().Contains(request.SearchWord.ToLower()));
            if (request.Status > 0)
                productGroupQ = productGroupQ.Where(x => x.Status == (ProductGroupStatus)request.Status);

            productGroupQ = productGroupQ.OrderBy(x => x.Title);

            var productGroupListDto = ObjectMapper.Map<List<ProductGroupDto>>(await productGroupQ.ToListAsync());
            return new PagedResultDto<ProductGroupDto>()
            {
                Items = productGroupListDto,
                TotalCount = productGroupListDto.Count
            };
        }

        [HttpGet]
        public async Task<List<ProductGroupDto>> GetProductGroupsByBrandId(long brandId)
        {
            var productGroupIds = _productGroupBrandRepository.GetAll().Where(x => x.BrandId == brandId).Select(x => x.ProductGroupId);
            var productGroupEntityList = await _productGroupRepository.GetAll().Where(x => productGroupIds.Contains(x.Id) && x.Status == ProductGroupStatus.Accepted).ToListAsync();
            return ObjectMapper.Map<List<ProductGroupDto>>(productGroupEntityList);
        }
    }

}
