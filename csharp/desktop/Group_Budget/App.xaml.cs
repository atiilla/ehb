using System.Configuration;
using System.Data;
using System.Windows;

namespace Group_Budget
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        static public BudgetContext Context = new BudgetContext();

        public App()
        {
            BudgetContext budgetContext = new BudgetContext();
            new Seeder(budgetContext);

        }
        


    }

}
