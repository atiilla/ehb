using System;
using System.Linq;
using Group_Budget.Models;
using Microsoft.EntityFrameworkCore;

namespace Group_Budget
{
    class Seeder
    {
        public Seeder(BudgetContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new Category { Name = "?", Description = "Improving the home", Deleted = DateTime.Now },
                    new Category { Name = "Road Trip", Description = "Traveling to different places by car", Deleted = DateTime.Now });
                context.SaveChanges();
            }

            if (!context.Projects.Any())
            {
                context.Projects.AddRange(new Project { Name = "Painting the house", Description = "Painting the house", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10), EstimatedBudget = 1000, CategoryId = 1 },
                    new Project { Name = "Trip to the beach", Description = "Trip to the beach", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10), EstimatedBudget = 1000, CategoryId = 2 });
                context.SaveChangesAsync();
            }

            if (!context.Persons.Any())
            {
                context.Persons.AddRange(new Person { Name = "John", FirstName = "John", LastName = "Doe", Deleted = DateTime.Now },
                    new Person { Name = "Jane", FirstName = "Jane", LastName = "Doe", Deleted = DateTime.Now });
                context.SaveChangesAsync();
            }

            if (!context.PersonProjects.Any())
            {
                var firstProject = context.Projects.FirstOrDefault();
                if (firstProject != null)
                {
                    int projectId = firstProject.Id;
                    context.PersonProjects.AddRange(
                        new PeopleProjects { PersonId = 1, ProjectId = projectId },
                        new PeopleProjects { PersonId = 2, ProjectId = projectId },
                        new PeopleProjects { PersonId = 1, ProjectId = projectId }
                    );
                    context.SaveChanges();
                }
            }

            if (!context.Budgets.Any())
            {
                context.Budgets.AddRange(new Budget { ProjectId = 1, Description = "Paint", Amount = 100, BudgetType = BudgetType.Expense, Date = DateTime.Now, PersonId = 1 },
                    new Budget { ProjectId = 2, Description = "Gas", Amount = 100, BudgetType = BudgetType.Expense, Date = DateTime.Now, PersonId = 2 });
                context.SaveChangesAsync();
            }
        }
    }
}
