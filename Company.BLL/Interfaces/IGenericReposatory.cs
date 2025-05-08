using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.hesham.DAL.Models;

namespace Company.BLL.Interfaces
{
    public interface IGenericReposatory<T> where T :BaseEntity
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        int Add(T t);
        int Update(T t);
        int Delete(T t);
    }
}
