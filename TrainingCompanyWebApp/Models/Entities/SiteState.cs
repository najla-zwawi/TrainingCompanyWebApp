using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingCompanyWebApp.Controllers;

namespace TrainingCompanyWebApp.Models.Entities
{
    public class SiteState:BaseEntity
    {
        public bool SystemOff { get; set; }
        public string SystemOffMessage { get; set; }
       
    }
}
