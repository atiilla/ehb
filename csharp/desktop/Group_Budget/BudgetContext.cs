using Group_Budget.Models;
using Microsoft.EntityFrameworkCore;
namespace Group_Budget
{
    public class BudgetContext : DbContext
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PeopleProjects> PersonProjects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            // Configure the connection to the database SQLServer
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Initial Catalog=GroupBudget; Integrated Security=True");
        }
    }
}
