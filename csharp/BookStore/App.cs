using System;

namespace BookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create book and magazine objects
            Book book1 = new Book("123-456-789", "C# Programming", "Pearson", 25.99);
            Magazine magazine1 = new Magazine("987-654-321", "Tech Monthly", "Magazine House", 7.99, SubscriptionType.Monthly);

            // Create an order for the book
            Order<Book> bookOrder = new Order<Book>(book1, DateTime.Now, 3);
            bookOrder.OrderPlaced += message => Console.WriteLine(message); // Event subscription
            Console.WriteLine(bookOrder.GetOrderSummary()); // Output: (ISBN, Quantity, Total Price)

            // Create an order for the magazine
            Order<Magazine> magazineOrder = new Order<Magazine>(magazine1, DateTime.Now, 12, new Tuple<DateTime, int>(DateTime.Now, 12));
            Console.WriteLine(magazineOrder.ToString());

            // create another order

            Order<Magazine> magazineOrder2 = new Order<Magazine>(magazine1, DateTime.Now, 12, new Tuple<DateTime, int>(DateTime.Now, 12));
            Console.WriteLine(magazineOrder2.ToString());

            // create order for book

            Order<Book> bookOrder2 = new Order<Book>(book1, DateTime.Now, 3);
            Console.WriteLine(bookOrder2.ToString());

            // total price

            Console.WriteLine(magazineOrder.GetOrderSummary().Item3);
            Console.WriteLine(magazineOrder2.GetOrderSummary().Item3);
            Console.WriteLine(bookOrder2.GetOrderSummary().Item3);

        }
    }
}
