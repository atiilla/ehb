namespace opdracht
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book1 = new Book("978-90-232-1456-7", "Harry Potter en de Steen der Wijzen", "Scholastica", 29.99m);
            Magazine magazine1 = new Magazine("000-123-456-789", "NRC Handelsblad", "Reijnst-Dijkstra Media", 5.99m, AppearancePeriod.Daily);

            Order<Book> bookOrder = new Order<Book>(1, book1, DateTime.Now, 1);
            bookOrder.OrderPlaced += (sender, e) => Console.WriteLine("Book order placed!");
            bookOrder.PlaceOrder();

            Order<Magazine> magazineOrder = new Order<Magazine>(2, magazine1, DateTime.Now, 1);
            magazineOrder.OrderPlaced += (sender, e) => Console.WriteLine("Magazine order placed!");
            magazineOrder.PlaceOrder();
        }
    }
}