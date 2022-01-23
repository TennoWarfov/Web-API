using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Models
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // create a new DB in first call
        }
    }
}
