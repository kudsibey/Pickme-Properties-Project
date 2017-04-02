using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMPET.Data;
using PMPET.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace PMPET.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly PMPETContext _context;

        //public RealEstatesController(PMPETContext context)
        //{
        //    _context = context;    
        //}


        private IHostingEnvironment _environment;

        public RealEstatesController(IHostingEnvironment environment, PMPETContext context)
        {
            _environment = environment;
            _context = context;
        }


        public IActionResult UploadPics(string filename)
        {
            ViewBag.CurrentPropAddress = filename;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPics(ICollection<IFormFile> files, string targetfilename)
        {
            if (targetfilename != null && files!=null )
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        using (var fileStream = new FileStream(Path.Combine(uploads, targetfilename.ToUpper().Replace(" ","")), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                    }

                }
            } else
            {
                
                return View("Index",await _context.RealEstates.ToListAsync());
            } 

            return View(UploadPics(targetfilename));
        }




        // GET: RealEstates
        public async Task<IActionResult> IndexBck()
        {
            return View(await _context.RealEstates.ToListAsync());
        }

        public async Task<IActionResult> Index(string sortOrder,string searchString)
        {
            ViewData["PostCodeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "postCode_desc" : "";
            ViewData["SalePriceSortParm"] = sortOrder == "SalePrice" ? "salePrice_desc" : "SalePrice";
            ViewData["CurrentFilter"] = searchString;

            var realEstates = from r in _context.RealEstates
                           select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                realEstates = realEstates.Where(r => r.AddressLine1.Contains(searchString)
                                       || r.AddressLine2.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "postCode_desc":
                    realEstates = realEstates.OrderByDescending(r => r.PostCode);
                    break;
                case "SalePrice":
                    realEstates = realEstates.OrderBy(r => r.SalePrice);
                    break;
                case "salePrice_desc":
                    realEstates = realEstates.OrderByDescending(r => r.SalePrice);
                    break;
                default:
                    realEstates = realEstates.OrderBy(r => r.PostCode);
                    break;
            }
            return View(await realEstates.AsNoTracking().ToListAsync());
        }


        // GET: RealEstates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var realEstate = await _context.RealEstates
              .Include(r => r.Person)
              .Include(s => s.Service)
              .Include(v => v.Viewings)
              .AsNoTracking()
              .SingleOrDefaultAsync(m => m.ID == id);

            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }

        

        // GET: RealEstates/Create
        public IActionResult Create()
        {
            ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "FullName");
            ViewData["ServiceID"] = new SelectList(_context.Services, "ID", "Description");
            return View();
        }

        // POST: RealEstates/Create
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
              [Bind("ID,AddressLine1,AddressLine2,PersonID,ServiceID,PostCode,RentalPrice,SalePrice,Town,PropertyType,BedroomQty,BathroomWCQty,BathroomQty,WCQty,FrontGarden,BackGarden,GenDescription")]
              RealEstate realEstate)
        {
            if (ModelState.IsValid)
            //if (true)
            {
                _context.Add(realEstate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "FullName");
            ViewData["ServiceID"] = new SelectList(_context.Services, "ID", "ID");
            return View(realEstate);
        }

        // GET: RealEstates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstates.SingleOrDefaultAsync(m => m.ID == id);
            if (realEstate == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "FullName");
            ViewData["ServiceID"] = new SelectList(_context.Services, "ID", "ID");
            return View(realEstate);
        }

        // POST: RealEstates/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ID,AddressLine1,AddressLine2,PersonID,ServiceID,PostCode,RentalPrice,SalePrice,Town,PropertyType,BedroomQty,BathroomWCQty,BathroomQty,WCQty,FrontGarden,BackGarden,GenDescription")]
             RealEstate realEstate)
        {
            if (id != realEstate.ID)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            //if (true)
            {
                try
                {
                    _context.Update(realEstate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealEstateExists(realEstate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "FullName");
            ViewData["ServiceID"] = new SelectList(_context.Services, "ID", "ID");
            return View(realEstate);
        }

        // GET: RealEstates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstates.SingleOrDefaultAsync(m => m.ID == id);
            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }

        public IActionResult CantDelete(RealEstate realEstate)
        {
            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }


        // POST: RealEstates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var realEstate = await _context.RealEstates.SingleOrDefaultAsync(m => m.ID == id);
            _context.RealEstates.Remove(realEstate);
            try { await _context.SaveChangesAsync(); }
            catch { return RedirectToAction("CantDelete", new RouteValueDictionary(realEstate)); }
            return RedirectToAction("Index");
        }


        private bool RealEstateExists(int id)
        {
            return _context.RealEstates.Any(e => e.ID == id);
        }
    }
}
