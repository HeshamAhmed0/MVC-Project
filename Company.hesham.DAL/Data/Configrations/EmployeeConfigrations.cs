using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.hesham.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.hesham.DAL.Data.Configrations
{
    public class EmployeeConfigrations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.salary).HasColumnType("decimal(18,2)");
            builder.HasOne(D => D.Department)
                   .WithMany(E => E.Employees)
                   .HasForeignKey(D => D.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
