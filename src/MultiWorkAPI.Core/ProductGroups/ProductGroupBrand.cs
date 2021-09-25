using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MultiWorkAPI.Brands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductGroups
{
    [Table("ProductGroupBrand")]
    public class ProductGroupBrand:Entity<long>,IHasCreationTime
    {
        [Required]
        public long ProductGroupId { get; set; }
        [Required]
        public long BrandId { get; set; }
        [Required]
        public long CreatedUserId { get; set; }
        public long EditedUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public ProductGroupBrandStatus Status { get; set; }
    }
    public enum ProductGroupBrandStatus : byte
    {
        Waiting = 0,
        Accepted = 1,
        Rejected = 2
    }
}