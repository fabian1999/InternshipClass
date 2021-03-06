using RazorMvc.Hubs;
using RazorMvc.Models;
using System.Collections.Generic;

namespace RazorMvc.Services
{
    public interface IInternshipService
    {
        Intern AddMember(Intern member);

        IList<Intern> GetMembers();

        void RemoveMember(int id);

        void UpdateMember(Intern intern);

        Intern GetMemberById(int id);
        void UpdateLocation(int id, int locationId);
    }
}