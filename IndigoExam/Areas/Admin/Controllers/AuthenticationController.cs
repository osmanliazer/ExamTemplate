using IndigoExam.DAL;
using IndigoExam.Models;
using IndigoExam.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IndigoExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AutendicationController : Controller
    {
        private readonly UserManager<AppUser> _usermaneger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AutendicationController(UserManager<AppUser> usermaneger, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _usermaneger = usermaneger;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new AppUser
            {
                Name = registerVM.Name,

                UserName = registerVM.UserName,
                Email = registerVM.Email,
                Surname = registerVM.Surname,

            };
            var result = await _usermaneger.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            await _signInManager.SignInAsync(appUser, false);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = await _usermaneger.FindByEmailAsync(loginVM.UserNameorEmail);
            if (appUser == null)
            {
                appUser = await _usermaneger.FindByNameAsync(loginVM.UserNameorEmail);

                if (appUser == null)
                {
                    ModelState.AddModelError(string.Empty, "UserName,Password or Email was Wrong");
                    return View();
                }
            }
            var Result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, loginVM.IsRemember, true);
            if (Result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "You re Block");
                return View();

            }
            if (!Result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "UserName,Password or Email was Wrong");
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }

}