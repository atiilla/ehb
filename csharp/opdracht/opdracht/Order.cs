namespace opdracht
{
    public class Order<T> where T : class // T can be Book or Magazine
    {
        public int Id { get; private set; }
        public T Item { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public AppearancePeriod? Period { get; set; } // appearance period of book or magazine

        public Order(int id, T item, DateTime date, int quantity)
        {
            Id = id;
            Item = item;
            Date = date;
            Quantity = quantity;
        }

        public void TupleOrder()
        {
            if (Item is Book book) // check if item is a book
            {
                Console.WriteLine($"ISBN: {book.Isbn}, Quantity: {Quantity}, Price: {(decimal)Quantity * book.Price:C}");
            }
            else if (Item is Magazine magazine)
            {
                Console.WriteLine($"ISBN: {magazine.Isbn}, Quantity: {Quantity}, Price: {(decimal)Quantity * magazine.Price:C}");
            }
        }

        public event EventHandler OrderPlaced; // event that will be raised when order is placed

        public void PlaceOrder()
        {
            TupleOrder(); // display order details
            OrderPlaced?.Invoke(this, EventArgs.Empty); // raise event
        }
    }
}