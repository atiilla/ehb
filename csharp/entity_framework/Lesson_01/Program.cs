using Microsoft.EntityFrameworkCore;
using Lesson_01;
namespace MyNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using (MyDbContext context = new MyDbContext())
            {

                context.Students.Add(new Students() { FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Male, Phone="43434343434" });
                context.Students.Add(new Students() { FirstName = "Jane", LastName = "Doe", BirthDate = new DateTime(1995, 1, 1), Gender = Gender.Female, Phone = "0488888888" });
                context.Students.Add(new Students() { FirstName= "Bob", LastName = "Smith", BirthDate = new DateTime(2000, 1, 1), Gender = Gender.Male, Phone = "048888834", SchoolName="EHB" });
                context.SaveChanges();
            }
        }
    }
}