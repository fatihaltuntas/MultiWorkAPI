using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductGroups.Dto
{
    [AutoMapFrom(typeof(ProductGroup))]
    public class UpdateProductGroupDto : Entity<long>, IHasCreationTime
    {
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public ProductGroupStatus Status { get; set; }
        public long CreatedUserId { get; set; }
        public long EditedUserId { get; set; }
    }
}
