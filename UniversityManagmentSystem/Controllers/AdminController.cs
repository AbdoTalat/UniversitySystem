using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityManagmentSystem.Models.Context;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(RegisterViewModel NewUserVM)
        {
            if (ModelState.IsValid)
            {
                var UserModel = new ApplicationUser
                {
                    UserName = NewUserVM.UserName,
                    Email = NewUserVM.Email,
                    PasswordHash = NewUserVM.Password,
                    UserType = NewUserVM.UserType
                };


                IdentityResult Result = await userManager.CreateAsync(UserModel, NewUserVM.Password);
                if (Result.Succeeded)
                {
                    await userManager.AddToRoleAsync(UserModel, "Admin");

                    await signInManager.SignInAsync(UserModel, false);
                    return RedirectToAction("Index", "Dashboard");   
                }
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(NewUserVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddExistAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddExistAdmin(LoginViewModel LoginUserVM)
        {
            if (ModelState.IsValid)
            {
                var UserModel = await userManager.FindByNameAsync(LoginUserVM.UserName);
                if (UserModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(UserModel, LoginUserVM.Password);
                    if (found)
                    {
                        await userManager.AddToRoleAsync(UserModel, "Admin");

                        await signInManager.SignInAsync(UserModel, false);
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                ModelState.AddModelError("", "User Name and Password Are Not Correct!");
            }

            return View(LoginUserVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveAdmin()
        {
            return View(new RemoveAdminViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdmin(RemoveAdminViewModel RemoveAdminModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(RemoveAdminModel.AdminUserName);
                if (user != null)
                {
                    var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
                    if (isAdmin)
                    {
                        
                        var Result = await userManager.RemoveFromRoleAsync(user, "Admin");
                        
                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        foreach (var item in Result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }

                    }
                    else
                        ModelState.AddModelError("", "No Admin With This User Name");
                }
                else
                    ModelState.AddModelError("", "No User With This Name");
            }
            return View(new RemoveAdminViewModel());
        }
    }
}
