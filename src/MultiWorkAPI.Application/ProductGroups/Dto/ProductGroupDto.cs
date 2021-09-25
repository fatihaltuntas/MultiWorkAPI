using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductGroups.Dto
{
    public class GetAllGroupInput
    {
        public ProductGroupStatus? Status { get; set; }
    }

    [AutoMapFrom(typeof(ProductGroup))]


    public class ProductGroupDto : EntityDto<long>, IHasCreationTime
    {
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public ProductGroupStatus Status { get; set; }
        public long CreatedUserId { get; set; }
        public long EditedUserId { get; set; }
    }
}
