﻿using Group_Budget.Migrations;
using Group_Budget.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        private List<Person> peopleList;
        public MainWindow()
        {
            BudgetContext context = App.Context;
            InitializeComponent();

            //peopleList = context.Persons.ToList(); // Initialize peopleList with the list of persons from the context
            
            //dgPersons.ItemsSource = context.Persons.ToList();
            //dgPersons.ItemsSource = context.Persons.Where(p => p.Deleted > DateTime.Now).ToList();
            dgPersons.ItemsSource = (from p in context.Persons
                                     where p.Deleted > DateTime.Now
                                     select new PersonDatagridViewModel(p)).ToList(); // Fix the LINQ query and add ToList()

            dgProjects.ItemsSource = context.Projects
         .Include(p => p.Category)
         .Include(p => p.People)  // Load related people
         .Select(p => new
         {
             p.Id,
             p.Name,
             p.Description,
             p.StartDate,
             p.EndDate,
             p.EstimatedBudget,
             CategoryName = p.Category.Name,  // Display the category name
             // Use PersonId to find names from the Person table
             PeopleNames = string.Join(", ",
                 context.Persons.Select(pp => new { pp.Id, pp.FirstName, pp.LastName })
                 .Where(pp => p.People.Any(ppp => ppp.Id == pp.Id))
                 .Select(pp => $"{pp.FirstName} {pp.LastName}")
                 )

         })
         .ToList();



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

        private void txSearch_Enter(object sender, RoutedEventArgs e)
        {
            txSearch.Text = "";
        }

        private void txSearch_Leave(object sender, RoutedEventArgs e)
        {
            if (txSearch.Text == "")
            {
                txSearch.Text = "Search";
            }
        }





        private void txSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.Context == null || App.Context.Persons == null || dgPersons == null)
            {
                // Handle the null case appropriately, e.g., show an error message or log the issue
                return;
            }

            string searchText = txSearch.Text.ToLower(); // Get the search text and convert it to lowercase for case-insensitive search

            dgPersons.ItemsSource = (from p in App.Context.Persons
                                     where p.Deleted > DateTime.Now &&
                                     (p.FirstName.ToLower().Contains(searchText) || p.LastName.ToLower().Contains(searchText))
                                     select new PersonDatagridViewModel(p)).ToList(); // Fix the LINQ query and add ToList()

            dgPersons.SelectedIndex = 0;
            dgPersons.UpdateLayout();
        }


    }
}