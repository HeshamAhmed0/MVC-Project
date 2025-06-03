using System.Data;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Helping;
using Company.hesham.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.hesham.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager , UserManager<AppUser> userManager)
        {
            this._roleManager = roleManager;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(string? SearchInput)
        {

            IEnumerable<RoleToResult> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(R => new RoleToResult()
                {
                    Id = R.Id,
                    Name = R.Name
                });
                return View(roles);
            }
            else
            {
                roles = _roleManager.Roles.Select(R => new RoleToResult()
                {
                    Id = R.Id,
                    Name = R.Name
                }).Where(N=>N.Name.ToLower().Contains(SearchInput.ToLower()));
            }
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleToResult model)
        {
            if (ModelState.IsValid)
            {
               
                   var role =await  _roleManager.FindByNameAsync(model.Name);
                    if(role is null)
                    {
                        role =new IdentityRole()
                        {
                            Name = model.Name,
                        };
                        var result =await _roleManager.CreateAsync(role);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("GetAll");
                        }
                    }
                
                ModelState.AddModelError("", "Invalid Create");

            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {

            if (id is null) return BadRequest("InValid Id");
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound("User Not Found");
            RoleToResult roleToResult = new RoleToResult()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(roleToResult);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {

            if (id is null) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return BadRequest("User Not Found");
            RoleToResult roleToResult = new RoleToResult()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(roleToResult);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string? id, RoleToResult model)
        {
            if (id is null) return BadRequest("InValid Id");
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return BadRequest("Role Not Found");
            var SearchRole =await _roleManager.FindByNameAsync(model.Name);
            if(SearchRole == null)
            {
                role.Name = model.Name;

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAll");
                }
            }
           
            ModelState.AddModelError("", "InValid Update");
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return BadRequest("User Not Found");
            RoleToResult roleToResult = new RoleToResult()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(roleToResult);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleToResult model)
        {
            if (id is null) return BadRequest("Invalid Id");

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return BadRequest("User Not Found");
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("GetAll");
            }
            ModelState.AddModelError("", "InValid Delete");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveRole(string RoleId)
        {
           var role=await _roleManager.FindByIdAsync(RoleId);
            if (role is null) return NotFound();
            ViewData["RoleId"] = RoleId;
            var UserRole = new List<UserRoleToResult>();
            var users =await userManager.Users.ToListAsync();
            if(users == null)
            {
                return NotFound();
            }
            foreach(var user in users)
            {
                var UserRoles = new UserRoleToResult()
                {
                    Id = user.Id,
                    Name=user.UserName,
                };
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    UserRoles.IsAdded = true;
                }
                else
                {
                    UserRoles.IsAdded = false;
                }
                UserRole.Add(UserRoles);
            }
            return View(UserRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveRole(string roleId, List<UserRoleToResult> UsersRole)
        {
            if(ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null) return NotFound();
                foreach (var userRole in UsersRole)
                {
                    var user = await userManager.FindByIdAsync(userRole.Id);
                    if (user == null) return NotFound();

                    if (userRole.IsAdded == true && !await userManager.IsInRoleAsync(user, role.Name))
                    {
                        await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!userRole.IsAdded == true && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                return RedirectToAction(nameof(Edit),new {roleId=roleId});
              
            }

            return View(UsersRole);
        }

    }
}
