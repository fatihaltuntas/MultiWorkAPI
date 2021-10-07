using AutoMapper;
using MultiWorkAPI.Authorization.Users;

namespace MultiWorkAPI.Models.Dto
{
    public class ModelMapProfile: Profile
    {
        public ModelMapProfile()
        {
            CreateMap<ModelDto, Model>();
        }
    }
}
