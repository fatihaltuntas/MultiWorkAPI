using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using MultiWorkAPI.Brands.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands
{
    public class BrandAppService : MultiWorkAPIAppServiceBase, IBrandAppService
    {
        private readonly IRepository<Brand,long> _brandRepository;

        public BrandAppService(IRepository<Brand,long> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public ListResultDto<BrandListDto> GetAll(GetAllBrandsInput input)
        {
            var brands = _brandRepository
                .GetAll()
                .WhereIf(input.Status.HasValue, b => b.Status == input.Status.Value)
                .OrderByDescending(b => b.CreationTime)
                .ToList();

            return new ListResultDto<BrandListDto>(
                ObjectMapper.Map<List<BrandListDto>>(brands)
            );
        }
        public BrandListDto Get(long brandId)
        {
            var brand = _brandRepository.Get(brandId);
            var brandDto = ObjectMapper.Map<BrandListDto>(brand);
            return brandDto;
        }
        
        public CreateAndUpdateBrandDto Create(CreateAndUpdateBrandDto brandDto)
        {
            var brand = ObjectMapper.Map<Brand>(brandDto);
            _brandRepository.Insert(brand);
            return brandDto;
        }
        public bool Delete(long id)
        {
            var brandEntity = _brandRepository.Get(id);
            if(brandEntity != null)
            {
                _brandRepository.Delete(brandEntity);
                return true;
            }
            return false;

        }
    }
}
