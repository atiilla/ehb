using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using assignment3.Models;
using assignment3.Commands;
using assignment3.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Microsoft.VisualBasic;

namespace assignment3.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Item> Items { get; set; }

        public ICommand ShowMessageCommand { get; }
        public ICommand AddCategoryCommand { get; }
        public ICommand AddItemCommand { get; }
        public ICommand UpdateItemCommand { get; }
        public ICommand DeleteItemCommand { get; }

        private string _newItemName;
        public string NewItemName
        {
            get => _newItemName;
            set
            {
                if (_newItemName != value)
                {
                    _newItemName = value;
                    OnPropertyChanged(nameof(NewItemName));
                }
            }
        }

        private ObservableCollection<Item> _filteredItems;
        public ObservableCollection<Item> FilteredItems
        {
            get => _filteredItems;
            set
            {
                _filteredItems = value;
                OnPropertyChanged(nameof(FilteredItems));
            }
        }

        public ViewModel()
        {
            Categories = new ObservableCollection<Category>();
            Items = new ObservableCollection<Item>();
            FilteredItems = new ObservableCollection<Item>();

            ShowMessageCommand = new RelayCommand(ShowMessage);
            AddCategoryCommand = new RelayCommand(AddCategory);
            AddItemCommand = new RelayCommand(AddItem);
            UpdateItemCommand = new RelayCommand(UpdateItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);

            _context = new AppDbContext();

            LoadCategoriesAndItemsAsync();
        }

        private void ShowMessage(object obj)
        {
            MessageBox.Show("Action performed!");
        }

        private async void AddCategory(object obj)
        {
            string newCategoryName = Interaction.InputBox("Enter new category name", "Add category", "New category");

            if (string.IsNullOrWhiteSpace(newCategoryName))
            {
                MessageBox.Show("Category name cannot be empty!");
                return;
            }

            try
            {
                var newCategory = new Category { Name = newCategoryName };

                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();

                await LoadCategoriesAndItemsAsync();

                MessageBox.Show("Category added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding category: {ex.Message}");
            }
        }

        private async void AddItem(object obj)
        {
            if (string.IsNullOrWhiteSpace(NewItemName))
            {
                MessageBox.Show("Task name cannot be empty!");
                return;
            }

            if (SelectedCategory == null || SelectedCategory.Name == "Select Category")
            {
                MessageBox.Show("Please select a category for the new task.");
                return;
            }

            try
            {
                var newItem = new Item
                {
                    Name = NewItemName,
                    Category = SelectedCategory
                };

                _context.Items.Add(newItem);
                await _context.SaveChangesAsync();

                await LoadCategoriesAndItemsAsync();

                MessageBox.Show("Task added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding task: {ex.Message}");
            }
        }

        private void UpdateItem(object obj)
        {
            if (obj is not Item selectedItem) return;

            string updatedName = Interaction.InputBox("Enter new Task Name", "Update Item", selectedItem.Name);
            if (string.IsNullOrWhiteSpace(updatedName)) return;

            try
            {
                selectedItem.Name = updatedName;

                _context.Items.Update(selectedItem);
                _context.SaveChangesAsync();

                LoadCategoriesAndItemsAsync();
                MessageBox.Show("Task updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating task: {ex.Message}");
            }
        }

        private async void DeleteItem(object obj)
        {
            MessageBox.Show("Delete Item Command Triggered!");  // Debugging message

            if (obj is not Item selectedItem)
            {
                MessageBox.Show("No item selected.");
                return;
            }

            try
            {
                // Check if the item exists in the database
                var itemToDelete = await _context.Items.FindAsync(selectedItem.Id);
                if (itemToDelete != null)
                {
                    _context.Items.Remove(itemToDelete);
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Task deleted!");

                    // Update the UI
                    Items.Remove(itemToDelete);
                    FilteredItems.Remove(itemToDelete);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting task: {ex.Message}");
            }
        }



        private bool CanExecuteUpdateDelete(object obj)
        {
            // Return true if an Item is selected, otherwise false
            return obj is Item selectedItem && selectedItem != null;
        }

        private async Task LoadCategoriesAndItemsAsync()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                var items = await _context.Items.ToListAsync();

                Categories.Clear();

                foreach (var category in categories)
                {
                    Categories.Add(category);
                }

                Items.Clear();
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                FilterItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void FilterItems()
        {
            if (SelectedCategory == null)
            {
                FilteredItems = new ObservableCollection<Item>(Items);
            }
            else
            {
                FilteredItems = new ObservableCollection<Item>(Items.Where(i => i.Category.Id == SelectedCategory.Id));
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));

                FilterItems();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
