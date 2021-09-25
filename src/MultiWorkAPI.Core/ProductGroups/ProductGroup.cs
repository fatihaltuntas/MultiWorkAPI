using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductGroups
{
    [Table("ProductGroup")]
    public class ProductGroup: Entity<long>,IHasCreationTime
    {
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public ProductGroupStatus Status { get; set; }
        public long CreatedUserId { get; set; }
        public long EditedUserId { get; set; }

        public ProductGroup()
        {
            CreationTime = Clock.Now;
            Status = ProductGroupStatus.Waiting;
        }
        public ProductGroup(string title)
            : this()
        {
            Title = title;
        }
    }
}

public enum ProductGroupStatus : byte
{
    Waiting = 0,
    Accepted = 1,
    Rejected = 2
}
