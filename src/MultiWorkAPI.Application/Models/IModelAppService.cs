using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultiWorkAPI.Brands.Dto;
using MultiWorkAPI.Models.Dto;
using System.Threading.Tasks;

namespace MultiWorkAPI.Model
{
    public interface IModelAppService : IAsyncCrudAppService<ModelDto, long, PagedResultRequestDto, ModelDto, ModelDto>
    {
        Task<PagedResultDto<ModelDto>> Filter(ModelFilterRequestDto request);
    }
}
