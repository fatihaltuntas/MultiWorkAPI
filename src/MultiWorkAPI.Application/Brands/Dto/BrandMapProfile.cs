using AutoMapper;
using MultiWorkAPI.Authorization.Users;

namespace MultiWorkAPI.Brands.Dto
{
    public class BrandMapProfile : Profile
    {
        public BrandMapProfile()
        {
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<UpdateBrandDto, Brand>();
        }
    }
}
