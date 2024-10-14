using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Group_Budget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            BudgetContext context = App.Context;
            InitializeComponent();
            dgPersons.ItemsSource = context.Persons.ToList();
            //dgPersons.ItemsSource = context.Persons.Where(p => p.Deleted > DateTime.Now).ToList();
            //dgPersons.ItemsSource = (from p in context.Persons
            //                         where p.Deleted > DateTime.Now
            //                         select new
            //                         {
            //                             p.Name,
            //                             p.FirstName,
            //                             p.LastName
            //                         }).ToList();

            // columnWidth
            //dgPersons.Width = context.Persons.Count() * 100; // 100 is the width of each column
        }

        private void dgPersons_GetFocus(object sender, RoutedEventArgs e)
        {
            dgPersons.Columns[0].Visibility = Visibility.Collapsed;
            dgPersons.Columns[1].Width = 150;
            dgPersons.Columns[2].Width = 150;
            dgPersons.Columns[3].Width = 150;

            dgPersons.UpdateLayout();
        }
    }
}