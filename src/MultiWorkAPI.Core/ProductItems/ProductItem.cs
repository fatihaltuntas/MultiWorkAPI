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

namespace MultiWorkAPI.ProductItems
{
    [Table("ProductItem")]
    public class ProductItem : Entity<long>, IHasCreationTime
    {

        public string Title { get; set; }
        public string Barcode { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public ProductItemStatus Status { get; set; }
        [Required]
        public long CreationUserId { get; set; }
        [Required]
        public long EditedUserId { get; set; }
        [Required]
        public long BrandId { get; set; }
        [Required]
        public long ModelId { get; set; }
        [Required]
        public long ProductGroupId { get; set; }
        public long UserId { get; set; }
        public string Note { get; set; }
        public ProductItem()
        {
            CreationTime = Clock.Now;
            Status = ProductItemStatus.Stock;
        }
        public ProductItem(string title) : this()
        {
            Title = title;

        }
        public enum ProductItemStatus:byte
        {
            Stock=1, //stok, Mevcut
            Maintenance=2, //servis, bakım, tamir
            Junk=3,  //Hurda, Döküntü
            Repository=4 //Depo
        }

    }
}
