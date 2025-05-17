using AutoMapper;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;

namespace Company.hesham.PL.Mapping.EmployeeMapping
{
    public class EmployeeToEditEmployee :Profile
    {
        public EmployeeToEditEmployee()
        {
            CreateMap<UpdateEmployeeDto,Employee>().ReverseMap();
        }
    }
}
