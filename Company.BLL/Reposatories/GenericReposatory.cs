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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T)==typeof(Employee))
            {
                var result= await  _companyDbcontext.Employees.Include(D=>D.Department).ToListAsync();
                return result.Cast<T>();
            }
            return await _companyDbcontext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _companyDbcontext.Employees.Include(D => D.Department).FirstOrDefaultAsync(I => I.Id==id) as T;
            }
            return _companyDbcontext.Set<T>().Find(id);

        }

        public void Update(T t)
        {
         _companyDbcontext.Set<T>().Update(t);
           
        }
    }
}
