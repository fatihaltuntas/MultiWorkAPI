using MultiWorkAPI.Base.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands.Dto
{
    public class ProductItemFilterRequestDto : BaseFilterRequestDto
    {
        public long ProductGroupId { get; set; }
        public long BrandId { get; set; }
        public long ModelId { get; set; }
        public long UserId { get; set; }
    }
}
