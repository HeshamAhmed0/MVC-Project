using Company.BLL;
using Company.BLL.Interfaces;
using Company.BLL.Reposatories;
using Company.hesham.DAL.Data.DbContexts;
using Company.hesham.DAL.Models;
using Company.hesham.PL.Mapping.DepartmentMapping;
using Company.hesham.PL.Mapping.EmployeeMapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.hesham.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IDepatmenReposatory,DepartmentReposatory>();
            //builder.Services.AddScoped<IEmployeeReposatorycs,EmployeeReposatory>(); //Allow Dependancy injection For EmployeeReposatory
            builder.Services.AddScoped<IUnionOfWork, UnionOWork>();
            builder.Services.AddAutoMapper(typeof(EmployeeToCreateEmployee));
            builder.Services.AddAutoMapper(typeof(EmployeeToEditEmployee));
            builder.Services.AddAutoMapper(typeof(CreateDepartmentDtoToDepartmen));
            builder.Services.AddAutoMapper(typeof(DeleteDepartmentDtoToDepartment));
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>();
            
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Department}/{action=GetAll}/{id?}");

            app.Run();
        }
    }
}
