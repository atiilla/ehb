namespace BookStore{

    public class Magazine : Book{

        public SubscriptionType Period { get; set; }
        
        public Magazine(string isbn, string name, string publisher, double price, SubscriptionType period)
            : base(isbn, name, publisher, price)
        {
            Period = period;
        }

        public override string ToString()
        {
            return base.ToString() + $", {nameof(SubscriptionType)}: {Period}";
        }

        public override void Read()
        {
            base.Read(); // call base class method
            Console.Write("Enter SubscriptionType: ");
            Period = (SubscriptionType)Enum.Parse(typeof(SubscriptionType), Console.ReadLine());
        }

    }
}