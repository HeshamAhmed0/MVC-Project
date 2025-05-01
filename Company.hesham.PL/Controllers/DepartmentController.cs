using Company.BLL.Reposatories;
using Company.hesham.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.hesham.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentReposatory _departmentReposatory;
        public DepartmentController(DepartmentReposatory departmentReposatory)
        {
            _departmentReposatory = departmentReposatory;
        }
        public IActionResult GetAll()
        {
            var model = _departmentReposatory.GetAll();
            return View(model);
        }
       public IActionResult Insert(int id)
        {

            return View();
        }
    }
}
