using Microsoft.EntityFrameworkCore;
using PersonnalTrackingProject.Models;

namespace PersonnalTrackingProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Personnel> Personals { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Inouttime> Inouttimes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
