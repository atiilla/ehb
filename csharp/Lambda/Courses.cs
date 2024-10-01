namespace MyNamespace
{
    public class Courses
    {
        string name;
        string category;

        // get and set

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

       

        public Courses()
        {
        }

        public override string ToString()
        {
            return name + " " + category;
        }

    }
}