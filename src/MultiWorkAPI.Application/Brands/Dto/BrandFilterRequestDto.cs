using MultiWorkAPI.Base.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands.Dto
{
    public class BrandFilterRequestDto : BaseFilterRequestDto
    {
        public long ProductGroupId { get; set; }
    }
}
