using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands.Dto
{
    public class GetAllBrandsInput
    {
        public BrandStatus? Status { get; set; }
    }
    [AutoMapFrom(typeof(Brand))]
    public class BrandListDto : EntityDto<long>, IHasCreationTime
    {
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public BrandStatus Status { get; set; }
        public long CreatedUserId { get; set; }
        public long EditedUserId { get; set; }
    }
}
