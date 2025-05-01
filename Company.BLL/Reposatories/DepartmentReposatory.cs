using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.hesham.DAL.Data.DbContexts;
using Company.hesham.DAL.Models;

namespace Company.BLL.Reposatories
{
    public class DepartmentReposatory : IDepatmenReposatory
    {
        private readonly CompanyDbContext _dbContext;
        public DepartmentReposatory(CompanyDbContext companyDbContext)
        {
            _dbContext = companyDbContext;
        }
        public int delete(Department model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAll()
        {
           return _dbContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Department model)
        {
            throw new NotImplementedException();
        }

        public int update(Department model)
        {
            throw new NotImplementedException();
        }
    }
}
