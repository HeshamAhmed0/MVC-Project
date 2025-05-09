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
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            if (ModelState.IsValid)
            {

                Employee employee = new Employee()
                {
                    Name = createEmployeeDto.Name,
                    HiringDate = createEmployeeDto.HiringDate,
                    Address = createEmployeeDto.Address,
                    Age = createEmployeeDto.Age,
                    CreateAt = createEmployeeDto.CreateAt,
                    Email = createEmployeeDto.Email,
                    IsActive = createEmployeeDto.IsActive,
                    IsDeleted = createEmployeeDto.IsDeleted,
                    Phone = createEmployeeDto.Phone,
                    salary = createEmployeeDto.salary,
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
        public IActionResult Edit(int? id,Employee employee)
        {
            if (id is null) return BadRequest("InValid Id");
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
