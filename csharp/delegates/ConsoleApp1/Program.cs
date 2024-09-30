using System;
using System.Globalization;
using System.Threading;

namespace MyNamespace
{
    internal class Program
    {
        delegate void WakeUpMethod();

        static Timer timer;
        static TimeSpan alarmTime;
        static TimeSpan snoozeDuration = TimeSpan.FromMinutes(5); // Default snooze
        static bool isSnoozing = false;
        static WakeUpMethod wakeUpMethods;

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Alarm Menu");
                Console.WriteLine("1. Set alarm time");
                Console.WriteLine("2. Set snooze time");
                Console.WriteLine("3. Start Alarm Clock");
                Console.WriteLine("4. Stop Alarm Clock");
                Console.WriteLine("5. Select wake up methods (sound, message, blink)");
                Console.WriteLine("0. Shut down");
                Console.Write("Make a choice: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        SetAlarmTime();
                        break;
                    case "2":
                        SetSnoozeTime();
                        break;
                    case "3":
                        StartAlarm();
                        break;
                    case "4":
                        StopAlarm();
                        break;
                    case "5":
                        SelectWakeUpMethods();
                        break;
                    case "0":
                        running = false;
                        StopAlarm();
                        break;
                }
            }
        }

        private static void SetAlarmTime()
        {
            Console.Write("Enter alarm time (HH:mm): ");
            string input = Console.ReadLine();
            if (TimeSpan.TryParseExact(input, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan setTime))
            {
                alarmTime = setTime;
                Console.WriteLine($"Alarm time set for {alarmTime}");
            }
            else
            {
                Console.WriteLine("Invalid time format. Please use HH:mm.");
            }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        private static void SetSnoozeTime()
        {
            Console.Write("Enter snooze duration in minutes: ");
            if (int.TryParse(Console.ReadLine(), out int minutes))
            {
                snoozeDuration = TimeSpan.FromMinutes(minutes);
                Console.WriteLine($"Snooze duration set to {minutes} minutes.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number of minutes.");
            }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        private static void StartAlarm()
        {
            if (alarmTime == default)
            {
                Console.WriteLine("Please set the alarm time first.");
                Console.ReadKey();
                return;
            }

            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan timeUntilAlarm = alarmTime - currentTime;
            if (timeUntilAlarm < TimeSpan.Zero) 
            {
                timeUntilAlarm = timeUntilAlarm.Add(TimeSpan.FromDays(1));
            }

            Console.WriteLine($"Alarm set. Will go off in {timeUntilAlarm.TotalMinutes:F2} minutes.");
            timer = new Timer(TriggerAlarm, null, timeUntilAlarm, TimeSpan.FromMilliseconds(-1)); 
            Console.ReadKey();
        }

        private static void StopAlarm()
        {
            if (timer != null)
            {
                timer.Dispose();
                Console.WriteLine("Alarm stopped.");
            }
            else
            {
                Console.WriteLine("No alarm is currently running.");
            }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        private static void TriggerAlarm(object state)
        {
            Console.Clear();
            Console.WriteLine("ALARM! Time to wake up!");

            wakeUpMethods?.Invoke(); 

            if (isSnoozing)
            {
                timer = new Timer(TriggerAlarm, null, snoozeDuration, TimeSpan.FromMilliseconds(-1)); // Snooze timer
                Console.WriteLine($"Snoozing for {snoozeDuration.TotalMinutes} minutes.");
            }
            else
            {
                StopAlarm();
            }
        }

        private static void SelectWakeUpMethods()
        {
            wakeUpMethods = null; 
            Console.WriteLine("Select wake-up methods (enter numbers separated by commas):");
            Console.WriteLine("1. Sound");
            Console.WriteLine("2. Message");
            Console.WriteLine("3. Blinking text");

            string[] choices = Console.ReadLine()?.Split(',');

            foreach (string choice in choices)
            {
                switch (choice.Trim())
                {
                    case "1":
                        wakeUpMethods += PlaySound;
                        break;
                    case "2":
                        wakeUpMethods += ShowMessage;
                        break;
                    case "3":
                        wakeUpMethods += BlinkText;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

            Console.WriteLine("Wake-up methods set.");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        private static void PlaySound()
        {
            Console.Beep(); 
            Console.WriteLine("Playing alarm sound...");
        }

        private static void ShowMessage()
        {
            Console.WriteLine("Wake up! It's time to start your day.");
        }

        private static void BlinkText()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Thread.Sleep(500); 
                Console.WriteLine("ALARM!!!");
                Thread.Sleep(500);
            }
        }
    }
}
