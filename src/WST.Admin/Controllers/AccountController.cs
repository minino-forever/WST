using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WST.Admin.Models.ViewModels;

namespace WST.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(loginViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.Name);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var signInResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        return Redirect(loginViewModel?.ReturnUrl ?? "/Admin/Index");
                    }
                }
            }
            
            ModelState.AddModelError(String.Empty, "Неверный логин или пароль");

            return View(loginViewModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }
    }
}