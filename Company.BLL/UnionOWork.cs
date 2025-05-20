using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.BLL.Reposatories;
using Company.hesham.DAL.Data.DbContexts;

namespace Company.BLL
{
    public class UnionOWork : IUnionOfWork
    {
        private readonly CompanyDbContext _companyDbContext;

        public IDepatmenReposatory depatmenReposatory { get; }

        public IEmployeeReposatorycs employeeReposatory { get; }
        public UnionOWork(CompanyDbContext companyDbContext)
        {
            _companyDbContext = companyDbContext;

            depatmenReposatory = new DepartmentReposatory(_companyDbContext);
            employeeReposatory = new EmployeeReposatory(_companyDbContext);
        }
        public async Task<int> Complete()
        {
            return _companyDbContext.SaveChanges();
        }
    }
}
