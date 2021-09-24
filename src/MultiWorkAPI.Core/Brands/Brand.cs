using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands
{
    [Table("Brand")]
    public class Brand: Entity<long>, IHasCreationTime
    {
        [Required]
        public string Title { get; set; }

        public DateTime CreationTime { get; set; }

        public BrandStatus Status { get; set; }
        [Required]
        public long CreatedUserId { get; set; }
        [Required]
        public long EditedUserId { get; set; }

        public Brand()
        {
            CreationTime = Clock.Now;
            Status = BrandStatus.Waiting;
        }

        public Brand(string title)
            : this()
        {
            Title = title;
        }
    }
    public enum BrandStatus : byte
    {
        Waiting = 0,
        Accepted = 1,
        Rejected = 2
    }
}
