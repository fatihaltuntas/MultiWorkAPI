using AutoMapper;
using MultiWorkAPI.Authorization.Users;

namespace MultiWorkAPI.Brands.Dto
{
    public class BrandMapProfile : Profile
    {
        public BrandMapProfile()
        {
            CreateMap<BrandDto, Brand>();
        }
    }
}
