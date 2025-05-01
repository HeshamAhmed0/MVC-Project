using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.hesham.DAL.Models;

namespace Company.BLL.Interfaces
{
    public interface IDepatmenReposatory
    {
        public IEnumerable<Department> GetAll();
        public Department GetById(int id);
        public int Insert(Department model);
        public int delete(Department model);
        public int update(Department model);
    }
}
