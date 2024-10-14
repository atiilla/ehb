using System.Net.Http;
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

namespace WpfApp2_controls
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameInput.Text;
            string password = passwordInput.Text;
            string email = emailInput.Text;
            string payload = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\",\"email\":\"" + email + "\"}";

            string response = SendPayloadToJsonPlaceHolder(payload);
            MessageBox.Show(response);
            result.Text = response;
        }

        public static string SendPayloadToJsonPlaceHolder(string payload)
        {
            string url = "https://jsonplaceholder.typicode.com/users";
            using (var client = new HttpClient())
            {
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Data sent successfully");
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    MessageBox.Show("Error sending data");
                    return null;
                }
            }
        }
}
}