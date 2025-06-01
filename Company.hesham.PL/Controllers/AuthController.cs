using System.Security.Policy;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Helping;
using Company.hesham.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company.hesham.PL.Controllers
{
    public class AuthController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = await userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        AppUser appUser = new AppUser()
                        {
                            UserName = model.UserName,
                            Email = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            IsAgree = model.IsAgree,
                        };
                        var result = await userManager.CreateAsync(appUser, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                ModelState.AddModelError("", "InValid SignUp");

            }


            return View(model);
        }

        #endregion

        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var flag = await userManager.CheckPasswordAsync(User, model.Password);
                    if (flag == true)
                    {
                        var result = await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");

                        }
                    }
                }
                ModelState.AddModelError("", "InValid Login");
            }
            return View(model);
        }

        #endregion

        #region SignOut

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
          await signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ForgerPasswordDto model)
        {
            if(ModelState.IsValid)
            {
                var user =await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    //Generate Tooken
                    var Tooken =userManager.GeneratePasswordResetTokenAsync(user);
                    //Generate Url
                    var url =Url.Action("ResetPassword","Auth",new { email =model.Email,Tooken},Request.Scheme);
                     Email email = new Email()
                     {
                         To=model.Email,
                         Subject="Resset Password",
                         Body="URL"
                     };
                    var Flag =Helping.EmailSetting.SendEmail(email);
                    if (Flag)
                    {
                        //Check YOur Boks
                    }
                }
            }
            ModelState.AddModelError("", "InValid Reset Password Operation !!");
            return View("ForgetPassword", model);
        }



        #endregion
    }
}
       