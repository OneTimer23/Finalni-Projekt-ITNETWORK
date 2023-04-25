using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace FinalFinal.Controllers
{

   

    [Authorize]
    public class KontaktsController : Controller
    {
        private readonly ProjektItnetworkContext _context;

        public KontaktsController(ProjektItnetworkContext context)
        {
            _context = context;
        }


        // GET: Kontakts

        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity.Name;
            var userid = _context.Users.Where(p => p.UserName == username).First().Id;
            var seznam = _context.Kontakts.Where(p => p.ZakladatelId == userid);
            if (User.IsInRole("admin"))
            {
                seznam = _context.Kontakts;
            }
            
            
              return View(await seznam.ToListAsync());
        }

        // GET: Kontakts/Details/5
     

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kontakts == null)
            {
                return NotFound();
            }

            var v = new KontaktDetail();
            
            v.Record = await _context.Kontakts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (v.Record == null)
            {
                return NotFound();
            }

            
            v.VypisPojisteni = _context.Pojistenis.Where(p => p.UzivatelId==id);
            
            return View(v);
        }
        
        // GET: Kontakts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kontakts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Jmeno,Prijmeni,SmerovaciCislo,Ulice,Mesto,TelCislo,Email")] Kontakt kontakt)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.User.Identity.Name;
                var userid = _context.Users.Where(p => p.UserName == username).First().Id;

                kontakt.ZakladatelId = userid;
                _context.Add(kontakt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kontakt);
        }

        // GET: Kontakts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kontakts == null)
            {
                return NotFound();
            }

            var kontakt = await _context.Kontakts.FindAsync(id);
            if (kontakt == null)
            {
                return NotFound();
            }
            return View(kontakt);
        }

        // POST: Kontakts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Jmeno,Prijmeni,SmerovaciCislo,Ulice,Mesto,TelCislo,Email")] Kontakt kontakt)
        {
            if (id != kontakt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kontakt);
                    var username = HttpContext.User.Identity.Name;
                    var userid = _context.Users.Where(p => p.UserName == username).First().Id;

                    kontakt.ZakladatelId = userid;
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KontaktExists(kontakt.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Kontakts", new { id = kontakt.Id });
            }
            return View(kontakt);
        }

        // GET: Kontakts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kontakts == null)
            {
                return NotFound();
            }

            var kontakt = await _context.Kontakts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kontakt == null)
            {
                return NotFound();
            }

            return View(kontakt);
        }

        // POST: Kontakts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kontakts == null)
            {
                return Problem("Entity set 'ProjektItnetworkContext.Kontakts'  is null.");
            }
            var kontakt = await _context.Kontakts.FindAsync(id);
            var pojistky = _context.Pojistenis.Where(p => p.UzivatelId == id);
            if (pojistky.Count() > 0)
            {
                ViewBag.hlaska = "Nelze odstranit, protože jsou sjednaná pojištění.";
                return View(kontakt);
            }
            if (kontakt != null)
            {
                _context.Kontakts.Remove(kontakt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KontaktExists(int id)
        {
          return _context.Kontakts.Any(e => e.Id == id);
        }
    }
}
