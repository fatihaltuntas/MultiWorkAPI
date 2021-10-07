using Abp.Application.Services;//
using Abp.Application.Services.Dto;//
using Abp.Collections.Extensions;//
using Abp.Domain.Repositories;//
using Abp.UI;//
using Microsoft.AspNetCore.Mvc;//
using Microsoft.EntityFrameworkCore;//
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

        //public Task DeleteAsync(EntityDto<long> input)
        //{
        //    throw new NotImplementedException();
        //}
        [HttpPost]
        public override Task<PagedResultDto<ModelDto>> GetAllAsync(PagedResultRequestDto input)
        {
            return base.GetAllAsync(input);
        }

        //public override Task<ModelDto> GetAsync(EntityDto<long> input)
        //{
        //    return base.GetAsync(input);
        //}

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
        public async Task<PagedResultDto<ModelDto>> Search(string keyword)
        {
            var modelQ = _modelRepository.GetAll();
            modelQ = modelQ.Where(x => x.Title.ToLower().Contains(keyword.ToLower()));
            var modelListDto = ObjectMapper.Map<List<ModelDto>>(modelQ.ToList());
            return new PagedResultDto<ModelDto>()
            {
                Items = modelListDto,
                TotalCount = modelListDto.Count
            };
        }

        [HttpGet]
        public async Task<List<ModelDto>> GetActiveModel()
        {
            var modelEntityList = await _modelRepository.GetAll().Where(x => x.Status == ModelStatus.Accepted).ToListAsync();
            var modelListDto = ObjectMapper.Map<List<ModelDto>>(modelEntityList);
            return modelListDto;
        }













        //Task<ModelDto> IAsyncCrudAppService<ModelDto, long, PagedResultRequestDto, ModelDto, ModelDto, EntityDto<long>, EntityDto<long>>.CreateAsync(ModelDto input)
        //{
        //    throw new NotImplementedException();
        //}
        //Task<ModelDto> IAsyncCrudAppService<ModelDto, long, PagedResultRequestDto, ModelDto, ModelDto, EntityDto<long>, EntityDto<long>>.UpdateAsync(ModelDto input)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
