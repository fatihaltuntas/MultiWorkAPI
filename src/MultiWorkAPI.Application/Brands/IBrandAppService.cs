using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultiWorkAPI.Brands.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands
{
    public interface IBrandAppService : IApplicationService
    {
        ListResultDto<BrandDto> GetAll(GetAllBrandsInput input);
        CreateBrandDto Create(CreateBrandDto createBrandDto);
        BrandDto Get(long brandId);
        UpdateBrandDto Update(UpdateBrandDto updateBrandDto);
        
        // bool[true yada false](Dışa dönüş tipi) Delete( Metoda atadığın isimI) long(Dışarıdan aldığım değer.)
        bool Delete(long brandId);

    }

}
