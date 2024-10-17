using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Text;

namespace GitRecon
{
    public partial class MainWindow : Window
    {
        private const string API_URL = "https://api.github.com";
        private static readonly Dictionary<string, string> HEADER = new Dictionary<string, string>
        {
            { "Accept", "application/vnd.github.v3+json" },
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.141 Safari/537.36" }
        };
        private static readonly int DELAY = 3000; // Delay in milliseconds

        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for querying by email
        private async void btnUsernameQuery_Click(object sender, RoutedEventArgs e)
        {
            string token = txToken.Text;  // Get token from the TextBox
            string email = txEmail.Text;  // Get email from the TextBox

            // Display message if email is not provided
            if (string.IsNullOrEmpty(email))
            {
                DisplayMessage(UsernameStatusTextBlock, "Please enter an email.");
                return;
            }

            // Add token to headers if provided
            if (!string.IsNullOrEmpty(token))
            {
                HEADER["Authorization"] = $"token {token}";
            }

            // Get username associated with the email
            var username = await FindUserNameByEmail(email);
            if (!string.IsNullOrEmpty(username))
            {
                // Populate the DataGrid with results
                var results = new List<object> { new { Username = username } };
                ResultUsernames.ItemsSource = results;
                //DisplayMessage(UsernameStatusTextBlock, "Query successful.");
            }
            else
            {
                DisplayMessage(UsernameStatusTextBlock, "No username found or failed to fetch data.");
            }
        }

        // Event handler for querying by username
        private async void btnEmailQuery_Click(object sender, RoutedEventArgs e)
        {
            string token = txToken.Text;  // Get token from the TextBox
            string username = txUsername.Text; // Get username from the TextBox

            // Display message if username is not provided
            if (string.IsNullOrEmpty(username))
            {
                DisplayMessage(EmailsStatusTextBlock, "Please enter a username.");
                return;
            }

            // Add token to headers if provided
            if (!string.IsNullOrEmpty(token))
            {
                HEADER["Authorization"] = $"token {token}";
            }

            // Get emails associated with the username
            List<(string Email, string Author)> emails = await GetEmailsByUsername(username);
            if (emails != null && emails.Count > 0)
            {
                // Populate the DataGrid with results
                var emailResults = emails.Select(email => new { email.Email, email.Author }).ToList();
                ResultEmails.ItemsSource = emailResults;
                //DisplayMessage(EmailsStatusTextBlock, "Query successful.");
            }
            else
            {
                DisplayMessage(EmailsStatusTextBlock, "No emails found or failed to fetch data.");
            }
        }

        private void dgEmails_DoubleClick(object sender, MouseButtonEventArgs e)
{
    if (ResultEmails.SelectedItem != null)
    {
        var email = (dynamic)ResultEmails.SelectedItem;

        bool success = false;
        int retries = 3;

        while (!success && retries > 0)
        {
            try
            {
                // Ensure the clipboard is accessed on the UI thread
                Dispatcher.Invoke(() =>
                {
                    Clipboard.Clear(); // Clear the clipboard before setting text

                    // Using Win32 API to open and close the clipboard
                    if (OpenClipboard(IntPtr.Zero))
                    {
                        string rowClipboardData = "Email: " + email.Email + "\nAuthor: " + email.Author;
                        Clipboard.SetText(rowClipboardData);
                        CloseClipboard();
                        success = true;

                        MessageBox.Show("Email copied to clipboard.", "Email Copied", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
            }
            catch (Exception ex)
            {
                retries--;
                if (retries == 0)
                {
                    MessageBox.Show($"Failed to copy email to clipboard after multiple attempts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    System.Threading.Thread.Sleep(100); // Delay between retries
                }
            }
        }
    }
}

// P/Invoke declarations for OpenClipboard and CloseClipboard
[DllImport("user32.dll", SetLastError = true)]
static extern bool OpenClipboard(IntPtr hWndNewOwner);

[DllImport("user32.dll", SetLastError = true)]
static extern bool CloseClipboard();


        // Method to find username by email using GitHub API
        private async Task<string> FindUserNameByEmail(string email)
        {
            using HttpClient client = new HttpClient();
            var url = $"{API_URL}/search/users?q={email}";
            var result = await ApiCall(client, url);

            if (result != null && result is JsonObject obj && obj["total_count"]?.GetValue<int>() > 0)
            {
                var username = obj["items"]?[0]?["login"]?.ToString();
                return username;
            }

            return null;
        }

        // Method to get emails associated with a username
        private async Task<List<(string Email, string Author)>> GetEmailsByUsername(string username)
        {
            using HttpClient client = new HttpClient();
            var emails = new List<(string Email, string Author)>();
            var url = $"{API_URL}/users/{username}/repos?per_page=100";

            try
            {
                var result = await ApiCall(client, url);

                if (result is JsonArray repos)
                {
                    foreach (var repo in repos)
                    {
                        var repoName = repo["name"]?.ToString();
                        EmailsStatusTextBlock.Text = $"Fetching emails from {repoName}...";
                        var commitsUrl = $"{API_URL}/repos/{username}/{repoName}/commits?per_page=100";

                        var commits = await ApiCall(client, commitsUrl);

                        if (commits is JsonArray commitArray)
                        {
                            foreach (var commit in commitArray)
                            {
                                var author = commit["commit"]?["author"];
                                if (author != null)
                                {
                                    var email = author["email"]?.ToString();
                                    var authorName = author["name"]?.ToString();

                                    if (!string.IsNullOrEmpty(email) && !emails.Any(e => e.Email == email))
                                    {
                                        emails.Add((email, authorName));
                                    }
                                }
                            }
                        }
                    }

                    EmailsStatusTextBlock.Text = "Query successful.";
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(EmailsStatusTextBlock, $"Error occurred while fetching emails: {ex.Message}");
            }

            return emails;
        }

        // API call method to send a request and return the result
        private async Task<JsonNode> ApiCall(HttpClient client, string url)
        {
            await Task.Delay(DELAY);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            foreach (var header in HEADER)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonNode.Parse(content);
        }

        // Helper method to display message in the corresponding TextBlock
        private void DisplayMessage(TextBlock statusTextBlock, string message)
        {
            statusTextBlock.Text = message;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (ResultEmails.ItemsSource != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Email,Author");

                foreach (var item in ResultEmails.ItemsSource)
                {
                    dynamic email = item;
                    sb.AppendLine($"{email.Email},{email.Author}");
                }

                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "CSV file (*.csv)|*.csv",
                    Title = "Export Emails to CSV",
                    FileName = "emails.csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    System.IO.File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                    MessageBox.Show("Emails exported to CSV successfully.", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }
        }
    }
}
