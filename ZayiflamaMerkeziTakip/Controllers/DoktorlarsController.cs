using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ZayiflamaMerkeziTakip.Models;

namespace ZayiflamaMerkeziTakip.Controllers
{
    [Authorize]
    public class DoktorlarsController : Controller
    {
        private readonly ZayiflamaMerkeziTakipContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public DoktorlarsController(ZayiflamaMerkeziTakipContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Doktorlars
        public async Task<IActionResult> Index()
        {
            var zayiflamaMerkeziTakipContext = _context.Doktorlars.Include(d => d.Uzmanlik);
            return View(await zayiflamaMerkeziTakipContext.ToListAsync());
        }

        // GET: Doktorlars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktorlar = await _context.Doktorlars
                .Include(d => d.Uzmanlik)
                .FirstOrDefaultAsync(m => m.DoktorId == id);
            if (doktorlar == null)
            {
                return NotFound();
            }

            return View(doktorlar);
        }

        // GET: Doktorlars/Create
        public IActionResult Create()
        {
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliks, "UzmanlikId", "Uzmanlik1");
            return View();
        }

        // POST: Doktorlars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoktorId,İsim,Soyisim,UzmanlikId,TelefonNo,Photo,ImageFile,Uzmanlik1")] Doktorlar doktorlar)
        {
            if (ModelState.IsValid)
            {
                if (doktorlar.ImageFile != null)
                {
                    string wwwrootpath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(doktorlar.ImageFile.FileName);
                    string extension = Path.GetExtension(doktorlar.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    doktorlar.Photo = "/contents/" + fileName;
                    string path = Path.Combine(wwwrootpath, "contents", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await doktorlar.ImageFile.CopyToAsync(filestream);
                    }
                }
                else
                {
                    // Varsayılan fotoğrafı ayarla
                    doktorlar.Photo = "/contents/default.jpg";
                }

                _context.Add(doktorlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliks, "UzmanlikId", "Uzmanlik1", doktorlar.UzmanlikId);
            return View(doktorlar);
        }

        // GET: Doktorlars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktorlar = await _context.Doktorlars.FindAsync(id);
            if (doktorlar == null)
            {
                return NotFound();
            }

            // Mevcut fotoğraf yolunu TempData'ya saklayın
            TempData["MevcutFotoPath"] = doktorlar.Photo;

            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliks, "UzmanlikId", "Uzmanlik1", doktorlar.UzmanlikId);
            return View(doktorlar);
        }


        // POST: Doktorlars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoktorId,İsim,Soyisim,UzmanlikId,Uzmanlik1,ImageFile,TelefonNo,Photo")] Doktorlar doktorlar)
        {
            if (id != doktorlar.DoktorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string mevcutFotoPath = TempData["MevcutFotoPath"] as string;

                    if (doktorlar.ImageFile != null)
                    {
                        string wwwrootpath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(doktorlar.ImageFile.FileName);
                        string extension = Path.GetExtension(doktorlar.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        doktorlar.Photo = "/contents/" + fileName;
                        string path = Path.Combine(wwwrootpath, "contents", fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await doktorlar.ImageFile.CopyToAsync(filestream);
                        }
                    }
                    else
                    {
                        doktorlar.Photo = mevcutFotoPath;
                    }

                    _context.Update(doktorlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorlarExists(doktorlar.DoktorId))
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

            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliks, "UzmanlikId", "Uzmanlik1", doktorlar.UzmanlikId);
            return View(doktorlar);
        }


        // GET: Doktorlars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktorlar = await _context.Doktorlars
                .Include(d => d.Uzmanlik)
                .FirstOrDefaultAsync(m => m.DoktorId == id);
            if (doktorlar == null)
            {
                return NotFound();
            }

            return View(doktorlar);
        }

        // POST: Doktorlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doktorlars == null)
            {
                return Problem("Entity set 'YourDbContext.Doktorlars' is null.");
            }

            var doktorlar = await _context.Doktorlars.FindAsync(id);
            if (doktorlar != null)
            {
                _context.Doktorlars.Remove(doktorlar);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Hata mesajı ekle
                ViewBag.messageString = "Doktor silinemedi. İlişkili Randevu kayıtları olabilir.";
                return View("Information");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DoktorlarExists(int id)
        {
            return _context.Doktorlars.Any(e => e.DoktorId == id);
        }
    }
}