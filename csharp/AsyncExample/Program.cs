using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace MyNamespace
{
    public class MyClass
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World! Test Test");

            // var stopDelay = Stopwatch.StartNew(); // Start the timer -> simulate delay in the web service

            string url = "http://10.2.89.25:3000/data";

            try
            {
                var stopDelay = new System.Diagnostics.Stopwatch();
                stopDelay.Start();
                string jsonData = await FetchDataAsync(url);
                stopDelay.Stop(); // Stop the timer
                Console.WriteLine($"Response time: {stopDelay.ElapsedMilliseconds} ms");
                Console.WriteLine(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

           
        }

        static async Task<string> FetchDataAsync(string url)
        {
            // client.Timeout = TimeSpan.FromSeconds(5); // Set a timeout to handle the delay
            string result = await client.GetStringAsync(url);
            return result;
        }
    }
}
