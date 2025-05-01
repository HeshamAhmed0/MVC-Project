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
            _dbContext.Remove(model);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
           return _dbContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
            var Result =_dbContext.Departments.Find(id);
            return Result;
        }

        public int Insert(Department model)
        {
            _dbContext.Add(model);
            return _dbContext.SaveChanges();
        }

        public int update(Department model)
        {
            _dbContext.Update(model);
            return _dbContext.SaveChanges();
        }
    }
}
