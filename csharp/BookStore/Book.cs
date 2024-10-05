namespace BookStore
{

    public enum SubscriptionType
    {
        Daily,
        Weekly,
        Monthly
    }


    public class Book
    {
        public string Isbn { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public double Price;


        public Book(string isbn, string name, string publisher, double price)
        {
            Isbn = isbn;
            Name = name;
            Publisher = publisher;
            Price = price;
        }


        public override string ToString()
        {
            return $"{nameof(Isbn)}: {Isbn}, {nameof(Name)}: {Name}, {nameof(Publisher)}: {Publisher}, {nameof(Price)}: {Price}";
        }

        public virtual void Read()
        {

            Console.Write("Enter ISBN: ");
            Isbn = Console.ReadLine();

            Console.Write("Enter Name: ");
            Name = Console.ReadLine();

            Console.Write("Enter Publisher: ");
            Publisher = Console.ReadLine();

            Console.Write("Enter Price: ");
            if (double.TryParse(Console.ReadLine(), out double price))
            {
                Price = price;
            }

        }

    }
}
