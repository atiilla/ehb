using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_01
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Students
    {
        
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? Phone {  get; set; }
        public string? SchoolName  { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, BirthDate: {BirthDate}, Gender: {Gender}, Phone: {Phone}, SchoolName: {SchoolName}";
        }
    }
}
