using Microsoft.EntityFrameworkCore;
using Student.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<StudentModel> Students { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Governorate> governorates { get; set; }
        public DbSet<Institute> institutes { get; set; }
        public DbSet<Nationality> nationalities { get; set; }
        public DbSet<Qualification> qualifications { get; set; }
    }
}
