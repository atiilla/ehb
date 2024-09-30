using System;
using System.Globalization;
using System.Threading;

namespace AlarmApp
{

    internal class App
    {

        private static void Main(string[] args)
        {
            // new WakeUpSound().Invoke(2);
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Alarm Menu");
                Console.WriteLine("1. Set alarm time");
                Console.WriteLine("2. Start Alarm");
                Console.WriteLine("3. Stop Alarm");
                Console.WriteLine("4. Snooze");
                Console.WriteLine("5. Exit");
                Console.Write("Make a choice: ");

                // take user choice
                int choice = int.Parse(Console.ReadLine());

                // an instance of the Alarm class
                Alarm alarm = new Alarm();

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Set alarm time");
                        alarm.SetAlarmTime();
                        break;
                    case 2:
                        Console.WriteLine("Start Alarm");
                        alarm.StartAlarm();
                        break;
                    case 3:
                        Console.WriteLine("Stop Alarm");
                        break;
                    case 4:
                        Console.WriteLine("Snooze");
                        alarm.SnozeAlarm();
                        break;
                    case 5:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }

        }
    }
}