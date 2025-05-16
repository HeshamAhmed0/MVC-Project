using Company.BLL.Interfaces;
using Company.BLL.Reposatories;
using Company.hesham.DAL.Data.DbContexts;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.hesham.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReposatorycs employeeReposatory;
        private readonly IDepatmenReposatory _depatmenReposatory;

        public EmployeeController(IEmployeeReposatorycs _employeeReposatory,IDepatmenReposatory depatmenReposatory)
        {
            employeeReposatory = _employeeReposatory;
            _depatmenReposatory = depatmenReposatory;
        }
        [HttpGet]
        public IActionResult GetAll(string? SearchInput)
        {
            IEnumerable<Employee> model;
            if (string.IsNullOrEmpty(SearchInput))
            {
                 model = employeeReposatory.GetAll();
                var department = _depatmenReposatory.GetAll();
                ViewData["department"] = department;
                return View(model);
            }else {
                 model = employeeReposatory.GetByName(SearchInput);
                return View(model);
            }
         
        }
        [HttpGet]
        public IActionResult Create()
        {
            var department = _depatmenReposatory.GetAll();
            ViewData["department"]=department;
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
                    DepartmentId = updateEmployeeDto.DepartmentId,
                };
               int result =employeeReposatory.Add(employee);
                if (result > 0)
                {
                    //Life Time Of This Property For One Request
                    //ViewData["Message"] = "Hello in ViewData";

                    //ViewBag.Message = "Hellow in viewBag";

                    // This Send This Date From Request For View Create To View GetAll
                    TempData["Message"] = "Employee Create Success ";
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
        public IActionResult Edit(int? id)
        {
            var department = _depatmenReposatory.GetAll();
            ViewData["department"] = department;
            if (id is null) return NotFound();
            var employee = employeeReposatory.GetById(id.Value);
            UpdateEmployeeDto updateEmployeeDto = new UpdateEmployeeDto()
            {
                Name=employee.Name,
                DepartmentId=employee.DepartmentId,
                HiringDate=employee.HiringDate,
                IsDeleted=employee.IsDeleted,
                Email=employee.Email,
               Address=employee.Address,
               Age=employee.Age,
               CreateAt=employee.CreateAt,
               IsActive=employee.IsActive,
               Phone=employee.Phone,
               salary=employee.salary,

            };
            return View(updateEmployeeDto);
        }
        [HttpPost]
        public IActionResult Edit(int? id,Employee updateEmployeeDto)
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
                DepartmentId = updateEmployeeDto.DepartmentId,
            };
            if (id != employee.Id) return NotFound();
             int result= employeeReposatory.Update(employee);
            if (result > 0)
            {
                TempData["EditEmployee"] = "Employe Edis Success";
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
