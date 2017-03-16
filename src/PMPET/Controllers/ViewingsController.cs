using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMPET.Data;
using PMPET.Models;

namespace PMPET.Controllers
{
    public class ViewingsController : Controller
    {
        private readonly PMPETContext _context;

        public ViewingsController(PMPETContext context)
        {
            _context = context;
        }

        // GET: Viewings
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["ViewDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ViewDate_desc" : "";
            ViewData["FullNameSortParm"] = sortOrder == "FullName" ? "FullName_desc" : "FullName";
            ViewData["AddressLine1SortParm"] = sortOrder == "AddressLine1" ? "AddressLine1_desc" : "AddressLine1";

            ViewData["CurrentFilter"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                switch (sortOrder)
                {
                    case "AddressLine1_desc":
                        var pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderByDescending(v => v.RealEstate.AddressLine1)
                                                                                                                    .Where(r => r.RealEstate.AddressLine1.Contains(searchString));
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                    case "FullName_desc":
                        pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderByDescending(v => v.Person.FullName)
                                                                                                                    .Where(r => r.RealEstate.AddressLine1.Contains(searchString));
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                    case "FullName":
                        pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderBy(v => v.Person.FullName)
                                                                                                                    .Where(r => r.RealEstate.AddressLine1.Contains(searchString));
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                    case "ViewDate_desc":
                        pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderByDescending(v => v.ViewDate)
                                                                                                                    .Where(r => r.RealEstate.AddressLine1.Contains(searchString));
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                    default:
                        pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderBy(v => v.ViewDate)
                                                                                                                    .Where(r => r.RealEstate.AddressLine1.Contains(searchString));
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                }
            }
            else 
            {
                switch (sortOrder)
                {
                    case "AddressLine1_desc":
                        var pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderByDescending(v => v.RealEstate.AddressLine1);
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                    case "FullName_desc":
                        pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderByDescending(v => v.Person.FullName);
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                    case "FullName":
                        pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderBy(v => v.Person.FullName);
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                    case "ViewDate_desc":
                        pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderByDescending(v => v.ViewDate);
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                    default:
                        pMPETContext = _context.Viewings.Include(v => v.Person).Include(l => l.RealEstate).OrderBy(v => v.ViewDate);
                        return View(await pMPETContext.AsNoTracking().ToListAsync());
                }
            }
        }






        // GET: Viewings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
            var viewing = await _context.Viewings
                           .Include(r => r.RealEstate)
                           .Include(p => p.Person)
                           .AsNoTracking()
                           .SingleOrDefaultAsync(m => m.ID == id);
            if (viewing == null)
            {
                return NotFound();
            }

            return View(viewing);
        }

        // GET: Viewings/Create
        public IActionResult Create()
        {
            ViewData["ViewingTypes"] = new SelectList(new[] { "Single", "Block"});
            ViewData["Members"] = new SelectList(_context.Persons, "ID","FullName");
            ViewData["RealEstateIDs"] = new SelectList(_context.RealEstates, "ID","AddressLine1");
            return View();
        }

        // POST: Viewings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Notes,PersonID,RealEstateID,ViewDate,ViewTime,ViewingType")] Viewing viewing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewing);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ViewingTypes"] = new SelectList(new[] { "Single", "Block" });
            ViewData["Members"] = new SelectList(_context.Persons, "ID", "FullName");
            ViewData["RealEstateIDs"] = new SelectList(_context.RealEstates, "ID", "AddressLine1");
            return View(viewing);
        }

        // GET: Viewings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings.SingleOrDefaultAsync(m => m.ID == id);
            if (viewing == null)
            {
                return NotFound();
            }
            ViewData["ViewingTypes"] = new SelectList(new[] { "Single", "Block" });
            ViewData["Members"] = new SelectList(_context.Persons, "ID", "FullName");
            ViewData["RealEstateIDs"] = new SelectList(_context.RealEstates, "ID", "AddressLine1");
            return View(viewing);
        }

        // POST: Viewings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Notes,PersonID,RealEstateID,ViewDate,ViewTime,ViewingType")] Viewing viewing)
        {
            if (id != viewing.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViewingExists(viewing.ID))
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
            ViewData["ViewingTypes"] = new SelectList(new[] { "Single", "Block" });
            ViewData["Members"] = new SelectList(_context.Persons, "ID", "FullName");
            ViewData["RealEstateIDs"] = new SelectList(_context.RealEstates, "ID", "AddressLine1");
            return View(viewing);
        }

        // GET: Viewings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var viewing = await _context.Viewings
                           .Include(r => r.RealEstate)
                           .Include(p => p.Person)
                           .AsNoTracking()
                           .SingleOrDefaultAsync(m => m.ID == id);
            if (viewing == null)
            {
                return NotFound();
            }

            return View(viewing);
        }

        // POST: Viewings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewing = await _context.Viewings.SingleOrDefaultAsync(m => m.ID == id);
            _context.Viewings.Remove(viewing);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ViewingExists(int id)
        {
            return _context.Viewings.Any(e => e.ID == id);
        }
    }
}
