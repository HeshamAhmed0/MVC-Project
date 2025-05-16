using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.hesham.DAL.Models;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeReposatorycs :IGenericReposatory<Employee>
    {
        public List<Employee> GetByName(string Name);
    }
}
