using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.hesham.DAL.Models;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeReposatorycs
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        int Add(Employee employee);
        int Update(Employee employee);
        int Delete(Employee employee);  
    }
}
