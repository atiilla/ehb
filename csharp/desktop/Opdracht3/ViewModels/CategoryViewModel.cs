using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using Opdracht3.Models;
using Opdracht3.Commands;
using Opdracht3.Data;
using Microsoft.VisualBasic;

namespace Opdracht3.ViewModels
{
    public class CategoryViewModel
    {
        private readonly AppDbContext _context;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Item> Items { get; set; }

        public ICommand ShowMessageCommand { get; }
        public ICommand AddCategoryCommand { get; }

        public CategoryViewModel()
        {
            Categories = new ObservableCollection<Category>();
            Items = new ObservableCollection<Item>();

            ShowMessageCommand = new RelayCommand(ShowMessage);
            AddCategoryCommand = new RelayCommand(AddCategory);

            _context = new AppDbContext();
            Categories = new ObservableCollection<Category>(_context.Categories);
            Items = new ObservableCollection<Item>(_context.Items);
        }

        private void ShowMessage(object obj)
        {
            MessageBox.Show("Action performed!");
        }

        private void AddCategory(object obj)
        {
            // Ask the user for a new category name
            string newCategoryName = Interaction.InputBox("Enter new category name", "Add category", "New category");

            if (string.IsNullOrWhiteSpace(newCategoryName))
            {
                MessageBox.Show("Category name cannot be empty!");
                return;
            }

            // Create a new category object
            var newCategory = new Category { Name = newCategoryName };

            using (var context = new AppDbContext())
            {
                // Add the new category to the database
                context.Categories.Add(newCategory);
                context.SaveChanges();
            }

            // Refresh the Categories collection
            LoadCategories();

            MessageBox.Show("Category added!");
        }

        // Helper method to reload categories from the database
        private void LoadCategories()
        {
            using (var context = new AppDbContext())
            {
                Categories.Clear();
                foreach (var category in context.Categories.ToList())
                {
                    Categories.Add(category);
                }
            }
        }

    }
}
