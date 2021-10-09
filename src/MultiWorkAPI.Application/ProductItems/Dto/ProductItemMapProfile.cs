using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductItems.Dto
{
   public class ProductItemMapProfile:Profile
    {
        public ProductItemMapProfile()
        {
            CreateMap<ProductItemDto, ProductItem>();
        }
    }
}
