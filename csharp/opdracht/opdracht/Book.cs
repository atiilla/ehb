namespace opdracht
{
    using System;
    using System.Collections.Generic;

    public enum AppearancePeriod // enum for appearance period of books and magazines
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
        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 5 || value > 50) // check if price is between 5€ and 50€
                    throw new ArgumentException("Price must be between 5€ and 50€");
                _price = value;
            }
        }

        public Book(string isbn, string name, string publisher, decimal price)
        {
            Isbn = isbn;
            Name = name;
            Publisher = publisher;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Isbn} - {Name} ({Publisher}) - {Price:C}"; // return book details in format: ISBN - Name (Publisher) - Price
        }

        public void Read()
        {
            Console.WriteLine($"Reading book: {Isbn}");
        }
    }
}