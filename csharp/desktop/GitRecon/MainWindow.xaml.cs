﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GitRecon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void queryFindEmail(object sender, RoutedEventArgs e)
        {
            // testing clickhandler and button
            MessageBox.Show("Button clicked");
            string username = txUsername.Text;
            MessageBox.Show(username);
        }

        private void queryFindUsername(object sender, RoutedEventArgs e)
        {
            // testin method for button click
            MessageBox.Show("Button clicked");
        }
    }
}