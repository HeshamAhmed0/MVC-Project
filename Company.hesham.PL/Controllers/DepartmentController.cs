using Company.BLL.Reposatories;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;
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
        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _departmentReposatory.GetAll();
            return View(model);
        }
        [HttpGet]
       public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreatenIn= model.CreatenIn,
                };
               int result= _departmentReposatory.Insert(department);
                if (result > 0)
                {
                    return RedirectToAction("GetAll");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id ,string viewname ="Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = _departmentReposatory.GetById(id.Value);
            if (department is null) return NotFound("Department Not Found");
           
            
            return View(viewname,department);
            
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //    if (id is null) return BadRequest("InValid Id");
            //    var department = _departmentReposatory.GetById(id.Value);
            //    if (department is null) return NotFound("Department Not Found");
            return Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {
            if (ModelState.IsValid)
            {

                int result = _departmentReposatory.update(department);
                if (result > 0)
                {
                    return RedirectToAction("GetAll");
                }

            }
            return View(department);
        }

        /// This is not Perfect Casting
        //[HttpPost]
        //public IActionResult Edit([FromRoute] int id,UpdateDepartmentDTO department)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Department _department = new Department()
        //        {
        //            Id=id,
        //            Name=department.Name,
        //            Code=department.Code,
        //            CreatenIn=department.CreatenIn,
        //        };
        //        int result = _departmentReposatory.update(_department);
        //        if (result > 0)
        //        {
        //            return RedirectToAction("GetAll");
        //        }

        //    }
        //    return View(department);
        //}
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");

            //var department = _departmentReposatory.GetById(id.Value);
            //if (department is null) return NotFound();
            return Details(id,"Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id , Department department)
        {
           if(ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest("InValid Id");
                int Result = _departmentReposatory.delete(department);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(GetAll));
                }
            }
           return View(department);
        }
    }
}
