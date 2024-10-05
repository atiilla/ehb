using System;

namespace BookStore
{
    // Delegate for Order event handling
    public delegate void OrderPlacedEventHandler(string message);

    // Generic Order class
    public class Order<T>
    {
        // Event to confirm order placement
        public event OrderPlacedEventHandler OrderPlaced;

        //  unique ID for each order
        private static int _idCounter = 0;

        private int _id;
        private T _item;
        private int _quantity;
        private double _price;

        public int ID
        {
            get => _id;
            // generate a unique ID for each order
            private set => _id = ++_idCounter;
        }

        public T Item
        {
            get => _item;
            set
            {
                _item = value;
                // Set price based on the item type, ensuring price is between €5 and €50 for books
                if (value is Book book)
                {
                    Price = Math.Clamp(book.Price, 5.0, 50.0);
                }
            }
        }

        public DateTime Date { get; set; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Quantity must be greater than zero.");
                }
                _quantity = value;
            }
        }

        public Tuple<DateTime, int> Period { get; set; }

        
        public double Price
        {
            get => _price;
            set
            {
                if (Item is Book)
                {
                    _price = Math.Clamp(value, 5.0, 50.0); // between €5 and €50 for books
                }
                else
                {
                    _price = value;
                }
            }
        }

        // Constructor for the Order class
        public Order(T item, DateTime date, int quantity, Tuple<DateTime, int> period = null)
        {
            ID = _idCounter; //  unique ID
            Item = item;
            Date = date;
            Quantity = quantity;
            Period = period;

            // Trigger the event when a new book is ordered
            if (item is Book book)
            {
                OnOrderPlaced($"Order placed for ISBN {book.Isbn}."); // Include ISBN in event message
            }
        }

        // Event handler to trigger the event
        protected virtual void OnOrderPlaced(string message)
        {
            OrderPlaced?.Invoke(message);
        }

        // Method a Tuple with ISBN, number of copies, and total price for books
        // tuple is used to store different types of data.
        public Tuple<string, int, double> GetOrderSummary()
        {
            if (Item is Book book)
            {
                return new Tuple<string, int, double>(book.Isbn, Quantity, Price * Quantity);
            }
            return null;
        }

        public override string ToString()
        {
            string periodString = Period != null ? $"{Period.Item1.ToShortDateString()}, {Period.Item2}" : "N/A";
            return $"Order ID: {ID}, Item: {Item}, Date: {Date.ToShortDateString()}, Quantity: {Quantity}, Period: {periodString}, Total Price: {Price * Quantity:C}";
        }
    }
}
