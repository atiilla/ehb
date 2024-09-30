using System;
using System.Globalization;
using System.Threading;

namespace AlarmApp
{
    internal class Alarm
    {
        delegate void WakeUpMethod();
        static Timer timer;
        static TimeSpan alarmTime;
        static TimeSpan snoozeDuration = TimeSpan.FromMinutes(1); // Default snooze duration is 5 minutes
        static bool isSnoozing = false;
        static WakeUpMethod wakeUpMethods;

        public void SnozeAlarm()
        {
            int snoozeDurationInMinutes = 3;
            snoozeDuration = TimeSpan.FromMinutes(snoozeDurationInMinutes);
            isSnoozing = true;
            Console.WriteLine("Alarm snoozed for {0} minutes.", snoozeDurationInMinutes);

            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }


        public void SetAlarmTime()
        {
            Console.WriteLine("Enter the time for the alarm (HH:MM): ");
            string time = Console.ReadLine();
            wakeUpMethods = null;

            if (TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan setTime))
            {
                alarmTime = setTime;
                Console.WriteLine("Alarm time set successfully. {0}", alarmTime);

                Console.WriteLine("Now you need to choose a wake-up method:");
                Console.WriteLine("1. Play sound");
                Console.WriteLine("2. Show message");
                Console.WriteLine("3. Blink text");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        wakeUpMethods += new WakeUpMethods().PlaySound; // Invoke the wake-up method
                        break;
                    case 2:
                        wakeUpMethods += new WakeUpMethods().ShowMessage;
                        break;
                    case 3:
                        wakeUpMethods += new WakeUpMethods().BlinkText;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                        break;
                }

            }
            else
            {
                Console.WriteLine("Invalid time format. Please enter time in HH:MM format");
            }

            Console.WriteLine("Your alarm successfully set. {0}", alarmTime);
            Console.WriteLine("Press any key to go back to the main menu.");
            Console.ReadKey(); // Wait for the user to press any key
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

        public void StartAlarm()
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
    }
}