using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultiWorkAPI.Base.Dto;
using MultiWorkAPI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Model
{
    public interface IModelAppService : IAsyncCrudAppService<ModelDto,long,PagedResultRequestDto,ModelDto,ModelDto>
    {
        Task<PagedResultDto<ModelDto>> Filter(BaseFilterRequestDto request);
    }
}
