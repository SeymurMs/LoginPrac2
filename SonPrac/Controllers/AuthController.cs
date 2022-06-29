using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SonPrac.Models;
using SonPrac.ViewModel.Autho;
using System.Threading.Tasks;

namespace SonPrac.Controllers
{
    public class AuthController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signManager;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        
        
        {
            if (!ModelState.IsValid)
            {
                return View(register);

            }
            if (!register.Email.Contains("@"))
            {
                ModelState.AddModelError("Email", "Wrong Email");
                return View(register);

            }
            AppUser appUser = new AppUser
            {
               Email = register.Email,
               LastName = register.LastName,
               FirstName=register.FirsName,
               UserName=register.Username  
            };
            var result=await _userManager.CreateAsync(appUser,register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                       
                }
                return View();
            }
             return RedirectToAction("Login","Auth");
    
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user;
            if (login.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
            }
            else
            {
                user=await _userManager.FindByNameAsync(login.UsernameOrEmail);
            }
            if (user==null)
            {
                ModelState.AddModelError("", "User Is Null !!!");
                return View();
            }
            var result = await _signManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Wrong Data");
                return View();

            }

            
    
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> LogOut()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
