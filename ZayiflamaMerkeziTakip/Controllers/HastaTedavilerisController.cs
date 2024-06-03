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
    public class HastaTedavilerisController : Controller
    {
        private readonly ZayiflamaMerkeziTakipContext _context;

        public HastaTedavilerisController(ZayiflamaMerkeziTakipContext context)
        {
            _context = context;
        }

        // GET: HastaTedavileris
        public async Task<IActionResult> Index()
        {
            var zayiflamaMerkeziTakipContext = _context.HastaTedavileris.Include(h => h.Hasta).Include(h => h.Tedavi);
            return View(await zayiflamaMerkeziTakipContext.ToListAsync());
        }

        // GET: HastaTedavileris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastaTedavileri = await _context.HastaTedavileris
                .Include(h => h.Hasta)
                .Include(h => h.Tedavi)
                .FirstOrDefaultAsync(m => m.HastaTedaviId == id);
            if (hastaTedavileri == null)
            {
                return NotFound();
            }

            return View(hastaTedavileri);
        }

        // GET: HastaTedavileris/Create
        public IActionResult Create()
        {
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim");
            ViewData["TedaviId"] = new SelectList(_context.Tedavilers, "TedaviId", "TedaviAdi");
            return View();
        }

        // POST: HastaTedavileris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HastaTedaviId,HastaId,Isim,TedaviAdi,TedaviId,BaslangicTarihi,BitisTarihi,Notlar")] HastaTedavileri hastaTedavileri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hastaTedavileri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim", hastaTedavileri.HastaId);
            ViewData["TedaviId"] = new SelectList(_context.Tedavilers, "TedaviId", "TedaviAdi", hastaTedavileri.TedaviId);
            return View(hastaTedavileri);
        }

        // GET: HastaTedavileris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastaTedavileri = await _context.HastaTedavileris.FindAsync(id);
            if (hastaTedavileri == null)
            {
                return NotFound();
            }
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim", hastaTedavileri.HastaId);
            ViewData["TedaviId"] = new SelectList(_context.Tedavilers, "TedaviId", "TedaviAdi", hastaTedavileri.TedaviId);
            return View(hastaTedavileri);
        }

        // POST: HastaTedavileris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HastaTedaviId,HastaId,TedaviId,Isim,TedaviAdi,BaslangicTarihi,BitisTarihi,Notlar")] HastaTedavileri hastaTedavileri)
        {
            if (id != hastaTedavileri.HastaTedaviId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hastaTedavileri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaTedavileriExists(hastaTedavileri.HastaTedaviId))
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
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim", hastaTedavileri.HastaId);
            ViewData["TedaviId"] = new SelectList(_context.Tedavilers, "TedaviId", "TedaviAdi", hastaTedavileri.TedaviId);
            return View(hastaTedavileri);
        }

        // GET: HastaTedavileris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastaTedavileri = await _context.HastaTedavileris
                .Include(h => h.Hasta)
                .Include(h => h.Tedavi)
                .FirstOrDefaultAsync(m => m.HastaTedaviId == id);
            if (hastaTedavileri == null)
            {
                return NotFound();
            }

            return View(hastaTedavileri);
        }

        // POST: HastaTedavileris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hastaTedavileri = await _context.HastaTedavileris.FindAsync(id);
            if (hastaTedavileri != null)
            {
                _context.HastaTedavileris.Remove(hastaTedavileri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaTedavileriExists(int id)
        {
            return _context.HastaTedavileris.Any(e => e.HastaTedaviId == id);
        }
    }
}
