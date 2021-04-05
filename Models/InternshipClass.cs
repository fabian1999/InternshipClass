using System;
using System.Collections.Generic;

namespace RazorMvc.Models
{
    public class InternshipClass
    {
        private readonly List<Intern> _members;
        

        public InternshipClass()
        {
            _members = new List<Intern>() {
                new Intern { Id = 1, Name = "Fabian", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Id = 2, Name = "Teo", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Id = 3, Name = "Bobi", DateOfJoin = DateTime.Parse("2021-03-31") },
            };

        }

        public IList<Intern> Members
        {
            get { return _members; }
        }
    }
}
