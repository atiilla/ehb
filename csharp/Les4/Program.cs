namespace MyNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Title = "Main Menu";

            MenuItem menuItem = new MenuItem(1, "Edit");
            menu.AddMenuItem(menuItem);
            MenuItem menuItem2 = new MenuItem(2, "Delete");
            menu.AddMenuItem(menuItem2);
            MenuItem menuItem3 = new MenuItem(3, "Add");
            menu.AddMenuItem(menuItem3);
            MenuItem menuItem4 = new MenuItem(4, "Exit");
            menu.AddMenuItem(menuItem4);

            menu.ExecuteGetInput();
        }
    }
}