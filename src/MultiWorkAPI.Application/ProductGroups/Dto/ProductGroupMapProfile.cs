using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductGroups.Dto
{
    public class ProductGroupMapProfile:Profile
    {
        public ProductGroupMapProfile()
        {
            CreateMap<ProductGroupDto, ProductGroup>();
        }
    }
}
