using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiWorkAPI.Base.Dto;
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
        public override Task<ProductItemDto>UpdateAsync(ProductItemDto input)
        {
            var existingProductItems = _producItemrepository.Get(input.Id);
            var anySameName = _producItemrepository.GetAll().Any(x => x.SerialNumber.ToUpper() == input.SerialNumber.ToUpper());
            if (existingProductItems.SerialNumber.ToLower()==input.SerialNumber.ToLower()|| !anySameName)
            {
                input.EditedUserId = AbpSession.UserId.Value;
                return base.UpdateAsync(input);
            }
            else
            {
                throw new UserFriendlyException("Aynı Seri Numaralı Ürün Mevcut !");
            }
        }
        [HttpGet]
        public async Task<List<ProductItemDto>> GetActiveModel()
        {
            var productItemEntityList = await _producItemrepository.GetAll().Where(x => x.Status == ProductItemStatus.Stock).ToListAsync();
            var productItemListDto = ObjectMapper.Map<List<ProductItemDto>>(productItemEntityList);
            return productItemListDto;
        }
        [HttpPost]
        public async Task<PagedResultDto<ProductItemDto>>Filter(BaseFilterRequestDto request)
        {
            var brandQ = _producItemrepository.GetAll();
            if (!string.IsNullOrEmpty(request.SearchWord))
            
                brandQ = brandQ.Where(x => x.SerialNumber.ToLower().Contains(request.SearchWord.ToLower()));

            if (request.Status > 0)
                brandQ = brandQ.Where(x => x.Status == (ProductItemStatus)request.Status);
            var productItemListDto = ObjectMapper.Map<List<ProductItemDto>>(await brandQ.ToListAsync());
            return new PagedResultDto<ProductItemDto>()
            {
                Items = productItemListDto,
                TotalCount = productItemListDto.Count
            };
        }
    }
}
