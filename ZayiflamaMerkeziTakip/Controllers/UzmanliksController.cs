﻿using System;
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
    public class UzmanliksController : Controller
    {
        private readonly ZayiflamaMerkeziTakipContext _context;

        public UzmanliksController(ZayiflamaMerkeziTakipContext context)
        {
            _context = context;
        }

        // GET: Uzmanliks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Uzmanliks.ToListAsync());
        }

        // GET: Uzmanliks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzmanlik = await _context.Uzmanliks
                .FirstOrDefaultAsync(m => m.UzmanlikId == id);
            if (uzmanlik == null)
            {
                return NotFound();
            }

            return View(uzmanlik);
        }

        // GET: Uzmanliks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uzmanliks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UzmanlikId,Uzmanlik1")] Uzmanlik uzmanlik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uzmanlik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uzmanlik);
        }

        // GET: Uzmanliks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzmanlik = await _context.Uzmanliks.FindAsync(id);
            if (uzmanlik == null)
            {
                return NotFound();
            }
            return View(uzmanlik);
        }

        // POST: Uzmanliks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UzmanlikId,Uzmanlik1")] Uzmanlik uzmanlik)
        {
            if (id != uzmanlik.UzmanlikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uzmanlik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UzmanlikExists(uzmanlik.UzmanlikId))
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
            return View(uzmanlik);
        }

        // GET: Uzmanliks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzmanlik = await _context.Uzmanliks
                .FirstOrDefaultAsync(m => m.UzmanlikId == id);
            if (uzmanlik == null)
            {
                return NotFound();
            }

            return View(uzmanlik);
        }

        // POST: Uzmanliks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uzmanlik = await _context.Uzmanliks.FindAsync(id);
            if (uzmanlik != null)
            {
                _context.Uzmanliks.Remove(uzmanlik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UzmanlikExists(int id)
        {
            return _context.Uzmanliks.Any(e => e.UzmanlikId == id);
        }
    }
}
