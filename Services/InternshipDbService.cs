﻿using RazorMvc.Data;
using RazorMvc.Hubs;
using RazorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorMvc.Services
{
    public class InternshipDbService : IInternshipService
    {
        private readonly InternDbContext db;

        public InternshipDbService(InternDbContext db)
        {
            this.db = db;
        }

        public Intern AddMember(Intern member)
        {
            db.Interns.AddRange(member);
            db.SaveChanges();
            return member;
        }

        public Intern GetMemberById(int id)
        {
            return db.Find<Intern>(id);
        }

        public IList<Intern> GetMembers()
        {
            var interns = db.Interns.ToList();
            return interns;
        }

        public void RemoveMember(int id)
        {
            var intern = GetMemberById(id);
            if(intern == null) return;
            db.Remove<Intern>(intern);
            db.SaveChanges();
        }

        public void UpdateMember(Intern intern)
        {
            var itemToBeUpdated = GetMemberById(intern.Id);
            itemToBeUpdated.Name = intern.Name;
            if (intern.DateOfJoin > DateTime.MinValue)
            {
                itemToBeUpdated.DateOfJoin = DateTime.Now;
            }

            db.Interns.Update(itemToBeUpdated);
            db.SaveChanges();
        }
    }
}
