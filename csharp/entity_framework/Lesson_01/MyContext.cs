using Lesson_01;
using Microsoft.EntityFrameworkCore;

namespace MyNamespace
{
    public class MyDbContext : DbContext
    {        
        public DbSet<Students> Students { get; set; }

        // Constructor
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public MyDbContext()
        {
        }



        // Override db configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDb;Trusted_Connection=True;");
            }
        }
    }
}
