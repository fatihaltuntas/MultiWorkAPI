using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiWorkAPI.Base.Dto;
using MultiWorkAPI.Brands;
using MultiWorkAPI.Brands.Dto;
using MultiWorkAPI.ProductGroups;
using MultiWorkAPI.ProductItems.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MultiWorkAPI.Models.Model;
using static MultiWorkAPI.ProductItems.ProductItem;

namespace MultiWorkAPI.ProductItems
{
    public class ProductItemAppService : AsyncCrudAppService<ProductItem, ProductItemDto, long, PagedResultRequestDto, ProductItemDto, ProductItemDto>, IProductItemAppService
    {
        private readonly IRepository<ProductItem, long> _producItemrepository;
        private readonly IRepository<ProductGroup, long> _productGroupRepository;
        private readonly IRepository<Brand, long> _brandRepository;
        private readonly IRepository<Models.Model, long> _modelRepository;
        public ProductItemAppService(IRepository<ProductItem, long> productItemsRepository,
            IRepository<ProductGroup, long> productGroupRepository,
            IRepository<Brand, long> brandRepository,
            IRepository<Models.Model, long> modelRepository) : base(productItemsRepository)
        {
            _producItemrepository = productItemsRepository;
            _productGroupRepository = productGroupRepository;
            _brandRepository = brandRepository;
            _modelRepository = modelRepository;
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

            var productGroups = _productGroupRepository.GetAll().Where(x => x.Status == ProductGroupStatus.Accepted).ToList();
            var brands = _brandRepository.GetAll().Where(x => x.Status == BrandStatus.Accepted).ToList();
            var models = _modelRepository.GetAll().Where(x => x.Status == ModelStatus.Accepted).ToList();

            foreach (var productItem in productItemListDto)
            {
                productItem.ProductGroupName = productGroups.FirstOrDefault(x => x.Id == productItem.ProductGroupId).Title;
                productItem.BrandName = brands.FirstOrDefault(x => x.Id == productItem.BrandId).Title;
                productItem.ModelName = models.FirstOrDefault(x => x.Id == productItem.ModelId).Title;
            }
           
            
            return new PagedResultDto<ProductItemDto>()
            {
                Items = productItemListDto,
                TotalCount = productItemListDto.Count
            };
        }
    }
}
