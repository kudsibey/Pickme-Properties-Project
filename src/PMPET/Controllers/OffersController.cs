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
    public class OffersController : Controller
    {
        private readonly PMPETContext _context;

        public OffersController(PMPETContext context)
        {
            _context = context;    
        }

        // GET: Offers
        public async Task<IActionResult> Index()
        {
            var pMPETContext = _context.Offers.Include(o => o.Person).Include(o => o.RealEstate);
            return View(await pMPETContext.ToListAsync());
        }

        // GET: Offers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.Include(o => o.Person).Include(o => o.RealEstate).SingleOrDefaultAsync(m => m.ID == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // GET: Offers/Create
        public IActionResult Create()
        {
            ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "FirstMidName");
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "AddressLine1");
            return View();
        }

        // POST: Offers/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Notes,OfferAmount,OfferDate,OfferType,PersonID,RealEstateID")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "FirstMidName", offer.PersonID);
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "AddressLine1", offer.RealEstateID);
            return View(offer);
        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.SingleOrDefaultAsync(m => m.ID == id);
            if (offer == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "FirstMidName", offer.PersonID);
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "AddressLine1", offer.RealEstateID);
            return View(offer);
        }

        // POST: Offers/Edit/5
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Notes,OfferAmount,OfferDate,OfferType,PersonID,RealEstateID")] Offer offer)
        {
            if (id != offer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.ID))
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
            ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "FirstMidName", offer.PersonID);
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "AddressLine1", offer.RealEstateID);
            return View(offer);
        }

        // GET: Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.SingleOrDefaultAsync(m => m.ID == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _context.Offers.SingleOrDefaultAsync(m => m.ID == id);
            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OfferExists(int id)
        {
            return _context.Offers.Any(e => e.ID == id);
        }
    }
}
