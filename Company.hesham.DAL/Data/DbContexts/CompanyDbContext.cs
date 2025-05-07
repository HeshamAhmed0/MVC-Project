using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.hesham.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.hesham.DAL.Data.DbContexts
{
    public class CompanyDbContext :DbContext
    {
        public CompanyDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
