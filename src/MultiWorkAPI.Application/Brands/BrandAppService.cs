using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using MultiWorkAPI.Brands.Dto;
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

        public BrandAppService(IRepository<Brand, long> brandRepository) : base(brandRepository)
        {
            _brandRepository = brandRepository;
        }
        [AbpAuthorize]
        [HttpPost]
        public override Task<PagedResultDto<BrandDto>> GetAllAsync(PagedResultRequestDto input)
        {
            return base.GetAllAsync(input);
        }
       
        public override Task<BrandDto> UpdateAsync(BrandDto input)
        {
            var anySameNamebrand = _brandRepository.GetAll().Any(x => x.Title.ToUpper() == input.Title.ToUpper());
            if (!anySameNamebrand)
            {
                return base.UpdateAsync(input);
            }
            else
            {
                throw new UserFriendlyException("Aynı isme sahip kayıt zaten mevcut!");
            }
        }

        public override Task<BrandDto> CreateAsync(BrandDto input)
        {
            var anySameNameBrand = _brandRepository.GetAll().Any(x => x.Title.ToUpper() == input.Title.ToUpper());
            if (anySameNameBrand)
            {
                return base.CreateAsync(input);
            }
            else
            {
                throw new UserFriendlyException("aynı isme sahip kayız zaten mevcut!");
            }
        }      

    }
}
