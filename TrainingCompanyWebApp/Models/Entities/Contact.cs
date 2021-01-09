using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingCompanyWebApp.Models.Entities
{
    public class Contact:BaseEntity 
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Facebook { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
