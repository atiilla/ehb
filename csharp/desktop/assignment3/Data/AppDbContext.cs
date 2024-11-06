using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using assignment3.Models;


namespace assignment3.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Opdracht3;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Productivity" },
                new Category { Id = 2, Name = "Games" }
            );
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Task Manager", CategoryId = 1 },
                new Item { Id = 2, Name = "Chess Game", CategoryId = 2 }
            );

            modelBuilder.Entity<User>().HasData(
        new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = ComputeHash("admin123")
        }
    );
        }

        private string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
