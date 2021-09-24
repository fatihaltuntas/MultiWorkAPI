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

        public CreateBrandDto Create(CreateBrandDto createBrandDto)
        {
            var brandEntity = ObjectMapper.Map<Brand>(createBrandDto);
            _brandRepository.Insert(brandEntity);
            return createBrandDto;
         }

        public bool Delete(long brandId)
        {
            var anyBrand = _brandRepository.GetAll().Any(x => x.Id == brandId);
            if (anyBrand)
            {
                _brandRepository.Delete(brandId);
                return true;
            }

            return false;
            
        }

        //Bu methot brandId parametresi ile brandEntity si çekip ön tarafa BrandDto döner

        public BrandDto Get(long brandId)
        {
            Brand brandEntity = _brandRepository.Get(brandId);
            BrandDto brandDto = ObjectMapper.Map<BrandDto>(brandEntity);
            return brandDto;
        }

        public ListResultDto<BrandDto> GetAll(GetAllBrandsInput input)
        {
            var brands = _brandRepository
                .GetAll()
                .WhereIf(input.Status.HasValue, b => b.Status == input.Status.Value)
                .OrderByDescending(b => b.CreationTime)
                .ToList();

            return new ListResultDto<BrandDto>(
                ObjectMapper.Map<List<BrandDto>>(brands)
            );
        }

        public UpdateBrandDto Update(UpdateBrandDto updateBrandDto)
        {
            var brandEntity = ObjectMapper.Map<Brand>(updateBrandDto);
            _brandRepository.Update(brandEntity);
            return updateBrandDto;
        }


    }
}
