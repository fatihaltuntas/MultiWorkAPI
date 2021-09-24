using AutoMapper;
using MultiWorkAPI.Brands.Dto;
using MultiWorkAPI.Brands;

namespace MultiWorkAPI.Brands.Dto
{
    public class BrandMapProfile : Profile
    {
        public BrandMapProfile()
        {

            CreateMap<CreateAndUpdateBrandDto,Brand>();
        }
    }
}
