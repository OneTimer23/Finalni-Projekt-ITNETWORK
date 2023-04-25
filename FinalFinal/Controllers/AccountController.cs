using FinalFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinalFinal.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller

    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly ProjektItnetworkContext _context;

        public AccountController(ProjektItnetworkContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
            {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        /*
        [HttpGet]
        public IActionResult Login()
        {
            var u = new LoginViewModel();
            

            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel u) 
        {
            if (ModelState.IsValid)
            {
                var qry = _context.Uzivateles.Where(p => p.UzivatelskeJmeno == u.UserName && p.Heslo == u.Password);
                if (qry.Count() == 0)
                {
                    u.UserMessage = "Neplatné heslo nebo login.";
                    
                    return View(u);
                }

                var user = new IdentityUser() { UserName = u.UserName };

                await _signInManager.SignInAsync(user, isPersistent: false);
               
                return RedirectToAction("Index", "Kontakts");

            }

            return View(u);

        }
        */
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string navratovaURL = null)
        {
            ViewData["ReturnUrl"] = navratovaURL;
            if (ModelState.IsValid)
            {
                var vysledekOvereni = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe,  lockoutOnFailure: false);
                if (vysledekOvereni.Succeeded)
                {
                    return RedirectToLocal(navratovaURL);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Neplatné přihlašovací údaje.");
                    return View(model);
                }
            }

            // Pokud byly odeslány neplatné údaje, vrátíme uživatele k přihlašovacímu formuláři
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, NormalizedUserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}
