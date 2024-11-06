using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.ContentModel;
using System.Diagnostics;
using System.Reflection.Metadata;
using UniversityManagmentSystem.ViewModels;
using UniversityManagmentSystem.Repository.Interfaces;
using UniversityManagmentSystem.Models.Context;

namespace UniversityManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IinstructorRepository iinstructorRepository;
        private readonly IStudentRepository studentRepository;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager,
            IinstructorRepository _iinstructorRepository, IStudentRepository _studentRepository)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            iinstructorRepository = _iinstructorRepository;
            studentRepository = _studentRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = model.Password,
                    UserType = model.UserType
                };

                // Check if the email exists in the Instructors table
                if (model.UserType.ToUpper() == "Instructor".ToUpper())
                {
                    var instructor = await iinstructorRepository.GetByEmailAsync(model.Email);
                    if (instructor == null)
                    {
                        ModelState.AddModelError("", "This Instructor Email Does Not Exist In The System");
                        return View(model);
                    }
                }

                // Check if the email exists in the Students table
                else if (model.UserType.ToUpper() == "Student".ToUpper())
                {
                    var student = await studentRepository.GetByEmailAsync(model.Email);
                    if (student == null)
                    {
                        ModelState.AddModelError("", "This Student Email Does Not Exist In The System");
                        return View(model);
                    }
                }

                // Proceed with user registration if email check passes
                var existingUser = await userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "This email is already registered.");
                    return View(model);
                }

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleName = model.UserType == "Instructor" ? "Instructor" : "Student";
                    await userManager.AddToRoleAsync(user, roleName);

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            #region
            //if (ExistUser == null)
            //{
            //    if (user.UserType.ToUpper() == "Instructor".ToUpper())
            //    {
            //        if (iinstructorRepository.GetByEmail(NewUserModel.Email) != null)
            //        {
            //            IdentityResult Result = await userManager.CreateAsync(user, user.PasswordHash);

            //            if (Result.Succeeded)
            //            {
            //                await signInManager.SignInAsync(user, isPersistent: false);
            //                await userManager.AddToRoleAsync(user, "Instructor");
            //                return RedirectToAction("Index", "Dashboard");
            //            }
            //            foreach (var item in Result.Errors)
            //            {
            //                ModelState.AddModelError("", item.Description);
            //            }
            //        }
            //        else
            //            ModelState.AddModelError("", "This Instructor Email Does Not Exist In The System");
            //    }

            //    else if (NewUserModel.UserType.ToUpper() == "Student".ToUpper())
            //    {
            //        if (studentRepository.GetByEmail(NewUserModel.Email) != null)
            //        {
            //            IdentityResult Result = await userManager.CreateAsync(user, user.PasswordHash);

            //            if (Result.Succeeded)
            //            {
            //                await signInManager.SignInAsync(user, isPersistent: false);
            //                await userManager.AddToRoleAsync(user, "Student");
            //                return RedirectToAction("Index", "Dashboard");
            //            }
            //            foreach (var item in Result.Errors)
            //            {
            //                ModelState.AddModelError("", item.Description);
            //            }
            //        }
            //        else
            //            ModelState.AddModelError("", "This Student Email Does Not Exist In The System");
            //    }

            //    else
            //        ModelState.AddModelError("", "This Email Does Not Exist In The System");
            //}

            //else
            //{
            //    ModelState.AddModelError("", "This Email Is Used");
            //}
            #endregion
            return View(model);
        }




        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel LoginUserVM)
        {
            if (ModelState.IsValid)
            {
                var UserModel = await userManager.FindByNameAsync(LoginUserVM.UserName);
                if (UserModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(UserModel, LoginUserVM.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(UserModel, isPersistent: LoginUserVM.RememberMe);
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                ModelState.AddModelError("", "User Name and Password Are Not Correct!");
            }

            return View(LoginUserVM);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Welcome", "Dashboard");
        }




        
    }
}
