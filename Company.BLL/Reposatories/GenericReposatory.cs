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
    public class GenericReposatory<T> : IGenericReposatory<T> where T :BaseEntity
    {
        private readonly CompanyDbContext _companyDbcontext;

        public GenericReposatory(CompanyDbContext companyDbcontext) 
        {
            _companyDbcontext = companyDbcontext;
        }
        public void Add(T t)
        {
           _companyDbcontext.Set<T>().Add(t);
        
        }

        public void Delete(T t)
        {
            _companyDbcontext.Set<T>().Remove(t);
          
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T)==typeof(Employee))
            {
                return (IEnumerable < T >) _companyDbcontext.Employees.Include(D=>D.Department).ToList();
            }
            return _companyDbcontext.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return _companyDbcontext.Employees.Include(D => D.Department).FirstOrDefault(I => I.Id==id) as T;
            }
            return _companyDbcontext.Set<T>().Find(id);

        }

        public void Update(T t)
        {
         _companyDbcontext.Set<T>().Update(t);
           
        }
    }
}
