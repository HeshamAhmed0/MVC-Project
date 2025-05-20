using AutoMapper;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Models;

namespace Company.hesham.PL.Mapping.EmployeeMapping
{
    public class UpdateEmployeeDtoToEmployee :Profile
    {
        public UpdateEmployeeDtoToEmployee()
        {
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<Employee, UpdateEmployeeDto>();
        }
    }
}
