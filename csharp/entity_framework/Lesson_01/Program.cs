using Microsoft.EntityFrameworkCore;
using Lesson_01;

namespace MyNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using (var context = new MyDbContext())
            {
                // Ensure the database is created
                context.Database.EnsureCreated();

                // Create and add faculties first
                var csFaculty = new Faculty { Name = "Computer Science" };
                var artsFaculty = new Faculty { Name = "Arts" };

                context.Faculties.Add(csFaculty);
                context.Faculties.Add(artsFaculty);
                context.SaveChanges(); // Save the faculties to the database

                // Now add students with valid FacultyId
                context.Students.Add(new Students { 
                    FirstName = "John", 
                    LastName = "Doe", 
                    BirthDate = new DateTime(1990, 1, 1), 
                    Gender = Gender.Male, 
                    Phone = "43434343434", 
                    FacultyId = csFaculty.Id // Assign valid FacultyId
                });
                context.Students.Add(new Students { 
                    FirstName = "Jane", 
                    LastName = "Doe", 
                    BirthDate = new DateTime(1995, 1, 1), 
                    Gender = Gender.Female, 
                    Phone = "0488888888", 
                    FacultyId = artsFaculty.Id // Assign valid FacultyId
                });
                context.Students.Add(new Students { 
                    FirstName = "Bob", 
                    LastName = "Smith", 
                    BirthDate = new DateTime(2000, 1, 1), 
                    Gender = Gender.Male, 
                    Phone = "048888834", 
                    SchoolName = "EHB", 
                    FacultyId = csFaculty.Id // Assign valid FacultyId
                });
                context.Students.Add(new Students { 
                    FirstName = "Sarah", 
                    LastName = "Doe", 
                    BirthDate = new DateTime(2005, 1, 1), 
                    Gender = Gender.Female, 
                    Phone = "048888", 
                    SchoolName = "EHB", 
                    FacultyId = csFaculty.Id // Assign valid FacultyId
                });

                context.SaveChanges(); // Save the students to the database
            }
        }
    }
}
