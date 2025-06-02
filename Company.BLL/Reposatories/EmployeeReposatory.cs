using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.hesham.DAL.Data.DbContexts;
using Company.hesham.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.BLL.Reposatories
{
    public class EmployeeReposatory : GenericReposatory<Employee>, IEmployeeReposatorycs
    {
        private readonly CompanyDbContext companyDbContext;

        public EmployeeReposatory(CompanyDbContext companyDbContext):base(companyDbContext) 
        {
            this.companyDbContext = companyDbContext;
        }

        public async Task<List<Employee>> GetByNameAsync(string Name)
        {
           return await companyDbContext.Employees.Include(D => D.Department).Where(N => N.Name.Contains(Name)).ToListAsync();
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
