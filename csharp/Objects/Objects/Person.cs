using System;

namespace Objects
{
    internal class Person
    {
        private int id;
        private string name;

        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Constructor
        public Person(int id, string name)
        {
            Id = id; // Set Id
            Name = name; // Set Name
        }

        public override string ToString()
        {
            return "[" + "Name: " + Name + "]";
        }

        static void Main(string[] args)
        {
            var person1 = new Person(1, "Test");
            Console.WriteLine(person1);
            Console.WriteLine(args.Length);
        }
    }
}
