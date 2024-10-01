namespace MyNamespace
{
    public class MyClass
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Hello, World!");

            List<Courses> courses = new List<Courses>
            {
                new Courses() { Name = "C#", Category = "Programming" },
                new Courses() { Name = "Java", Category = "Programming" },
                new Courses() { Name = "Python", Category = "Programming" },
                new Courses() { Name = "Cisco", Category = "Networking" },
            };

            // sort
            IEnumerable<Courses> sortedCourses = courses.Where(x => x.Category == "Programming").OrderBy(x => x.Name);

            foreach (Courses course in sortedCourses)
            {
                Console.WriteLine(course);
            }

            // get by category method

            List<Courses> programming = GetByCategory(courses, "Programming");
            foreach (Courses course in programming)
            {
                Console.WriteLine(course);
            }

            List<Courses> networking = GetByCategory(courses, "Networking");
            foreach (Courses course in networking)
            {
                Console.WriteLine(course);
            }
        }


        public static List<Courses> GetByCategory(List<Courses> courses, string category){

            IEnumerable<Courses> filteredCourses = courses.Where(x => x.Category == category);

            return filteredCourses.ToList();
        }
    }
}