using RazorMvc.Models;
using System.Collections.Generic;

namespace RazorMvc.Services
{
    public interface IInternshipService
    {
        Intern AddMember(Intern member);
        InternshipClass GetClass();
        IList<Intern> GetMembers();
        void RemoveMember(int id);
        void UpdateMember(Intern intern);
    }
}