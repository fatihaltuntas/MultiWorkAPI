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
    }
}
