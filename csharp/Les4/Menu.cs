namespace MyNamespace
{
    internal class Menu
    {
        public string Title = "";
        List<MenuItem> menuItems = new List<MenuItem>();

        public void Execute()
        {
            Console.WriteLine(Title);

            foreach (var item in menuItems)
            {
                Console.WriteLine("{0}. {1}", item.Id, item.MenuText);
            }
        }

        public void ExecuteGetInput()
        {
            Console.ResetColor();

            string userChoice;
            do
            {
                // Set the background and text color for the prompt
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Select the number of your choice:");
                // Reset colors after the message
                Console.ResetColor();

                // Space break
                Console.WriteLine();

                // Execute() method call
                Execute();

                // Read the user's input
                userChoice = Console.ReadLine();

                int choice;

                // Check for valid input
                if (!int.TryParse(userChoice, out choice) || choice > menuItems.Count || choice < 1)
                {
                    // Set the colors for error message
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Wrong input");
                    // Reset after displaying the message
                    Console.ResetColor();
                }
                else
                {
                    // Set the colors for successful selection
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("You selected: {0} and MenuItem is {1}\n", userChoice, menuItems[choice - 1].MenuText);
                    // Reset after displaying the message
                    Console.ResetColor();
                }

            } while (true);

        }

        public void AddMenuItem(MenuItem menuItem)
        {
            menuItems.Add(menuItem);
        }


    }

    internal class MenuItem
    {
        public int Id = 0;
        public string MenuText = "";

        public MenuItem(int id, string menuText)
        {
            Id = id;
            MenuText = menuText;
        }
    }


}