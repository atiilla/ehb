namespace AlarmApp
{
    public class WakeUpMethods
    {
        // Parameterless constructor
        public WakeUpMethods() { }

        // Public methods so they can be accessed from other classes
        public void PlaySound()
        {
            Console.Beep(); 
            Console.WriteLine("BEEEP!!!");
        }

        public void ShowMessage()
        {
            Console.WriteLine("Wake up! Soldiers, get up!");
        }

        public void BlinkText()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Thread.Sleep(500);
                Console.WriteLine("ALARM!!! WARNING!!! :D ");
                Thread.Sleep(500);
            }
        }
    }
}
