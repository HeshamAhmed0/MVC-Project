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
    public class EmployeeReposatory : GenericReposatory<Employee>, IEmployeeReposatorycs
    {
        public EmployeeReposatory(CompanyDbContext companyDbContext):base(companyDbContext) 
        {

        }

        //private readonly CompanyDbContext _dbContext;
        //public EmployeeReposatory(CompanyDbContext companyDbContext)
        //{
        //    _dbContext = companyDbContext;
        //}
        //public IEnumerable<Employee> GetAll()
        //{
        //    return _dbContext.Employees.ToList();
        //}
        //public Employee? GetById(int id)
        //{
        //    return _dbContext.Employees.Find(id);
        //}
        //public int Add(Employee employee)
        //{
        //   _dbContext.Employees.Add(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public int Update(Employee employee)
        //{
        //    _dbContext.Employees.Update(employee);
        //    return _dbContext.SaveChanges();
        //}
        //public int Delete(Employee employee)
        //{
        //  _dbContext.Employees.Remove(employee);
        //    return _dbContext.SaveChanges();
        //}
    }
}
