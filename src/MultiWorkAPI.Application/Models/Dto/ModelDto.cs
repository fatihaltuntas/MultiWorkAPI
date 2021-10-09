using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MultiWorkAPI.ProductGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MultiWorkAPI.Models.Model;

namespace MultiWorkAPI.Models.Dto
{
    public class GetAllModelInput
    {
        public ModelStatus? Status { get; set; }
    }
    [AutoMapFrom(typeof(Model))]

    public class ModelDto:EntityDto<long>,IHasCreationTime
    {     
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public ModelStatus Status { get; set; }
        public long CreatedUserId { get; set; }
        public long EditedUserId { get; set; }
        public long BrandId { get; set; }
        public long ProductGroupId { get; set; }
    }
}
