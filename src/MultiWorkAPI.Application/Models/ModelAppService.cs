using Abp.Application.Services;//
using Abp.Application.Services.Dto;//
using Abp.Collections.Extensions;//
using Abp.Domain.Repositories;//
using Abp.UI;//
using Microsoft.AspNetCore.Mvc;//
using Microsoft.EntityFrameworkCore;//
using MultiWorkAPI.Base.Dto;
using MultiWorkAPI.Model;// araştırılacak
using MultiWorkAPI.Models.Dto;//
using System;//
using System.Collections.Generic;//
using System.Linq;//
using System.Text;//
using System.Threading.Tasks;//
using static MultiWorkAPI.Models.Model;

namespace MultiWorkAPI.Models
{
    public class ModelAppService : AsyncCrudAppService<Model, ModelDto, long, PagedResultRequestDto, ModelDto, ModelDto>, IModelAppService
    {
        private readonly IRepository<Model, long> _modelRepository;

        public ModelAppService(IRepository<Model, long> modelRepository) : base(modelRepository)
        {
            _modelRepository = modelRepository;
        }
        [HttpPost]
        public override Task<PagedResultDto<ModelDto>> GetAllAsync(PagedResultRequestDto input)
        {
            return base.GetAllAsync(input);
        }

        public override Task<ModelDto> UpdateAsync(ModelDto input)
        {
            var existingModel = _modelRepository.Get(input.Id);
            var anySameName = _modelRepository.GetAll().Any(x => x.Title.ToUpper() == input.Title.ToUpper());
            if (existingModel.Title.ToLower() == input.Title.ToLower() || !anySameName)
            {
                input.EditedUserId = AbpSession.UserId.Value;
                return base.UpdateAsync(input);
            }
            else
            {
                throw new UserFriendlyException("Aynı Model Adı Mevcut !");
            }
        }

        public override Task<ModelDto> CreateAsync(ModelDto input)
        {
            var anySameName = _modelRepository.GetAll().Any(x => x.Title.ToLower() == input.Title.ToLower());
            if (!anySameName)
            {
                input.CreatedUserId = AbpSession.UserId.Value;
                input.EditedUserId = AbpSession.UserId.Value;
                return base.CreateAsync(input);
            }
            else
            {
                throw new UserFriendlyException("Aynı Model Adı Zaten Mevcut !");
            }
        }

        [HttpGet]
        public async Task<List<ModelDto>> GetActiveModel()
        {
            var modelEntityList = await _modelRepository.GetAll().Where(x => x.Status == ModelStatus.Accepted).ToListAsync();
            var modelListDto = ObjectMapper.Map<List<ModelDto>>(modelEntityList);
            return modelListDto;
        }

        [HttpPost]
        public async Task<PagedResultDto<ModelDto>> Filter(BaseFilterRequestDto request)
        {
            var brandQ = _modelRepository.GetAll();
            if (!string.IsNullOrEmpty(request.SearchWord))
                brandQ = brandQ.Where(x => x.Title.ToLower().Contains(request.SearchWord.ToLower()));
            if (request.Status > 0)
                brandQ = brandQ.Where(x => x.Status == (ModelStatus)request.Status);

            var brandListDto = ObjectMapper.Map<List<ModelDto>>(await brandQ.ToListAsync());
            return new PagedResultDto<ModelDto>()
            {
                Items = brandListDto,
                TotalCount = brandListDto.Count
            };
        }
    }
}
