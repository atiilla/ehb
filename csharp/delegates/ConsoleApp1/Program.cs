// See https://aka.ms/new-console-template for more information
/*
Write the console applications "Wekker" for the use of delegates (and "Timer"):

Provide a repetitive menu with which you 

set the time when your alarm clock "runs off"
can set the slumber time after your alarm clock has ended
a "stop alarm" + snooze button
enable or disable the different ways of arousing
Provide for method of awakening at least

sound
a message
a flashing light
to be used separately or in combinations.  These may be written as "text" on the console.  But if "sound" or "flashing" can really be produced, that is of course an added value.

An absolute condition: Except for the "menu" Switch, no conditional statements (if / else) may be used.  So you will have to work strictly with delegates and Timer
*/


using System.Globalization;

namespace Wekker
{
    class Program
    {
        static System.Timers.Timer timer;
        public delegate void Alarm();
        static bool IsRunning = false;
        static bool IsStopped = false;
        static bool IsSnoozed = false;
        static DateTime alarmTime;

        static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Tick");
        }

        static void Countdown(DateTime time)
        {
            alarmTime = time;
            var timeSpan = alarmTime - DateTime.Now;




            if (timeSpan.TotalMilliseconds > 0)
            {
                timer = new System.Timers.Timer(1000); // 1 second interval
                timer.Elapsed += Timer_Elapsed;
                timer.Start();



                while (DateTime.Now < alarmTime)
                {

                    // Get Belgium Time Zone
                    TimeZoneInfo belgiumTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time"); // UTC+1 

                    // Get the current Belgium time
                    DateTime currentBelgiumTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgiumTimeZone);
                    Console.WriteLine($"Current Belgium Time: {currentBelgiumTime}");

                    // Display remaining time
                    timeSpan = alarmTime - currentBelgiumTime;
                    Console.WriteLine($"Remaining time: {timeSpan.Hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s");

                    // is Snoozed?
                    if (IsSnoozed){
                        Console.WriteLine("Snoozed");
                        // Sleep until next snooze 10 seconds
                        System.Threading.Thread.Sleep(10000);
                        IsSnoozed = false;

                    }
                    System.Threading.Thread.Sleep(1000); // wait 1 second
                }

                Console.WriteLine("Alarm time has reached.");
            }
            else
            {
                Console.WriteLine("Alarm time has already passed.");
            }
        }

        static void StartSnooze(){

            IsSnoozed = true;
        }

        static void StopSnooze(){
            IsSnoozed = false;
        }

        static void Stop(){
            IsRunning = false;
            IsStopped = true;
        }

        static void Start(){
            IsRunning = true;
            IsStopped = false;
            IsSnoozed = false;
        }

        static void Main(string[] args)
        {
            // Get Belgium Time Zone
            TimeZoneInfo belgiumTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time"); // UTC+1 or UTC+2 depending on DST

            // Get the current Belgium time
            DateTime currentBelgiumTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgiumTimeZone);
            Console.WriteLine($"Current Belgium Time: {currentBelgiumTime}");

            // Set alarm time to 23:59:59 Belgium time
            DateTime alarmBelgiumTime = new DateTime(currentBelgiumTime.Year, currentBelgiumTime.Month, currentBelgiumTime.Day, 23, 59, 59);

            // alternative time conversion for Belgium
           // alarmBelgiumTime.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("nl-BE"));

            // Check if it's past, if yes, move to the next day
            if (currentBelgiumTime > alarmBelgiumTime)
            {
                alarmBelgiumTime = alarmBelgiumTime.AddDays(1); // Next day
            }

            Console.WriteLine($"Alarm Time: {alarmBelgiumTime}");

            Countdown(alarmBelgiumTime);
        }
    }
}