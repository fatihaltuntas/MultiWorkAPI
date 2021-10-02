using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Timing;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using MultiWorkAPI.Brands.Dto;
using MultiWorkAPI.ProductGroups;
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

        public BrandAppService(IRepository<Brand, long> brandRepository, IRepository<ProductGroupBrand, long> productGroupBrandRepository) : base(brandRepository)
        {
            _brandRepository = brandRepository;
            _productGroupBrandRepository = productGroupBrandRepository;
        }
        [AbpAuthorize]
        [HttpPost]
        public override Task<PagedResultDto<BrandDto>> GetAllAsync(PagedResultRequestDto input)
        {
            return base.GetAllAsync(input);
        }




        //----------------------------------Update ------------------------------
        // var anySameNamebrand = _brandRepository.GetAll().Any(x => x.Title.ToUpper() == input.Title.ToUpper());
        //if (!anySameNamebrand)
        //{
        //    return base.UpdateAsync(input);
        //}
        //else
        //{
        //    throw new UserFriendlyException("Aynı isme sahip kayıt zaten mevcut!");
        //}
        public override async Task<BrandDto> UpdateAsync(BrandDto input)
        {
            var anySameNamebrand = _brandRepository.GetAll().Any(x => x.Title.ToUpper() == input.Title.ToUpper());
            var brand = ObjectMapper.Map<Brand>(input);
            var brandId = await _brandRepository.InsertOrUpdateAndGetIdAsync(brand);
            if (!anySameNamebrand)
            {                
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
                
                //return MapToEntityDto(brand);

            }
            return MapToEntityDto(brand);
           // return base.UpdateAsync(brand);

        }
        [AbpAuthorize]
        public override async Task<BrandDto> CreateAsync(BrandDto input)
        {
            var brand = ObjectMapper.Map<Brand>(input);
            var brandId = await _brandRepository.InsertAndGetIdAsync(brand);
            if (brandId >0)
            {
                foreach (var productGroup in input.SelectedProductGroups)
                {
                    _productGroupBrandRepository.Insert(new ProductGroupBrand()
                    {
                        BrandId = brandId,
                        CreatedUserId=0,
                        CreationTime=Clock.Now,
                        EditedUserId=0,
                        ProductGroupId=productGroup.Id,
                        Status=ProductGroupBrandStatus.Accepted

                    });

                }
            }
            return MapToEntityDto(brand);

        }
    }

}

