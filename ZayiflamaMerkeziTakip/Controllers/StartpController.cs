using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ZayiflamaMerkeziTakip.Models;
namespace ZayiflamaMerkeziTakip.Controllers
{
    public class StartpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ZayiflamaMerkeziTakipContext _context;

        public StartpController(ZayiflamaMerkeziTakipContext context)
        {
            _context = context;

        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Randevus");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Login(Login logincs)
        {
            bool userExist = _context.Logins.Any(x => x.Username == logincs.Username && x.Password == logincs.Password);
            if (userExist)
            {
                List<Claim> claims = new List<Claim>()
                {
                new Claim(ClaimTypes.NameIdentifier, logincs.Username),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties prop = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = logincs.IsActive
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), prop);
                return RedirectToAction("Index", "Randevus");
            }
            ViewData["OnayMesaji"] = "Kullanıcı Bulunamadı";
            return View();
        }      
    }
}
