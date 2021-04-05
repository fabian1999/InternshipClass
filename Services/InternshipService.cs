using RazorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorMvc.Services
{
    public class InternshipService
    {
        private readonly InternshipClass _internshipClass = new ();

        public void RemoveMember(int id)
        {
            var itemToBeDeleted = _internshipClass.Members.Single(_ => _.Id == id);
            _internshipClass.Members.Remove(itemToBeDeleted);
        }

        public int AddMember(string memberName)
        {
            var maxId = _internshipClass.Members.Max(_ => _.Id);
            var newId = maxId + 1;

            var intern = new Intern()
            {
                Id = maxId + 1,
                Name = memberName,
                DateOfJoin = DateTime.Now,
            };

            _internshipClass.Members.Add(intern);
            return newId;
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

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }

        internal Intern AddMember(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}