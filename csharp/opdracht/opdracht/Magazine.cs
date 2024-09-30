namespace opdracht {
    public class Magazine : Book // inherit from Book
{
    public AppearancePeriod AppearancePeriod { get; set; }

    public Magazine(string isbn, string name, string publisher, decimal price, AppearancePeriod appearancePeriod)
        : base(isbn, name, publisher, price)
    {
        AppearancePeriod = appearancePeriod;
    }

    public override string ToString()
    {
        return base.ToString() + $" - {AppearancePeriod}";
    }

    public void Read()
    {
        Console.WriteLine($"Reading magazine: {Isbn}");
    }
}

}