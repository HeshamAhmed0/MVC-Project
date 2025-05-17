using AutoMapper;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;

namespace Company.hesham.PL.Mapping.DepartmentMapping
{
    public class DeleteDepartmentDtoToDepartment:Profile
    {
        public DeleteDepartmentDtoToDepartment()
        {
            CreateMap<Department, DeleteDepartmentDto>().ReverseMap();
        }
    }
}
