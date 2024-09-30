// using System;
// using System.Globalization;
// using System.Threading;

// namespace Wekker
// {
//     class Program
//     {
//         static System.Timers.Timer timer;
//         public delegate void Alarm();
//         static bool IsRunning = false;
//         static bool IsStopped = false;
//         static bool IsSnoozed = false;
//         static DateTime alarmTime;

//         static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
//         {
//             Console.WriteLine("Tick");
//         }

//         static void Countdown(DateTime time)
//         {
//             alarmTime = time;
//             var timeSpan = alarmTime - DateTime.Now;

//             if (timeSpan.TotalMilliseconds > 0)
//             {
//                 while (DateTime.Now < alarmTime || IsRunning)
//                 {
//                     // Get Belgium Time Zone
//                     TimeZoneInfo belgiumTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time"); // UTC+1 

//                     // Get the current Belgium time
//                     DateTime currentBelgiumTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgiumTimeZone);

//                     // Display remaining time
//                     timeSpan = alarmTime - currentBelgiumTime;
//                     Console.WriteLine($"Remaining time: {timeSpan.Hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s");

//                     System.Threading.Thread.Sleep(1000); // wait 1 second
//                 }

//                 Console.WriteLine("Alarm time has reached.");
//             }
//             else
//             {
//                 Console.WriteLine("Alarm time has already passed.");
//             }
//         }

//         static void StartSnooze()
//         {
//             IsSnoozed = true;
//             Console.WriteLine("Snoozed for 10 seconds...");
//             Thread.Sleep(10000); // wait 10 seconds
//             IsSnoozed = false;
//             Console.WriteLine("Snooze ended.");
//         }

//         static void Stop()
//         {
//             IsRunning = false;
//             IsStopped = true;
//         }

//         static void Start()
//         {
//             IsRunning = true;
//             IsStopped = false;
//             IsSnoozed = false;
//         }

//         static void Main(string[] args)
//         {
//             // Get Belgium Time Zone
//             TimeZoneInfo belgiumTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time"); // UTC+1 or UTC+2 depending on DST

//             // Get the current Belgium time
//             DateTime currentBelgiumTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgiumTimeZone);
//             Console.WriteLine($"Current Belgium Time: {currentBelgiumTime}");

//             // Set alarm time to 23:59:59 Belgium time
//             DateTime alarmBelgiumTime = new DateTime(currentBelgiumTime.Year, currentBelgiumTime.Month, currentBelgiumTime.Day, 23, 59, 59);

//             // alternative time conversion for Belgium
//             // alarmBelgiumTime.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("nl-BE"));

//             // Check if it's past, if yes, move to the next day
//             if (currentBelgiumTime > alarmBelgiumTime)
//             {
//                 alarmBelgiumTime = alarmBelgiumTime.AddDays(1); // Next day
//             }

//             Console.WriteLine($"Alarm Time: {alarmBelgiumTime}");

//             do
//             {
//                 Console.WriteLine("1. Show remaining time");
//                 Console.WriteLine("2. Snooze");
//                 Console.WriteLine("3. Stop");
//                 Console.WriteLine("4. Exit");
//                 string menu = Console.ReadLine();

//                 switch (menu)
//                 {
//                     case "1":
//                         if (IsSnoozed)
//                         {
//                             Console.WriteLine("Snoozed. Waiting for 10 seconds...");
//                         }
//                         else
//                         {
//                             // Get Belgium Time Zone
//                             TimeZoneInfo belgiumTimeZone2 = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time"); // UTC+1 

//                             // Get the current Belgium time
//                             DateTime currentBelgiumTime2 = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgiumTimeZone2);

//                             // Display remaining time
//                             var timeSpan = alarmBelgiumTime - currentBelgiumTime2;
//                             Console.WriteLine($"Remaining time: {timeSpan.Hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s");
//                         }
//                         break;
//                     case "2":
//                         StartSnooze();
//                         break;
//                     case "3":
//                         Stop();
//                         break;
//                     case "4":
//                         Environment.Exit(0);
//                         break;
//                     default:
//                         break;
//                 }
//             } while (true);
//         }
//     }
// }