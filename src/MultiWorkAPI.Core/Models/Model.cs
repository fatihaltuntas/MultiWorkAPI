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

namespace MultiWorkAPI.Models
{
   [Table("Model")]
    public class Model:Entity<long>,IHasCreationTime
    {
        [Required]
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public ModelStatus Status { get; set; }
        [Required]
        public long  CreatedUserId { get; set; }
        [Required]
        public long EditedUserId { get; set; }
        [Required]
        public long BrandId { get; set; }
        public Model()
        {
            CreationTime = Clock.Now;
            Status = ModelStatus.Waiting;
        }
        public Model(string title)
            :this()
        {
            Title = title;
        }
        public enum ModelStatus:byte
        {
            Waiting=1,
            Accepted=2,
            Rejected=3
        }
    }
}
