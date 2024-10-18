using System.Net.Http;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GitRecon
{
    public partial class MainWindow : Window
    {

        public class AccountResult
        {
            public string Network { get; set; }
            public string ProfileUrl { get; set; }
        }

        public class WebsiteInfo
        {
            public string ErrorType { get; set; }
            public string ErrorMessage { get; set; }
            public string Url { get; set; }
        }

        public class SocialAccount
        {
            public string ProfileUrl { get; set; }
        }


        private const string API_URL = "https://api.github.com";
        private static readonly Dictionary<string, string> HEADER = new Dictionary<string, string>

        {
            { "Accept", "application/vnd.github.v3+json" },
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.141 Safari/537.36" }
        };
        private static readonly int DELAY = 3000; // Delay 3 seconds to avoid rate limiting

        // Social Account Finder 

        
        private List<AccountResult> resultList = new List<AccountResult>();

        private readonly Dictionary<string, WebsiteInfo> websites = new Dictionary<string, WebsiteInfo>
        {
            {"About.me", new WebsiteInfo {ErrorType = "status_code", Url = "https://about.me/{}"}},
            {"Chess", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.chess.com/member/{}"}},
            {"DailyMotion", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.dailymotion.com/{}"}},
            {"Docker Hub", new WebsiteInfo {ErrorType = "status_code", Url = "https://hub.docker.com/u/{}"}},
            {"Duolingo", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Duolingo - Learn a language for free @duolingo", Url = "https://www.duolingo.com/profile/{}"}},
            {"Fiverr", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.fiverr.com/{}"}},
            {"Flickr", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.flickr.com/people/{}"}},
            {"GeeksforGeeks", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Login GeeksforGeeks", Url = "https://auth.geeksforgeeks.org/user/{}"}},
            {"Genius (Artists)", new WebsiteInfo {ErrorType = "status_code", Url = "https://genius.com/artists/{}"}},
            {"Genius (Users)", new WebsiteInfo {ErrorType = "status_code", Url = "https://genius.com/{}"}},
            {"Giphy", new WebsiteInfo {ErrorType = "status_code", Url = "https://giphy.com/{}"}},
            {"GitHub", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.github.com/{}"}},
            {"Imgur", new WebsiteInfo {ErrorType = "status_code", Url = "https://api.imgur.com/account/v1/accounts/{}?client_id=546c25a59c58ad7"}},
            {"Minecraft", new WebsiteInfo {ErrorType = "status_code", Url = "https://api.mojang.com/users/profiles/minecraft/{}"}},
            {"npm", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.npmjs.com/~{}"}},
            {"Pastebin", new WebsiteInfo {ErrorType = "status_code", Url = "https://pastebin.com/u/{}"}},
            {"Patreon", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.patreon.com/{}"}},
            {"PyPi", new WebsiteInfo {ErrorType = "status_code", Url = "https://pypi.org/user/{}"}},
            {"Reddit", new WebsiteInfo {ErrorType = "message", ErrorMessage = "\"error\": 404}", Url = "https://www.reddit.com/user/{}/about.json"}},
            {"Replit", new WebsiteInfo {ErrorType = "status_code", Url = "https://replit.com/@{}"}},
            {"Roblox", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.roblox.com/user.aspx?username={}"}},
            {"RootMe", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.root-me.org/{}"}},
            {"Scribd", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.scribd.com/{}"}},
            {"Snapchat", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.snapchat.com/add/{}"}},
            {"SoundCloud", new WebsiteInfo {ErrorType = "status_code", Url = "https://soundcloud.com/{}"}},
            {"SourceForge", new WebsiteInfo {ErrorType = "status_code", Url = "https://sourceforge.net/u/{}"}},
            {"Spotify", new WebsiteInfo {ErrorType = "status_code", Url = "https://open.spotify.com/user/{}"}},
            {"Steam", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Steam Community :: Error", Url = "https://steamcommunity.com/id/{}"}},
            {"Telegram", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<meta name=\"robots\" content=\"noindex, nofollow\">", Url = "https://t.me/{}"}},
            {"Tenor", new WebsiteInfo {ErrorType = "status_code", Url = "https://tenor.com/users/{}"}},
            {"TryHackMe", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>TryHackMe</title>", Url = "https://tryhackme.com/p/{}"}},
            {"Vimeo", new WebsiteInfo {ErrorType = "status_code", Url = "https://vimeo.com/{}"}},
            {"Wattpad", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.wattpad.com/user/{}"}},
            {"Wikipedia", new WebsiteInfo {ErrorType = "message", ErrorMessage = "(centralauth-admin-nonexistent:", Url = "https://en.wikipedia.org/wiki/Special:CentralAuth/{}?uselang=qqx"}},
            {"AllMyLinks", new WebsiteInfo {ErrorType = "status_code", Url = "https://allmylinks.com/{}"}},
            {"Buy Me a Coffee", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.buymeacoffee.com/{}"}},
            {"BuzzFeed", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.buzzfeed.com/{}"}},
            {"Cash APP", new WebsiteInfo {ErrorType = "status_code", Url = "https://cash.app/${}"}},
            {"Ebay", new WebsiteInfo {ErrorType = "message", Url = "https://www.ebay.com/usr/{}"}},
            {"Instagram", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.picuki.com/profile/{}"}},
            {"JsFiddle", new WebsiteInfo {ErrorType = "status_code", Url = "https://jsfiddle.net/user/{}/"}},
            {"Linktree", new WebsiteInfo {ErrorType = "message", ErrorMessage = "\"statusCode\":404", Url = "https://linktr.ee/{}"}},
            {"Medium", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<span class=\"fs\">404</span>", Url = "https://{}.medium.com/about"}},
            {"Pinterest", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title></title>", Url = "https://pinterest.com/{}/"}},
            {"Rapid API", new WebsiteInfo {ErrorType = "status_code", Url = "https://rapidapi.com/user/{}"}},
            {"TradingView", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.tradingview.com/u/{}/"}},
        };

        private int Checked = 0;
        private int Results = 0;

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text.Trim();
            if (string.IsNullOrEmpty(username) || username == "Enter username here...")
            {
                MessageBox.Show("Please enter a username to check.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SearchButton.IsEnabled = false;
            UsernameInput.IsEnabled = false;
            EmailsStatusTextBlock.Text = $"Checking username {username}";
            Checked = 0;
            Results = 0;
            SocialMediaResults.Items.Clear();  // Clear previous results

            Stopwatch stopwatch = Stopwatch.StartNew();
            var resultList = new List<AccountResult>();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(8);
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
                var tasks = new List<Task>();
                var semaphore = new SemaphoreSlim(10);  // You can modify the concurrency limit

                foreach (var website in websites)
                {
                    SocialAccountResult.Text = $"Checking {website.Key} for {username}...";
                    await semaphore.WaitAsync();
                    var task = Task.Run(async () =>
                    {
                        var result = await CheckWebsite(username, website.Key, website.Value, httpClient);
                      
                        if (result != null)
                        {
                            AddResult(result.ProfileUrl); // Pass only the ProfileUrl
                        }
                        Checked++;
                        Dispatcher.Invoke(() =>
                        {
                        });
                        semaphore.Release();
                    });
                    tasks.Add(task);
                }

                await Task.WhenAll(tasks);
                stopwatch.Stop();

                Results = SocialMediaResults.Items.Count; // Get the count after adding results

                if (Results == 0)
                {
                    SocialAccountResult.Text = $"No accounts found for {username}.";
                }
                else
                {
                    SocialAccountResult.Text = $"Found {Results} accounts for {username} in {stopwatch.Elapsed.TotalSeconds} seconds.";

                    // Clear UsernameInput
                    
                }

                SearchButton.IsEnabled = true;

                if (Results > 0)
                {
                    MessageBox.Show($"Found {Results} accounts for {username} in {stopwatch.Elapsed.TotalSeconds} seconds.", "Search Completed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            SearchButton.IsEnabled = true;
            UsernameInput.IsEnabled = true;
        }

        private void AddResult(string url)
        {
            Dispatcher.Invoke(() =>
            {
                if (string.IsNullOrEmpty(url))
                {
                    MessageBox.Show("Profile URL is null or empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create an instance of SocialAccount with the ProfileUrl
                var account = new SocialAccount
                {
                    ProfileUrl = url
                };

                // Add the account to the DataGrid
                SocialMediaResults.Items.Add(account); // Add to DataGrid
            });
        }

        private async Task<AccountResult> CheckWebsite(string username, string websiteName, WebsiteInfo websiteInfo, HttpClient httpClient)
        {
            try
            {
                var url = websiteInfo.Url.Replace("{}", username);
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return new AccountResult { ProfileUrl = url }; // Only return ProfileUrl
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            return null; // Return null if no valid account found
        }

        private void btnSocialFinderExport_Click(object sender, RoutedEventArgs e)
        {
            if (SocialMediaResults.Items.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Profile URL");

                foreach (SocialAccount account in SocialMediaResults.Items)
                {
                    sb.AppendLine(account.ProfileUrl);  // Only export the ProfileUrl
                }

                // File Save Dialog
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "CSV file (*.csv)|*.csv",
                    Title = "Export Profile URLs to CSV",
                    FileName = "profile_urls.csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    System.IO.File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                    MessageBox.Show("Profile URLs exported to CSV successfully.", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("No data available in the DataGrid.", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // End of Social Account Finder




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

            // Add token to headers if user provided
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

            // Check if "Ignore forks" checkbox is checked
            bool ignoreForks = isForksCheckBox.IsChecked ?? false;

            // Add token to headers if provided
            if (!string.IsNullOrEmpty(token))
            {
                HEADER["Authorization"] = $"token {token}";
            }

            // Get emails associated with the username
            List<(string Email, string Author)> emails = await GetEmailsByUsername(username, ignoreForks);
            if (emails != null && emails.Count > 0)
            {
                // Populate the DataGrid with results
                var emailResults = emails.Select(email => new { email.Email, email.Author }).ToList();
                ResultEmails.ItemsSource = emailResults;
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
                Dispatcher.Invoke(() =>
                {
                    Clipboard.Clear(); // Clear the clipboard before setting text

                    
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

        // These dll imports are required for clipboard operations
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

        // Method to get emails by username using GitHub API
        private async Task<List<(string Email, string Author)>> GetEmailsByUsername(string username, bool ignoreForks)
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
                        // Check if the repository is a fork then skip it
                        bool isFork = repo["fork"]?.GetValue<bool>() ?? false;
                        if (ignoreForks && isFork)
                        {
                            continue;  // We are skipping this repository
                        }

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


        // Logic to make API call with delay
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

        // Display message in the status text block
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
