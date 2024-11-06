using assignment3.Data;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Security.Cryptography;
using assignment3;

namespace assignment3
{

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (AuthenticateUser(username, password))
            {
                // Open the main window and close the login window
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Username == username);
                if (user != null)
                {
                    string passwordHash = ComputeHash(password);
                    return user.PasswordHash == passwordHash;
                }
            }
            return false;
        }

        private string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}