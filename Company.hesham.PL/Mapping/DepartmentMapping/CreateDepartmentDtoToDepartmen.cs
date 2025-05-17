using AutoMapper;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;

namespace Company.hesham.PL.Mapping.DepartmentMapping
{
    public class CreateDepartmentDtoToDepartmen:Profile
    {
        public CreateDepartmentDtoToDepartmen()
        {
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<Department,CreateDepartmentDto>();

        }
    }
}
