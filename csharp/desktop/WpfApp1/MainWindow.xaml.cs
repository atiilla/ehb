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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Label1.Content = "Hello World";
            //Label1.Width = 200;
            //TextBox textBox = new TextBox();
            //textBox.Text = "This is a TextBox";
            //textBox.Width = 200;
            //textBox.Height = 30;
            //textBox.Margin = new Thickness(10, 100, 10, 10);
            //MainGrid.Children.Add(textBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("This is test Button");
            //string username = usernameInput.Text;
            //Label1.MaxWidth = 300;

            //Label1.Content = "Hello " + username;

            string url = usernameInput.Text;
            string _url = URLFormatter(url);
            browser1.Navigate(_url);
        }

        void Browser_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            var browser = sender as WebBrowser;

            if (browser == null || browser.Document == null)
                return;

            dynamic document = browser.Document;

            if (document.readyState != "complete")
                return;

            dynamic script = document.createElement("script");
            script.type = @"text/javascript";
            script.text = @"window.onerror = function(msg,url,line){return true;}";
            document.head.appendChild(script);
        }

        public string URLFormatter(string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "http://" + url;
            }
            return url;
        }
    }
}