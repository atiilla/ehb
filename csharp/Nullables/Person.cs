using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullables
{
    internal class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public Person(string name)
        {
            this.Name = name;
        }

        public string getAge() => (DateTime.Now - BirthDate).ToString();

        public override string ToString() => Name;

    }

    internal class Student : Person
    {
        public long studentNumber;
        public Student(string name):base(name) { }

        

        public override string ToString() => base.ToString() + " " + studentNumber;
    }
}
