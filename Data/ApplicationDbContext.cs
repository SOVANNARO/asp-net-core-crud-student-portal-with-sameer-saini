using crud_student_portal_with_sameer_saini.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace crud_student_portal_with_sameer_saini.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}