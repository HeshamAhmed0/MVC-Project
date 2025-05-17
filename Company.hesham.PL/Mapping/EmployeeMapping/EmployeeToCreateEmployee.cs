using AutoMapper;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;

namespace Company.hesham.PL.Mapping.EmployeeMapping
{
    public class EmployeeToCreateEmployee:Profile
    {
        public EmployeeToCreateEmployee()
        {
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
        }
    }
}
