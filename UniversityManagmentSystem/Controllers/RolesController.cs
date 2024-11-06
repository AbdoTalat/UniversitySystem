using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityManagmentSystem.Models.Context;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RolesController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New( RoleViewModel NewRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = NewRole.RoleName;
                IdentityResult result = await roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return View(new RoleViewModel());
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RemoveRoleFromUserViewModel RemoveRoleVM)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser User = await userManager.FindByNameAsync(RemoveRoleVM.UserName);
                if (User == null)
                {
                    return NotFound();
                }

                var isInRole = await userManager.IsInRoleAsync(User, RemoveRoleVM.RoleName);
                if (!isInRole)
                {
                    return BadRequest($"User Not in The {RemoveRoleVM.RoleName} Role.");
                }
                var Result = await userManager.RemoveFromRoleAsync(User, RemoveRoleVM.RoleName);
                if (!Result.Succeeded)
                {
                    return BadRequest($"Failed To Remove Role");
                }
                return Ok($"Removed {RemoveRoleVM.RoleName} from User");
            }

            return Ok($"Removed {RemoveRoleVM.RoleName} from User");
        }

    }
}
