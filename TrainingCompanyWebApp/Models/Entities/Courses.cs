using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingCompanyWebApp.Models.Entities
{
    public class Courses:BaseEntity 
    {
        public byte[] Picture { get; set; }

        [Required ]
        public int Code { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        public DateTime RegDate { get; set; } = DateTime.Now;

        [Required]
        public String Details { get; set; }

        [Required]
        public float CourseFee { get; set; }

        [Required]
        public int HourNum { get; set; }

        [Required]
        public int Seate { get; set; }

        public Trainers Trainer { get; set; }

    }
}
