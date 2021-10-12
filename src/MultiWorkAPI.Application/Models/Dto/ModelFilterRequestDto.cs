using MultiWorkAPI.Base.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Brands.Dto
{
    public class ModelFilterRequestDto : BaseFilterRequestDto
    {
        public long BrandId { get; set; }
    }
}
