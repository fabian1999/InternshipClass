using RazorMvc.Models;
using System;

namespace RazorMvc.Services
{
    public class InternshipService
    {
        private readonly InternshipClass _internshipClass = new ();

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public int AddMember(Intern intern)
        {
            _internshipClass.Members.Add(intern);
            return intern.Id;
        }

        public void UpdateMember(Intern intern)
        {
            //_internshipClass.Members[];
        }

        public InternshipClass GetClass()
        {
            return _internshipClass;
        }

        internal Intern AddMember(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}