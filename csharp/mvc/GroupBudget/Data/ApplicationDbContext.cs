using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GroupBudget.Models;

namespace GroupBudget.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GroupBudget.Models.Project> Projects { get; set; } = default!;
        public DbSet<GroupBudget.Models.Category> Categories { get; set; } = default!;
    }
}
