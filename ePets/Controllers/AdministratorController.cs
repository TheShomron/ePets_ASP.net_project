using ePets.Data;
using ePets.Models;
using ePets.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePets.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _SignInManger;
        private readonly MyAppDbContext _context;


        public AdministratorController(UserManager<ApplicationUser> userManger,
            SignInManager<ApplicationUser> SignInManger,
            MyAppDbContext context)
        {
            _userManger =userManger;
            _SignInManger = SignInManger;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users =await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            if (!ModelState.IsValid) return View(loginVm);

            var user = await _userManger.FindByEmailAsync(loginVm.EmailAdress);
            if (user != null)
            {
                var passwordCheck = await _userManger.CheckPasswordAsync(user, loginVm.Password);
                if (passwordCheck)
                {
                    var result = await _SignInManger.PasswordSignInAsync(user, loginVm.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Catalog");
                    }
                }
                TempData["Error"] = "Wrong Credentials. Please Try Again";
                return View(loginVm);
            }
            TempData["Error"] = "Wrong Credentials. Please Try Again";
            return View(loginVm);
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManger.FindByEmailAsync(registerVM.EmailAdress);
            if (user != null)
            {
                TempData["Error"] = "Email Adress Is Already In Use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAdress,
                UserName = registerVM.EmailAdress
            };
            var newUserRespone = await _userManger.CreateAsync(newUser, registerVM.Password);
            

            if (newUserRespone.Succeeded)
            {
                await _userManger.AddToRoleAsync(newUser, UserRoles.User);
            }
            else
            {
                List<IdentityError> errorList = newUserRespone.Errors.ToList();
                foreach(var item in errorList)
                {
                    TempData["Error"] += item.Description + " | ";
                }
                return View(registerVM);
            }

            return View("RegisterCompleted");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _SignInManger.SignOutAsync();
            return RedirectToAction("Index", "Catalog");
        }
    }
}
