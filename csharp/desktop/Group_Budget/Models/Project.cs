using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Budget.Models
{
   public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        public Decimal EstimatedBudget { get; set; } = 0;
        public DateTime Deleted { get; set; } = DateTime.MaxValue;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("Person")]
        public List<int> PersonId { get; set; }

        public Category Category { get; set; }
        public List<Person> People { get; set; }



    }
}
