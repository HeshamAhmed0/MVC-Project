using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Reposatories;
using Company.hesham.DAL.Data.DbContexts;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Helping;
using Company.hesham.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.hesham.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnionOfWork _unionOfWork;

        //private readonly IEmployeeReposatorycs employeeReposatory;
        //private readonly IDepatmenReposatory _depatmenReposatory;
        private readonly IMapper _mapper;

        public EmployeeController(IUnionOfWork unionOfWork, IMapper mapper)
        {
            //employeeReposatory = _employeeReposatory;
            //_depatmenReposatory = depatmenReposatory;
             _unionOfWork = unionOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? SearchInput)
        {
            IEnumerable<Employee> model;
            if (string.IsNullOrEmpty(SearchInput))
            {
                 model =await _unionOfWork.employeeReposatory.GetAllAsync();
                var department =await _unionOfWork.depatmenReposatory.GetAllAsync();
                ViewData["department"] = department;
                return View(model);
            }else {
                 model =await _unionOfWork.employeeReposatory.GetByNameAsync(SearchInput);
                return View(model);
            }
         
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var department =await _unionOfWork.depatmenReposatory.GetAllAsync();
            ViewData["department"]=department;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(UpdateEmployeeDto updateEmployeeDto)
        {
            if (ModelState.IsValid)
            {

                //Employee employee = new Employee()
                //{
                //    Name = updateEmployeeDto.Name,
                //    HiringDate = updateEmployeeDto.HiringDate,
                //    Address = updateEmployeeDto.Address,
                //    Age = updateEmployeeDto.Age,
                //    CreateAt = updateEmployeeDto.CreateAt,
                //    Email = updateEmployeeDto.Email,
                //    IsActive = updateEmployeeDto.IsActive,
                //    IsDeleted = updateEmployeeDto.IsDeleted,
                //    Phone = updateEmployeeDto.Phone,
                //    salary = updateEmployeeDto.salary,
                //    DepartmentId = updateEmployeeDto.DepartmentId,
                //};
                if (updateEmployeeDto.Image is not null)
                {
                    updateEmployeeDto.ImgName = DocumentSetting.Upload(updateEmployeeDto.Image, "Images");
                }

                var employee =_mapper.Map<Employee>(updateEmployeeDto);
               _unionOfWork.employeeReposatory.Add(employee);
                int result =await _unionOfWork.Complete();
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
        public async Task<IActionResult> Details(int? id,string viewName="Details")
        {

            if (id is null) return BadRequest("InValid Id");
            var employee =await _unionOfWork.employeeReposatory.GetByIdAsync(id.Value);
            if (employee is null) return NotFound("Employee Not Found");
            else
            {
                return View(viewName,employee);
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var department =await _unionOfWork.depatmenReposatory.GetAllAsync();
            ViewData["department"] = department;
            if (id is null) return NotFound();
            var employee = _unionOfWork.employeeReposatory.GetByIdAsync(id.Value);
            //UpdateEmployeeDto updateEmployeeDto = new UpdateEmployeeDto()
            //{
            //    Name=employee.Name,
            //    DepartmentId=employee.DepartmentId,
            //    HiringDate=employee.HiringDate,
            //    IsDeleted=employee.IsDeleted,
            //    Email=employee.Email,
            //   Address=employee.Address,
            //   Age=employee.Age,
            //   CreateAt=employee.CreateAt,
            //   IsActive=employee.IsActive,
            //   Phone=employee.Phone,
            //   salary=employee.salary,

            //};
            
            var updateEmployeeDto=_mapper.Map<UpdateEmployeeDto>(employee);
            return View(updateEmployeeDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id,UpdateEmployeeDto updateEmployeeDto)
        {
            if (id is null) return BadRequest("InValid Id");
            if(updateEmployeeDto.ImgName is not null && updateEmployeeDto.Image is not null)
            {
                DocumentSetting.Delete(updateEmployeeDto.ImgName, "Images");
            }
            if (updateEmployeeDto.Image is not null)
            {
                DocumentSetting.Upload(updateEmployeeDto.Image, "Images");

            }

            //Employee employee = new Employee()
            //{
            //    Id = id.Value,
            //    Name = updateEmployeeDto.Name,
            //    Address = updateEmployeeDto.Address,
            //    HiringDate = updateEmployeeDto.HiringDate,
            //    Age = updateEmployeeDto.Age,
            //    CreateAt=updateEmployeeDto.CreateAt,
            //    Email = updateEmployeeDto.Email,
            //    IsDeleted = updateEmployeeDto.IsDeleted,
            //    IsActive = updateEmployeeDto.IsActive,
            //    Phone = updateEmployeeDto.Phone,
            //    salary=updateEmployeeDto.salary,
            //    DepartmentId = updateEmployeeDto.DepartmentId,
            //};
            var employee=_mapper.Map<Employee>(updateEmployeeDto);
            if (id is null) return NotFound();
            employee.Id = id.Value;
              _unionOfWork.employeeReposatory.Update(employee);
            int result =await _unionOfWork.Complete();

            if (result > 0)
            {
                TempData["EditEmployee"] = "Employe Edis Success";
                return RedirectToAction("GetAll");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int?id )
        {
            return await Details(id,"Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int?id ,Employee employee)
        {
            if (id is null) return BadRequest("Invalid Id");
            employee.Id = id.Value;
            var Emp =await _unionOfWork.employeeReposatory.GetByIdAsync(id.Value);
            if (Emp is null) return NotFound();
             _unionOfWork.employeeReposatory.Delete(Emp);
            int result =await _unionOfWork.Complete();

            if (result > 0)
            {
                if (employee.ImgName is not null )
                {
                    DocumentSetting.Delete(employee.ImgName, "Images");

                }
                return RedirectToAction("GetAll");
            }
            return View();
        }

    }
}
