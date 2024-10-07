using Lesson_01;
using Microsoft.EntityFrameworkCore;

namespace MyNamespace
{
    public class MyDbContext : DbContext
    {        
        public DbSet<Students> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        // Constructor
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
                .HasOne<Faculty>(s => s.Faculty)
                .WithMany(f => f.Students)
                .HasForeignKey(s => s.FacultyId);
        }
    }
}
