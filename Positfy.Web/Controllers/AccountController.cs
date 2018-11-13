using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Postify.Controllers;
using Postify.Data.Models;
using Postify.Data.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Positfy.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> RegisterUser(RegisterUserViewModel vm)
        {
            if (vm.Password != vm.ConfirmPassword)
            {
                vm.Description = "Password and Confirmation must match.";
                vm.Class = "alert alert-danger";
                return Json(vm);
            }

            var user = new ApplicationUser { UserName = vm.Username, Email = vm.Email };
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                vm.Description = "Registration successful!";
                vm.Class = "alert alert-success";
                return Json(vm);
            }
            else
            {
                vm.Description = result.Errors.First().Description;
                vm.Class = "alert alert-danger";
                return Json(vm);
            }
        }

        public async Task<ActionResult> LoginUser(UserLoginViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);

            if (result.Succeeded)
            {
                return Json(new { type = "success", url = Url.Action("Index", "Home") });
            }

            return Json(new { type = "error", message = "Invalid password or username!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
