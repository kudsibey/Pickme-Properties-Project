using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMPET.Data;
using PMPET.Models;

namespace PMPET
{
    public class ServicesController : Controller
    {
        private readonly PMPETContext _context;

        public ServicesController(PMPETContext context)
        {
            _context = context;    
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var service = await _context.Services
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["ServiceTypes"] = new SelectList(new[] { "Sale", "Let", "Sale&Let"});
            ViewData["FeeTypes"] = new SelectList(new[] { "Commission", "Fixed Fee"});
            return View();
        }

        // POST: Services/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,RentCommision,SaleCommission,ServiceType,FeeType,FixedSaleFee,FixedLetFee,StartDate,EndDate")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FeeTypes"] = new SelectList(new[] { "Commission", "Fixed Fee" });
            ViewData["ServiceTypes"] = new SelectList(new[] { "Sale", "Let", "Sale&Let" }); 
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.SingleOrDefaultAsync(m => m.ID == id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["FeeTypes"] = new SelectList(new[] { "Commission", "Fixed Fee" });
            ViewData["ServiceTypes"] = new SelectList(new[] { "Sale", "Let", "Sale&Let" });
            return View(service);
        }

        // POST: Services/Edit/5
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,RentCommision,SaleCommission,ServiceType,FeeType,FixedSaleFee,FixedLetFee,StartDate,EndDate")] Service service)
        {
            if (id != service.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ID))
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
            ViewData["FeeTypes"] = new SelectList(new[] { "Commission", "Fixed Fee" });
            ViewData["ServiceTypes"] = new SelectList(new[] { "Sale", "Let", "Sale&Let" });
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.SingleOrDefaultAsync(m => m.ID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.SingleOrDefaultAsync(m => m.ID == id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ID == id);
        }
    }
}
