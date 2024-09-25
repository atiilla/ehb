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
namespace Wekker
{
    internal class Alarm{
        public bool IsRunning { get; set; }
        public bool IsSnoozed { get; set; }
        public bool IsStopped { get; set; }
        public int snoozedMinutes=3;

        public void Start()
        {
            IsRunning = true;
            IsStopped = false;
            IsSnoozed = false;
        }

        public void Stop()
        {
            IsRunning = false;
            IsStopped = true;
            IsSnoozed = false;
        }

        public void Snooze()
        {
            IsRunning = false;
            IsStopped = false;
            IsSnoozed = true;
        }

        public void StopSnooze()
        {
            IsRunning = true;
            IsStopped = false;
            IsSnoozed = false;
        }
    }
}