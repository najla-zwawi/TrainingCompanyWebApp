using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingCompanyWebApp.Models.Entities
{
    public class Trainers:BaseEntity
    {
       
        public byte[] DataFile { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime RegDate { get; set; } = DateTime.Now;

        [Required]
        public String Specialization { get; set; }

      
        public string Facebook { get; set; }

      
        public string Email { get; set; }
    }
}
