using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les3
{
    internal class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        static int follownumber = 0;

        public Project(string name, string description = "")
        {
            Id = follownumber++;
            Name = name;
            Description = description; 
        }

        public string EditDescription(string name, string description)
        {
            string oldName = Name;
            Name = name;
            Description = description + "aangepaste versie";
            return oldName;
        }

        public override string ToString() {
            return "Project = " + Name;
        }
    }
}
