using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZayiflamaMerkeziTakip.Models;

namespace ZayiflamaMerkeziTakip.Controllers
{
    [Authorize]
    public class OlcumlersController : Controller
    {
        private readonly ZayiflamaMerkeziTakipContext _context;

        public OlcumlersController(ZayiflamaMerkeziTakipContext context)
        {
            _context = context;
        }

        // GET: Olcumlers
        public async Task<IActionResult> Index()
        {
            var zayiflamaMerkeziTakipContext = _context.Olcumlers.Include(o => o.Hasta);
            return View(await zayiflamaMerkeziTakipContext.ToListAsync());
        }

        // GET: Olcumlers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var olcumler = await _context.Olcumlers
                .Include(o => o.Hasta)
                .FirstOrDefaultAsync(m => m.OlcumId == id);
            if (olcumler == null)
            {
                return NotFound();
            }

            return View(olcumler);
        }

        // GET: Olcumlers/Create
        public IActionResult Create()
        {
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim");
            return View();
        }

        // POST: Olcumlers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OlcumId,HastaId,Isim,OlcumTarihi,Kilo,Boy,Bmi,Notlar")] Olcumler olcumler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(olcumler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim", olcumler.HastaId);
            return View(olcumler);
        }

        // GET: Olcumlers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var olcumler = await _context.Olcumlers.FindAsync(id);
            if (olcumler == null)
            {
                return NotFound();
            }
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim", olcumler.HastaId);
            return View(olcumler);
        }

        // POST: Olcumlers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OlcumId,HastaId,Isim,OlcumTarihi,Kilo,Boy,Bmi,Notlar")] Olcumler olcumler)
        {
            if (id != olcumler.OlcumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(olcumler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OlcumlerExists(olcumler.OlcumId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim", olcumler.HastaId);
            return View(olcumler);
        }

        // GET: Olcumlers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var olcumler = await _context.Olcumlers
                .Include(o => o.Hasta)
                .FirstOrDefaultAsync(m => m.OlcumId == id);
            if (olcumler == null)
            {
                return NotFound();
            }

            return View(olcumler);
        }

        // POST: Olcumlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var olcumler = await _context.Olcumlers.FindAsync(id);
            if (olcumler != null)
            {
                _context.Olcumlers.Remove(olcumler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OlcumlerExists(int id)
        {
            return _context.Olcumlers.Any(e => e.OlcumId == id);
        }
    }
}
