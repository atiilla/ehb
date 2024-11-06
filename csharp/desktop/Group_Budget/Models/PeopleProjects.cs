using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Budget.Models
{
    public class PeopleProjects
    {
        public int ProjectId { get; set; }
        public required Project Project { get; set; }

        public int PersonId { get; set; }
        public required Person Person { get; set; }
    }

}
