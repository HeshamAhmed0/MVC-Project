using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Reposatories;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.hesham.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnionOfWork _unionOfWork;

        //private readonly IDepatmenReposatory _departmentReposatory;
        private readonly IMapper _mapper;

        public DepartmentController(IUnionOfWork unionOfWork,IMapper mapper)
        {
            _unionOfWork = unionOfWork;
            //_departmentReposatory = departmentReposatory;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? SearchInput)
        {
            if (string.IsNullOrEmpty(SearchInput))
            {
                var model =await _unionOfWork.depatmenReposatory.GetAllAsync();
                return View(model);
            }
            else
            {
                var model =await _unionOfWork.depatmenReposatory.GetDepartmentsByNameAsync(SearchInput);
                return View(model);
            }
           
        }
        [HttpGet]
       public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                //Department department = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreatenIn,
                //};
                var department = _mapper.Map<Department>(model);
               _unionOfWork.depatmenReposatory.Add(department);
                int Result =await _unionOfWork.Complete();

                if (Result > 0)
                {
                    return RedirectToAction("GetAll");
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id ,string viewname ="Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var department =await _unionOfWork.depatmenReposatory.GetByIdAsync(id.Value);
            if (department is null) return NotFound("Department Not Found");
            var DeleteDepartmentDto = _mapper.Map<DeleteDepartmentDto>(department);
            return View(viewname,DeleteDepartmentDto);
            
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest("InValid Id");
            var department =await _unionOfWork.depatmenReposatory.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Edit([FromRoute] int id,CreateDepartmentDto _department)
        {
            if (ModelState.IsValid)
            {
                //    Department department = new Department()
                //    {
                //        Id=id,
                //        Name=_department.Name,
                //        Code=_department.Code,
                //        CreateAt=_department.CreatenIn,
                //    };

               var department= _mapper.Map<Department>(_department);
                department.Id = id;
                 _unionOfWork.depatmenReposatory.Update(department);
                int Result =await _unionOfWork.Complete();

                if (Result > 0)
                {
                    return RedirectToAction("GetAll");
                }

            }
            return View();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var department =await _unionOfWork.depatmenReposatory.GetByIdAsync(id.Value);
            if (department is null) return NotFound();
            var DeleteDepartmentDto = _mapper.Map<DeleteDepartmentDto>(department);
           return View(DeleteDepartmentDto);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id , DeleteDepartmentDto _department)
        {
           if(ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(_department);
                if (id != department.Id) return BadRequest("InValid Id");
                 _unionOfWork.depatmenReposatory.Delete(department);
                int Result =await _unionOfWork.Complete();

                if (Result > 0)
                {
                    return RedirectToAction(nameof(GetAll));
                }
            }
           return View();
        }
    }
}
