using Group_Budget.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Budget
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Deleted { get; set; } = DateTime.MaxValue;

        // many to many relationship
        public List<Project> Projects { get; set; }
    }


    public class PeopleProjects
    {
        public int Id { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
        public DateTime Deleted { get; set; } = DateTime.MaxValue;
    }
}
