using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.Base.Dto
{
    public class BaseFilterRequestDto : PagedResultRequestDto
    {
        public long Status { get; set; }
        public string SearchWord { get; set; }
    }
}
