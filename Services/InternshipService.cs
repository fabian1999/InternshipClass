using RazorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var itemToBeUpdated = _internshipClass.Members.Single(_ => _.Id == intern.Id);
            itemToBeUpdated.Name = intern.Name;
        }

        public InternshipClass GetClass()
        {
            return _internshipClass;
        }

        public IList<string> GetMembers()
        {
            return _internshipClass.Members.Select(_ => _.Name).ToList();
        }

        internal Intern AddMember(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}