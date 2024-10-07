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

namespace Containers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 4x4 grid using a Grid.
            Grid grid = new Grid();
            grid.ShowGridLines = true;
            grid.Margin = new Thickness(10);

            // Create columns.
            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                grid.ColumnDefinitions.Add(colDef);
            }

            // Create rows.
            for (int i = 0; i < 4; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                grid.RowDefinitions.Add(rowDef);
            }


            // odd rows and columns will have a different color.
            for (int i = 0;i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button btn = new Button();
                    btn.Content = string.Format("{0},{1}", i, j);
                    btn.Margin = new Thickness(5);
                    btn.Padding = new Thickness(10);
                    btn.Background = new SolidColorBrush((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0) ? Colors.LightGray : Colors.White);

                    // Set the row and column position of the button.
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    // Add the button to the grid.
                    grid.Children.Add(btn);
                }
            }


            // Add the grid to the window.
            this.Content = grid;

        }
    }
}