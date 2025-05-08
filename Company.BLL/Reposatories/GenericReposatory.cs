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
    public class GenericReposatory<T> : IGenericReposatory<T> where T :BaseEntity
    {
        private readonly CompanyDbContext _companyDbcontext;

        public GenericReposatory(CompanyDbContext companyDbcontext) 
        {
            _companyDbcontext = companyDbcontext;
        }
        public int Add(T t)
        {
           _companyDbcontext.Set<T>().Add(t);
            return _companyDbcontext.SaveChanges();
        }

        public int Delete(T t)
        {
            _companyDbcontext.Set<T>().Remove(t);
            return _companyDbcontext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _companyDbcontext.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return _companyDbcontext.Set<T>().Find(id);

        }

        public int Update(T t)
        {
         _companyDbcontext.Set<T>().Update(t);
            return _companyDbcontext.SaveChanges();
        }
    }
}
