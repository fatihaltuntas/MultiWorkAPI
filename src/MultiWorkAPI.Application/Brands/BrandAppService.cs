using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Timing;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiWorkAPI.Base.Dto;
using MultiWorkAPI.Brands.Dto;
using MultiWorkAPI.ProductGroups;
using MultiWorkAPI.ProductGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands
{
    public class BrandAppService : AsyncCrudAppService<Brand, BrandDto, long, PagedResultRequestDto, BrandDto, BrandDto>, IBrandAppService
    {
        private readonly IRepository<Brand, long> _brandRepository;
        private readonly IRepository<ProductGroupBrand, long> _productGroupBrandRepository;
        private readonly IRepository<ProductGroup, long> _productGroupRepository;


        public BrandAppService(IRepository<Brand, long> brandRepository,
            IRepository<ProductGroupBrand, long> productGroupBrandRepository,
            IRepository<ProductGroup, long> productGroupRepository) : base(brandRepository)
        {
            _brandRepository = brandRepository;
            _productGroupBrandRepository = productGroupBrandRepository;
            _productGroupRepository = productGroupRepository;
        }

        public override async Task<BrandDto> GetAsync(EntityDto<long> input)
        {
            var productGroupIds = _productGroupBrandRepository.GetAll().Where(x => x.BrandId == input.Id).Select(x => x.ProductGroupId);
            var productGroups = await _productGroupRepository.GetAll().Where(x => productGroupIds.Contains(x.Id)).ToListAsync();
            var productGroupDtos = ObjectMapper.Map<List<ProductGroupDto>>(productGroups);
            var brandDto = ObjectMapper.Map<BrandDto>(await _brandRepository.GetAsync(input.Id));
            brandDto.SelectedProductGroups = productGroupDtos;
            return brandDto;
        }

        [HttpPost]
        public async Task<PagedResultDto<BrandDto>> Filter(BaseFilterRequestDto request)
        {
            var brandQ = _brandRepository.GetAll();
            if (!string.IsNullOrEmpty(request.SearchWord))
                brandQ = brandQ.Where(x => x.Title.ToLower().Contains(request.SearchWord.ToLower()));
            if (request.Status > 0)
                brandQ = brandQ.Where(x => x.Status == (BrandStatus)request.Status);

            var brandListDto = ObjectMapper.Map<List<BrandDto>>(await brandQ.ToListAsync());
            return new PagedResultDto<BrandDto>()
            {
                Items = brandListDto,
                TotalCount = brandListDto.Count
            };
        }

        [AbpAuthorize]
        [HttpPost]
        public async override Task<PagedResultDto<BrandDto>> GetAllAsync(PagedResultRequestDto input)
        {
            return await base.GetAllAsync(input);
        }
        public override async Task<BrandDto> UpdateAsync(BrandDto input)
        {
            var anySameNamebrand = _brandRepository.GetAll().Any(x => x.Title.ToUpper() == input.Title.ToUpper());
            var brand = ObjectMapper.Map<Brand>(input);
            var brandId = await _brandRepository.InsertOrUpdateAndGetIdAsync(brand);
            if (brandId > 0)
            {
                DeleteThenCreateProductGroupBrands(input.Id, input.SelectedProductGroups);
            }
            return MapToEntityDto(brand);

        }
        [AbpAuthorize]
        public override async Task<BrandDto> CreateAsync(BrandDto input)
        {
            var brand = ObjectMapper.Map<Brand>(input);
            var brandId = await _brandRepository.InsertAndGetIdAsync(brand);
            if (brandId > 0)
            {
                foreach (var productGroup in input.SelectedProductGroups)
                {
                    _productGroupBrandRepository.Insert(new ProductGroupBrand()
                    {
                        BrandId = brandId,
                        CreatedUserId = 0,
                        CreationTime = Clock.Now,
                        EditedUserId = 0,
                        ProductGroupId = productGroup.Id,
                        Status = ProductGroupBrandStatus.Accepted

                    });

                }
            }
            return MapToEntityDto(brand);
        }
        private void DeleteThenCreateProductGroupBrands(long brandId, List<ProductGroupDto> productGroupDtos)
        {
            var addedProductGroupBrands = _productGroupBrandRepository.GetAll().Where(x => x.BrandId == brandId).ToList();
            foreach (var item in addedProductGroupBrands)
            {
                _productGroupBrandRepository.Delete(item.Id);
            }
            foreach (var item in productGroupDtos)
            {
                _productGroupBrandRepository.InsertAsync(new ProductGroupBrand()
                {
                    BrandId = brandId,
                    CreatedUserId = 0,
                    CreationTime = Clock.Now,
                    EditedUserId = 0,
                    ProductGroupId = item.Id,
                    Status = ProductGroupBrandStatus.Accepted
                });
            }
        }

        [HttpGet]
        public async Task<List<BrandDto>> GetActiveBrands()
        {
            var entityList = await _brandRepository.GetAll().Where(x => x.Status == BrandStatus.Accepted).ToListAsync();
            var listDto = ObjectMapper.Map<List<BrandDto>>(entityList);
            return listDto;
        }

    }

}

