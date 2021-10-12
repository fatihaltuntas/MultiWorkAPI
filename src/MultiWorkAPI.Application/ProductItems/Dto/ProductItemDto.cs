using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MultiWorkAPI.ProductItems.ProductItem;

namespace MultiWorkAPI.ProductItems.Dto


{
    public class GetAllProductItemInput
    {
        public ProductItemStatus? Status { get; set; }
    }
    [AutoMapFrom(typeof(ProductItem))]
    public class ProductItemDto:EntityDto<long>,IHasCreationTime
    {
        public string Title { get; set; }
        public string Barcode { get; set; }   
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        [Required]
        public ProductItemStatus Status { get; set; }     
        public long CreationUserId { get; set; }
        public long EditedUserId { get; set; }
        [Required]
        public long BrandId { get; set; }
        public string BrandName { get; set; }
        [Required]
        public long ModelId { get; set; }
        public string ModelName { get; set; }
        [Required]
        public long ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }
        [Required]
        public long UserId { get; set; }
        public string Note { get; set; }

        
    }
}
