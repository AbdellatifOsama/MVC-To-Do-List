using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Data.Entities;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid && user.IsTermsAgreed == true)
            {
                //if Model Valid
                if (!IsEmailDuplicated(user.Email).Result)
                {
                    //if the email isn't Duplicated
                     var IdentityUser = new ApplicationUser
                     {
                         UserName = user.UserName,
                         Email = user.Email,
                     };
                    var result = await userManager.CreateAsync(IdentityUser, user.Password);
                    //if Register Process Succeed
                    if (result.Succeeded)
                    {
                        await signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
                        return RedirectToAction("index", "home");
                    }
                    //if register process Failed
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                //if email duplicated
                else
                {
                    ModelState.AddModelError("", "Email Is Used Before");
                }
            }
            return View(user);
        }
        private async Task<bool> IsEmailDuplicated(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is not null)
                return true;
            return false;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email??"");
            if (user is not null)
            {
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe,false);
                if (result.Succeeded)
                    return Redirect("/home/index");
            }
            ModelState.AddModelError("", "Username or Password isn't Correct");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }
    }
}
