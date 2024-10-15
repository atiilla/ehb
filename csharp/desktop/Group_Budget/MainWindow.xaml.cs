using Group_Budget.Migrations;
using Group_Budget.Models.ViewModels;
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
            //dgPersons.ItemsSource = context.Persons.ToList();
            //dgPersons.ItemsSource = context.Persons.Where(p => p.Deleted > DateTime.Now).ToList();
            dgPersons.ItemsSource = (from p in context.Persons
                                     where p.Deleted > DateTime.Now
                                     select new PersonDatagridViewModel(p)).ToList(); // Fix the LINQ query and add ToList()

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

            if(dgPersons.ItemsSource == null)
            {
                // init sample
                dgPersons.ItemsSource = new List<PersonDatagridViewModel>
                {
                    new PersonDatagridViewModel(new Person { Id = 1, FirstName = "John", LastName = "Doe" }),
                    new PersonDatagridViewModel(new Person { Id = 2, FirstName = "Jane", LastName = "Doe" }),
                    new PersonDatagridViewModel(new Person { Id = 3, FirstName = "John", LastName = "Smith" })
            };

                dgPersons.SelectedIndex = 0;
                dgPersons.UpdateLayout();

            }
        }

        private void dgPersons_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PersonDatagridViewModel person = (PersonDatagridViewModel)dgPersons.CurrentItem;
            if (person != null)
            {
                dgPersons.SelectedItem = person;
                txId.Text = person.Id.ToString();
                // formatted text - first name and last name
                txFirstName.Text = person.FirstName;
                txLastName.Text = person.LastName;
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            BudgetContext context = App.Context;
            Person newPerson = new Person
            {
                Name = "",
                FirstName = txFirstName.Text,
                LastName = txLastName.Text
            };
            context.Persons.Add(newPerson);
            context.SaveChanges();
            dgPersons.ItemsSource = (from p in context.Persons
                                     where p.Deleted > DateTime.Now
                                     select new PersonDatagridViewModel(p)).ToList();
            dgPersons.SelectedIndex = 0;
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            BudgetContext context = App.Context;
            PersonDatagridViewModel person = (PersonDatagridViewModel)dgPersons.SelectedItem;
            if (person != null)
            {
                Person updatePerson = context.Persons.Find(person.Id);
                updatePerson.FirstName = txFirstName.Text;
                updatePerson.LastName = txLastName.Text;
                context.SaveChanges();
                dgPersons.ItemsSource = (from p in context.Persons
                                         where p.Deleted > DateTime.Now
                                         select new PersonDatagridViewModel(p)).ToList();
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            BudgetContext context = App.Context;
            PersonDatagridViewModel person = (PersonDatagridViewModel)dgPersons.SelectedItem;
            if (person != null)
            {
                Person person1 = context.Persons.Find(person.Id);
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this person?", "Delete Person", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    person1.Deleted = DateTime.Now;
                    //context.Persons.Remove(person1); // avoid conflict in the database by removing the person1
                    context.Persons.Update(person1);
                    context.SaveChanges();
                    dgPersons.ItemsSource = (from p in context.Persons
                                             where p.Deleted > DateTime.Now
                                             select new PersonDatagridViewModel(p)).ToList();
                }
            }
        }
    }
}