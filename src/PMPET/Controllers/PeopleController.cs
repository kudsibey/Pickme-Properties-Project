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

namespace PMPET.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PMPETContext _context;

        public PeopleController(PMPETContext context)
        {
            _context = context;    
        }

        // GET: People
        public async Task<IActionResult> IndexBck()
        {
            return View(await _context.Persons.ToListAsync());
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            //ViewData["JoinDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "joinDate_desc" : "joinDate_asc";
            ViewData["JoinDateSortParm"] = sortOrder == "joinDate_desc" ? "joinDate_asc": "joinDate_desc";
            ViewData["FirstMidNameSortParm"] = sortOrder == "FirstMidName" ? "firstMidName_desc" : "FirstMidName";
            ViewData["CurrentFilter"] = searchString;

            var persons = from p in _context.Persons
                              select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                persons = persons.Where(r => r.FirstMidName.Contains(searchString)
                                       || r.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "joinDate_desc":
                    persons = persons.OrderByDescending(r => r.JoinDate);
                    break;
                case "joinDate_asc":
                    persons = persons.OrderBy(r => r.JoinDate);
                    break;
                case "FirstMidName":
                    persons = persons.OrderBy(r => r.FirstMidName);
                    break;
                case "firstMidName_desc":
                    persons = persons.OrderByDescending(r => r.FirstMidName);
                    break;
                default:
                    persons = persons.OrderBy(r => r.FirstMidName);
                    break;
            }
            return View(await persons.AsNoTracking().ToListAsync());
        }


        // GET: People/Details/5

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(r => r.RealEstates)
                .Include(v => v.Viewings)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["PersonTypes"] = new SelectList(new[] { "Buyer", "Seller", "Landlord","Tenant","Viewer"});
            return View();
        }

        // POST: People/Create
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AddressLine1,AddressLine2,FirstMidName,JoinDate,LastName,MemberType,PostCode,Town, TelephoneNo,MobileNo,EmailAddress,GeneralNotes")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PersonTypes"] = new SelectList(new[] { "Buyer", "Seller", "Landlord", "Tenant", "Viewer" });
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.SingleOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["PersonTypes"] = new SelectList(new[] { "Buyer", "Seller", "Landlord", "Tenant", "Viewer" });
            return View(person);
        }

        // POST: People/Edit/5
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AddressLine1,AddressLine2,FirstMidName,JoinDate,LastName,MemberType,PostCode,Town,TelephoneNo,MobileNo,EmailAddress,GeneralNotes")] Person person)
        {
            if (id != person.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.ID))
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
            ViewData["PersonTypes"] = new SelectList(new[] { "Buyer", "Seller", "Landlord", "Tenant", "Viewer" });
            return View(person);
        }

        public IActionResult CantDelete(Person person)
        {
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.SingleOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(m => m.ID == id);
            _context.Persons.Remove(person);
            try { await _context.SaveChangesAsync(); }
            catch { return RedirectToAction("CantDelete", new RouteValueDictionary(person)); }
            return RedirectToAction("Index");
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.ID == id);
        }
    }
}
