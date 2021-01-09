using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingCompanyWebApp.Models.Entities;
using TrainingCompanyWebApp.Models.Interfaces;

namespace TrainingCompanyWebApp.Controllers
{
    public class BaseController : Controller
    {
      

        public byte[] FileToByte(IFormFile file)
        {
            var target = new MemoryStream();
            file.CopyTo(target);
            return (target.ToArray());
        }

        public bool CheckImgExtension(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName.ToLower());
            string[] validExtensions = { ".jpeg", ".jpg", ".bmp", ".gif", ".png", ".tiff", ".ico" };
            if (validExtensions.Contains(fileExtension))
                return true;
            else
                return false;
        }

        
    }
}
