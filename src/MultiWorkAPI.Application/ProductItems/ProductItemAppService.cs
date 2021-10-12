using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiWorkAPI.Base.Dto;
using MultiWorkAPI.Brands.Dto;
using MultiWorkAPI.ProductItems.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MultiWorkAPI.ProductItems.ProductItem;

namespace MultiWorkAPI.ProductItems
{
    public class ProductItemAppService : AsyncCrudAppService<ProductItem, ProductItemDto, long, PagedResultRequestDto, ProductItemDto, ProductItemDto>, IProductItemAppService
    {
        private readonly IRepository<ProductItem, long> _producItemrepository;
        public ProductItemAppService(IRepository<ProductItem, long> productItemsRepository) : base(productItemsRepository)
        {
            _producItemrepository = productItemsRepository;
        }

        [HttpPost]
        public override Task<PagedResultDto<ProductItemDto>> GetAllAsync(PagedResultRequestDto input)
        {
            return base.GetAllAsync(input);
        }
        public override Task<ProductItemDto> UpdateAsync(ProductItemDto input)
        {
            var existingProductItems = _producItemrepository.Get(input.Id);
            var anySameName = _producItemrepository.GetAll().Any(x => x.SerialNumber.ToUpper() == input.SerialNumber.ToUpper());
            if (existingProductItems.SerialNumber.ToLower() == input.SerialNumber.ToLower() || !anySameName)
            {
                input.EditedUserId = AbpSession.UserId.Value;
                return base.UpdateAsync(input);
            }
            else
            {
                throw new UserFriendlyException("Aynı Seri Numaralı Ürün Mevcut !");
            }
        }
        [HttpPost]
        public override Task<ProductItemDto> CreateAsync(ProductItemDto input)
        {
            input.CreationUserId = AbpSession.UserId.Value;
            return base.CreateAsync(input);
        }
        [HttpPost]
        public async Task<PagedResultDto<ProductItemDto>> Filter(ProductItemFilterRequestDto request)
        {
            var productItemQ = _producItemrepository.GetAll();
            if (!string.IsNullOrEmpty(request.SearchWord))
                productItemQ = productItemQ.Where(x => x.SerialNumber.ToLower().Contains(request.SearchWord.ToLower()));
            if (request.Status > 0)
                productItemQ = productItemQ.Where(x => x.Status == (ProductItemStatus)request.Status);
            if (request.UserId > 0)
                productItemQ = productItemQ.Where(x => x.UserId == request.UserId);
            if (request.ProductGroupId > 0)
                productItemQ = productItemQ.Where(x => x.ProductGroupId == request.ProductGroupId);
            if (request.BrandId > 0)
                productItemQ = productItemQ.Where(x => x.BrandId == request.BrandId);
            if (request.ModelId > 0)
                productItemQ = productItemQ.Where(x => x.ModelId == request.ModelId);

            productItemQ = productItemQ.OrderBy(x => x.Title);
            var productItemListDto = ObjectMapper.Map<List<ProductItemDto>>(await productItemQ.ToListAsync());
            return new PagedResultDto<ProductItemDto>()
            {
                Items = productItemListDto,
                TotalCount = productItemListDto.Count
            };
        }
    }
}
