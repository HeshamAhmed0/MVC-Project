using Company.hesham.DAL.Models;
using Company.hesham.PL.Helping;
using Company.hesham.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.hesham.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpGet]
        public  IActionResult GetAll(string? SearchInput)
        {

            IEnumerable<UserToResult> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
               users= userManager.Users.Select(U => new UserToResult
                {
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Id = U.Id,
                    Roles = userManager.GetRolesAsync(U).Result
                }); 

               return View(users);
                
            }
            else
            {
                users = userManager.Users.Select(U => new UserToResult
                {
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Id = U.Id,
                    Roles = userManager.GetRolesAsync(U).Result
                }).Where(U=> U.FirstName.ToLower().Contains(SearchInput.ToLower()));
                return View(users);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {

            if (id is null) return BadRequest("InValid Id");
            var user =await userManager.FindByIdAsync(id);
            if (user is null) return NotFound("User Not Found");
            UserToResult userToResult = new UserToResult()
            {
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email
            };
                return View(userToResult);
           
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {

            if (id is null) return NotFound();
            var user =await userManager.FindByIdAsync(id);
            if (user is null) return BadRequest("User Not Found");
            UserToResult userToResult = new UserToResult()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return View(userToResult);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string? id, UserToResult model)
        {
            if (id is null) return BadRequest("InValid Id");
            var user = await userManager.FindByIdAsync(id);
            if (user is null) return BadRequest("User Not Found");
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Id=model.Id ;
            var result =await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("GetAll");
            }
            ModelState.AddModelError("", "InValid Update");
            return View(model);
            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
           if(id is null) return NotFound();
            var user =await userManager.FindByIdAsync(id);
            if (user is null) return BadRequest("User Not Found");
            UserToResult userToResult = new UserToResult()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return View(userToResult);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserToResult model)
        {
            if (id is null) return BadRequest("Invalid Id");
           
            var user =await userManager.FindByIdAsync(id);
            if (user is null) return BadRequest("User Not Found");
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("GetAll");
            }
            ModelState.AddModelError("","InValid Delete");
            return View(model);
        }

    }
}
