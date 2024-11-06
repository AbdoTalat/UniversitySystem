using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityManagmentSystem.Models.Context;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ProfileController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel NewPassModel)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if(user != null)
                {
                    var Result = await userManager.ChangePasswordAsync(user,NewPassModel.CurrentPassword, NewPassModel.NewPassword);
                    if(Result.Succeeded)
                    {
                        await signInManager.SignOutAsync();
                        return RedirectToAction("Welcome", "Dashboard");
                    }
                    foreach(var error in Result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Not Found");
                }
            }
            return View(NewPassModel);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteProfile(DeleteProfileViewModel DeleteModel)
        {
            var user = await userManager.GetUserAsync(User);
            if(user != null)
            {
                DeleteModel.UserName = user.UserName;
                DeleteModel.Email = user.Email;
            }
            return View(DeleteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProfile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var Result = await userManager.DeleteAsync(user);

            if (Result.Succeeded)
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Welcome", "Dashboard");
            }
            foreach (var error in Result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }

            return View(new DeleteProfileViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> OverView(EditProfileViewModel EditProfileVM)
        {
            var user = await userManager.GetUserAsync(User);
            if(user != null)
            {
                EditProfileVM.UserName = user.UserName;
                EditProfileVM.Email = user.Email;
                EditProfileVM.UserType = user.UserType;
            }
            return View(EditProfileVM);
        }
    }
}
