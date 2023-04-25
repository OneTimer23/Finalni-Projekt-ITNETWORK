using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FinalFinal.Controllers
{
    [Authorize]
    public class PojistenisController : Controller
    {
        private readonly ProjektItnetworkContext _context;

        public PojistenisController(ProjektItnetworkContext context)
        {
            _context = context;
        }

        
        // GET: Pojistenis
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity.Name;
            var userid = _context.Users.Where(p => p.UserName == username).First().Id;
            ViewBag.userid = userid;
            var projektItnetworkContext = _context.Pojistenis.Include(p => p.Uzivatel);
            
            return View(await projektItnetworkContext.ToListAsync());
        }

        // GET: Pojistenis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var username = HttpContext.User.Identity.Name;
            var userid = _context.Users.Where(p => p.UserName == username).First().Id;
            ViewBag.userid = userid;

            if (id == null || _context.Pojistenis == null)
            {
                return NotFound();
            }

            var pojisteni = await _context.Pojistenis
                .Include(p => p.Uzivatel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pojisteni == null)
            {
                return NotFound();
            }

            return View(pojisteni);
        }

        // GET: Pojistenis/Create
        public IActionResult Create(int UserId)
        {
            var v = new Models.Pojisteni() { UzivatelId = UserId, PlatnostOd = DateTime.Today, PlatnostDo = DateTime.Today };

            ViewData["UzivatelId"] = new SelectList(_context.Kontakts, "Id", "PrijmeniPlusJmeno");
            
            return View(v);
        }

        // POST: Pojistenis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypPojistky,PojistenyPredmet,Hodnota,PlatnostOd,PlatnostDo, UzivatelId")] Pojisteni pojisteni)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(pojisteni);
                var username = HttpContext.User.Identity.Name;
                var userid = _context.Users.Where(p => p.UserName == username).First().Id;
                pojisteni.ZakladatelId = userid;
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Kontakts", new { id = pojisteni.UzivatelId });
            }
            ViewData["UzivatelId"] = new SelectList(_context.Kontakts, "Id", "Id", pojisteni.UzivatelId);
            return View(pojisteni);
        }

        // GET: Pojistenis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pojistenis == null)
            {
                return NotFound();
            }

            var pojisteni = await _context.Pojistenis.FindAsync(id);
            if (pojisteni == null)
            {
                return NotFound();
            }
            ViewData["UzivatelId"] = new SelectList(_context.Kontakts, "Id", "Id", pojisteni.UzivatelId);
            return View(pojisteni);
        }

        // POST: Pojistenis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypPojistky,PojistenyPredmet,Hodnota,PlatnostOd,PlatnostDo,UzivatelId, ZakladatelId")] Pojisteni pojisteni)
        {
            if (id != pojisteni.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var username = HttpContext.User.Identity.Name;
                    var userid = _context.Users.Where(p => p.UserName == username).First().Id;
                    if(pojisteni.ZakladatelId == null)
                        {
                        pojisteni.ZakladatelId = userid;

                    }
                    _context.Update(pojisteni);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PojisteniExists(pojisteni.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Kontakts", new { id = pojisteni.UzivatelId });
            }
            ViewData["UzivatelId"] = new SelectList(_context.Kontakts, "Id", "Id", pojisteni.UzivatelId);
            return View(pojisteni);
        }

        // GET: Pojistenis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pojistenis == null)
            {
                return NotFound();
            }

            var pojisteni = await _context.Pojistenis
                .Include(p => p.Uzivatel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pojisteni == null)
            {
                return NotFound();
            }

            return View(pojisteni);
        }

        // POST: Pojistenis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pojistenis == null)
            {
                return Problem("Entity set 'ProjektItnetworkContext.Pojistenis'  is null.");
            }
            var pojisteni = await _context.Pojistenis.FindAsync(id);
            if (pojisteni != null)
            {
                _context.Pojistenis.Remove(pojisteni);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Kontakts", new { id = pojisteni.UzivatelId });
        }

        private bool PojisteniExists(int id)
        {
          return _context.Pojistenis.Any(e => e.Id == id);
        }
    }
}
