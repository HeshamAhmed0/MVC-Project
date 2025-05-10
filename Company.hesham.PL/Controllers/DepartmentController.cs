using Company.BLL.Interfaces;
using Company.BLL.Reposatories;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.hesham.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepatmenReposatory _departmentReposatory;
        public DepartmentController(IDepatmenReposatory departmentReposatory)
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
                    CreateAt = model.CreatenIn,
                };
               int result= _departmentReposatory.Add(department);
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
            if (id is null) return BadRequest("InValid Id");
            var department = _departmentReposatory.GetById(id.Value);
            if (department is null) return NotFound("Department Not Found");

            CreateDepartmentDto createDepartmentDto = new CreateDepartmentDto()
            {
                Name=department.Name,
                Code=department.Code,
                CreatenIn=department.CreateAt,
            };
            return View(createDepartmentDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,CreateDepartmentDto _department)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Id=id,
                    Name=_department.Name,
                    Code=_department.Code,
                    CreateAt=_department.CreatenIn,
                };

                int result = _departmentReposatory.Update(department);
                if (result > 0)
                {
                    return RedirectToAction("GetAll");
                }

            }
            return View(_department);
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
                int Result = _departmentReposatory.Delete(department);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(GetAll));
                }
            }
           return View(department);
        }
    }
}
