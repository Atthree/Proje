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
    public class RandevusController : Controller
    {
        private readonly ZayiflamaMerkeziTakipContext _context;

        public RandevusController(ZayiflamaMerkeziTakipContext context)
        {
            _context = context;
        }

        // GET: Randevus
        public async Task<IActionResult> Index()
        {
            var zayiflamaMerkeziTakipContext = _context.Randevus.Include(r => r.Doktor).Include(r => r.Hasta).Include(r => r.Uzmanlik);
            return View(await zayiflamaMerkeziTakipContext.ToListAsync());
        }

        // GET: Randevus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevus
                .Include(r => r.Doktor)
                .Include(r => r.Hasta)
                .Include(r => r.Uzmanlik)
                .FirstOrDefaultAsync(m => m.RandevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Randevus/Create
        public IActionResult Create()
        {
            List<Uzmanlik> uzmanlikListe = new List<Uzmanlik>();
            uzmanlikListe = (from Uzmanlik in _context.Uzmanliks
                             select
                             Uzmanlik).ToList();
            uzmanlikListe.Insert(0, new Uzmanlik { UzmanlikId = 0, Uzmanlik1 = "Uzmanlık Seçiniz" });
            ViewBag.ListofUzmanlik = uzmanlikListe;
            ViewData["DoktorId"] = new SelectList(_context.Doktorlars, "DoktorId", "İsim");
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim");
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliks, "UzmanlikId", "Uzmanlik1");
            return View();
        }
        [HttpPost]
        public JsonResult GetDoktorlar(int UzmanlikId)
        {

            var doktorListe = (from doktorlar in _context.Doktorlars
                             where doktorlar.UzmanlikId == UzmanlikId
                             select new
                             {
                                 Text = doktorlar.İsim,
                                 Value = doktorlar.DoktorId
                             }).ToList();

            return Json(doktorListe, new System.Text.Json.JsonSerializerOptions());

        }

        // POST: Randevus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RandevuId,HastaId,RandevuTarihi,UzmanlikId,DoktorId,Isim,İsim,Uzmanlik1")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<Uzmanlik> uzmanlikListe = new List<Uzmanlik>();
            uzmanlikListe = (from Uzmanlik in _context.Uzmanliks
                           select
                           Uzmanlik).ToList();
            uzmanlikListe.Insert(0, new Uzmanlik { UzmanlikId = 0, Uzmanlik1 = "Uzmanlık Seçiniz" });
            ViewBag.ListofUzmanlik = uzmanlikListe;
            ViewData["DoktorId"] = new SelectList(_context.Doktorlars, "DoktorId", "İsim", randevu.DoktorId);
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim", randevu.HastaId);
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliks, "UzmanlikId", "Uzmanlik1", randevu.UzmanlikId);
            return View(randevu);
        }

        // GET: Randevus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevus.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            List<Uzmanlik> uzmanlikListe = new List<Uzmanlik>();
            uzmanlikListe = (from Uzmanlik in _context.Uzmanliks
                             select
                             Uzmanlik).ToList();
            uzmanlikListe.Insert(0, new Uzmanlik { UzmanlikId = 0, Uzmanlik1 = "Uzmanlık Seçiniz" });
            ViewBag.ListofUzmanlik = uzmanlikListe;
            ViewData["DoktorId"] = new SelectList(_context.Doktorlars, "DoktorId", "İsim", randevu.DoktorId);
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim", randevu.HastaId);
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliks, "UzmanlikId", "Uzmanlik1", randevu.UzmanlikId);
            return View(randevu);
        }

        // POST: Randevus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RandevuId,HastaId,RandevuTarihi,UzmanlikId,DoktorId,Isim,İsim,Uzmanlik1")] Randevu randevu)
        {
            if (id != randevu.RandevuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.RandevuId))
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
            List<Uzmanlik> uzmanlikListe = new List<Uzmanlik>();
            uzmanlikListe = (from Uzmanlik in _context.Uzmanliks
                             select
                             Uzmanlik).ToList();
            uzmanlikListe.Insert(0, new Uzmanlik { UzmanlikId = 0, Uzmanlik1 = "Uzmanlık Seçiniz" });
            ViewBag.ListofUzmanlik = uzmanlikListe;
            ViewData["DoktorId"] = new SelectList(_context.Doktorlars, "DoktorId", "İsim", randevu.DoktorId);
            ViewData["HastaId"] = new SelectList(_context.Hastalars, "HastaId", "Isim", randevu.HastaId);
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliks, "UzmanlikId", "Uzmanlik1", randevu.UzmanlikId);
            return View(randevu);
        }

        // GET: Randevus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevus
                .Include(r => r.Doktor)
                .Include(r => r.Hasta)
                .Include(r => r.Uzmanlik)
                .FirstOrDefaultAsync(m => m.RandevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevus == null)
            {
                return Problem("Entity set 'ZayiflamaMerkeziTakipContext.Randevus' is null.");
            }

            var randevu = await _context.Randevus.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevus.Remove(randevu);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Hata mesajı ekle
                ViewBag.messageString = "Randevu silinemedi. İlişkili Hasta kayıtları olabilir.";
                return View("Information");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
            return _context.Randevus.Any(e => e.RandevuId == id);
        }

    }
}
