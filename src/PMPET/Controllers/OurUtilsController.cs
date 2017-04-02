using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PMPET.Controllers
{
    public class OurUtilsController : Controller
    {
        private IHostingEnvironment _environment;

        public OurUtilsController(IHostingEnvironment environment)
        {
            _environment = environment;
        }


        public IActionResult UploadPics(string filename)
        {
            ViewBag.CurrentPropAddress = filename;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPics(ICollection<IFormFile> files,string targetfilename)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    //                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    using (var fileStream = new FileStream(Path.Combine(uploads, targetfilename), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}

   
    

