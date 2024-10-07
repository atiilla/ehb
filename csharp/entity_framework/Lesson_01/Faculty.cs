using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lesson_01
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Students> Students { get; set; } = new List<Students>();

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
        public Faculty() { }

    }
}
