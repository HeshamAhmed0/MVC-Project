using Company.BLL.Interfaces;
using Company.BLL.Reposatories;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.hesham.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReposatorycs employeeReposatory;

        public EmployeeController(IEmployeeReposatorycs _employeeReposatory)
        {
            employeeReposatory = _employeeReposatory;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
          var model =employeeReposatory.GetAll();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UpdateEmployeeDto updateEmployeeDto)
        {
            if (ModelState.IsValid)
            {

                Employee employee = new Employee()
                {
                    Name = updateEmployeeDto.Name,
                    HiringDate = updateEmployeeDto.HiringDate,
                    Address = updateEmployeeDto.Address,
                    Age = updateEmployeeDto.Age,
                    CreateAt = updateEmployeeDto.CreateAt,
                    Email = updateEmployeeDto.Email,
                    IsActive = updateEmployeeDto.IsActive,
                    IsDeleted = updateEmployeeDto.IsDeleted,
                    Phone = updateEmployeeDto.Phone,
                    salary = updateEmployeeDto.salary,
                };
               int result =employeeReposatory.Add(employee);
                if (result > 0)
                {
                    return RedirectToAction("GetAll");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id,string viewName="Details")
        {
            if (id is null) return BadRequest("InValid Id");
            var employee = employeeReposatory.GetById(id.Value);
            if (employee is null) return NotFound("Employee Not Found");
            else
            {
                return View(viewName,employee);
            }
            
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(int? id,UpdateEmployeeDto updateEmployeeDto)
        {
            if (id is null) return BadRequest("InValid Id");
            Employee employee = new Employee()
            {
                Id = id.Value,
                Name = updateEmployeeDto.Name,
                Address = updateEmployeeDto.Address,
                HiringDate = updateEmployeeDto.HiringDate,
                Age = updateEmployeeDto.Age,
                CreateAt=updateEmployeeDto.CreateAt,
                Email = updateEmployeeDto.Email,
                IsDeleted = updateEmployeeDto.IsDeleted,
                IsActive = updateEmployeeDto.IsActive,
                Phone = updateEmployeeDto.Phone,
                salary=updateEmployeeDto.salary,
            };
            if (id != employee.Id) return NotFound();
             int result= employeeReposatory.Update(employee);
            if (result > 0)
            {
                return RedirectToAction("GetAll");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int?id )
        {
            return Details(id,"Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute]int?id ,Employee employee)
        {
            if (id is null) return BadRequest("Invalid Id");
            if (id != employee.Id) return NotFound();
            var result = employeeReposatory.Delete(employee);
            if (result > 0)
            {
                return RedirectToAction("GetAll");
            }
            return View();
        }

    }
}
