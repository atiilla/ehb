using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Budget.Models
{
   public enum BudgetType
    {
        Income,
        Expense

    }
    public class Budget
    {
        public int Id { get; set; }

        [ForeignKey("Projects")]
        public int ProjectId { get; set; }
        public string Description { get; set; } = "";
        public double Amount { get; set; } = 0;
        public BudgetType BudgetType { get; set; } = BudgetType.Expense;
        public DateTime Date { get; set; } = DateTime.Now;
        public int PersonId { get; set; } = 1;

        public Person Person { get; set; }


    }
}
