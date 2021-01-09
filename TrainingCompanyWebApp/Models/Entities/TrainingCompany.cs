using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingCompanyWebApp.Models.Entities
{
    public class TrainingCompany:BaseEntity 
    {

        public byte[] Picture { get; set; }

        [Required]
        public string Name { get; set; }
       
        [Required ]
        public string Specialization { get; set; }

        [Required]
        public string Description { get; set; }

     

    }
}
