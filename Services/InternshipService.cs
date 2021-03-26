﻿using RazorMvc.Models;

namespace RazorMvc.Services
{
    public class InternshipService
    {
        private readonly InternshipClass _internshipClass = new ();

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public string AddMember(string member)
        {
            _internshipClass.Members.Add(member);
            return member;
        }

        public void UpdateMember(int index, string name)
        {
            _internshipClass.Members[index] = name;
        }

        public InternshipClass GetClass()
        {
            return _internshipClass;
        }
    }
}