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
    public class TedavilersController : Controller
    {
        private readonly ZayiflamaMerkeziTakipContext _context;

        public TedavilersController(ZayiflamaMerkeziTakipContext context)
        {
            _context = context;
        }

        // GET: Tedavilers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tedavilers.ToListAsync());
        }

        // GET: Tedavilers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedaviler = await _context.Tedavilers
                .FirstOrDefaultAsync(m => m.TedaviId == id);
            if (tedaviler == null)
            {
                return NotFound();
            }

            return View(tedaviler);
        }

        // GET: Tedavilers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tedavilers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TedaviId,TedaviAdi,Aciklama,Ucret")] Tedaviler tedaviler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tedaviler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tedaviler);
        }

        // GET: Tedavilers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedaviler = await _context.Tedavilers.FindAsync(id);
            if (tedaviler == null)
            {
                return NotFound();
            }
            return View(tedaviler);
        }

        // POST: Tedavilers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TedaviId,TedaviAdi,Aciklama,Ucret")] Tedaviler tedaviler)
        {
            if (id != tedaviler.TedaviId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tedaviler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TedavilerExists(tedaviler.TedaviId))
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
            return View(tedaviler);
        }

        // GET: Tedavilers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedaviler = await _context.Tedavilers
                .FirstOrDefaultAsync(m => m.TedaviId == id);
            if (tedaviler == null)
            {
                return NotFound();
            }

            return View(tedaviler);
        }

        // POST: Tedavilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tedavilers == null)
            {
                return Problem("Entity set 'YourDbContext.Tedavilers' is null.");
            }

            var tedaviler = await _context.Tedavilers.FindAsync(id);
            if (tedaviler != null)
            {
                _context.Tedavilers.Remove(tedaviler);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Hata mesajı ekle
                ViewBag.messageString = "Tedavi silinemedi. İlişkili Hasta kayıtları olabilir.";
                return View("Information");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TedavilerExists(int id)
        {
            return _context.Tedavilers.Any(e => e.TedaviId == id);
        }

    }
}
