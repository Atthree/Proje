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
    public class HastalarsController : Controller
    {
        private readonly ZayiflamaMerkeziTakipContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public HastalarsController(ZayiflamaMerkeziTakipContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Hastalars
        public async Task<IActionResult> Index()
        {
            var zayiflamaMerkeziTakipContext = _context.Hastalars.Include(h => h.Tedavi);
            return View(await zayiflamaMerkeziTakipContext.ToListAsync());
        }

        // GET: Hastalars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastalar = await _context.Hastalars
                .Include(h => h.Tedavi)
                .FirstOrDefaultAsync(m => m.HastaId == id);
            if (hastalar == null)
            {
                return NotFound();
            }

            return View(hastalar);
        }

        // GET: Hastalars/Create
        public IActionResult Create()
        {
            ViewData["TedaviId"] = new SelectList(_context.Tedavilers, "TedaviId", "TedaviAdi");
            return View();
        }

        // POST: Hastalars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HastaId,Isim,Soyisim,DogumTarihi,Cinsiyet,TelefonNo,Photo,ImageFile,TedaviAdi,KayitTarihi,TedaviId")] Hastalar hastalar)
        {
            if (ModelState.IsValid)
            {
                if (hastalar.ImageFile != null)
                {
                    string wwwrootpath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(hastalar.ImageFile.FileName);
                    string extension = Path.GetExtension(hastalar.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    hastalar.Photo = "/contents/" + fileName;
                    string path = Path.Combine(wwwrootpath, "contents", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await hastalar.ImageFile.CopyToAsync(filestream);
                    }
                }
                else
                {
                    // Varsayılan fotoğrafı ayarla
                    hastalar.Photo = "/contents/default.jpg";
                }

                _context.Add(hastalar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TedaviId"] = new SelectList(_context.Tedavilers, "TedaviId", "TedaviAdi");
            return View(hastalar);
        }

        // GET: Hastalars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastalar = await _context.Hastalars.FindAsync(id);
            if (hastalar == null)
            {
                return NotFound();
            }

            // Mevcut fotoğraf yolunu TempData'ya saklayın
            TempData["MevcutFotoPath"] = hastalar.Photo;

            ViewData["TedaviId"] = new SelectList(_context.Tedavilers, "TedaviId", "TedaviAdi", hastalar.TedaviId);
            return View(hastalar);
        }


        // POST: Hastalars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HastaId,Isim,Soyisim,DogumTarihi,Cinsiyet,TelefonNo,Photo,ImageFile,TedaviAdi,KayitTarihi,TedaviId")] Hastalar hastalar)
        {
            if (id != hastalar.HastaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TempData'dan mevcut fotoğraf yolunu alın
                    string mevcutFotoPath = TempData["MevcutFotoPath"] as string;

                    if (hastalar.ImageFile != null)
                    {
                        string wwwrootpath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(hastalar.ImageFile.FileName);
                        string extension = Path.GetExtension(hastalar.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        hastalar.Photo = "/contents/" + fileName;
                        string path = Path.Combine(wwwrootpath, "contents", fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await hastalar.ImageFile.CopyToAsync(filestream);
                        }
                    }
                    else
                    {
                        // Eğer yeni bir fotoğraf yüklenmemişse mevcut fotoğrafı kullanın
                        hastalar.Photo = mevcutFotoPath;
                    }

                    _context.Update(hastalar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastalarExists(hastalar.HastaId))
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

            ViewData["TedaviId"] = new SelectList(_context.Tedavilers, "TedaviId", "TedaviAdi", hastalar.TedaviId);
            return View(hastalar);
        }

        // GET: Hastalars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastalar = await _context.Hastalars
                .Include(h => h.Tedavi)
                .FirstOrDefaultAsync(m => m.HastaId == id);
            if (hastalar == null)
            {
                return NotFound();
            }

            return View(hastalar);
        }

        // POST: Hastalars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hastalars == null)
            {
                return Problem("Entity set 'YourDbContext.Hastalars' is null.");
            }

            var hastalar = await _context.Hastalars.FindAsync(id);
            if (hastalar != null)
            {
                _context.Hastalars.Remove(hastalar);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Hata mesajı ekle
                ViewBag.messageString = "Hasta silinemedi. İlişkili Randevu kayıtları olabilir.";
                return View("Information");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool HastalarExists(int id)
        {
            return _context.Hastalars.Any(e => e.HastaId == id);
        }

    }
}
