using RazorMvc.Models;
using RazorMvc.Services;
using System.Linq;
using Xunit;

namespace RazorMvc.Tests
{
    public class RazorMvcTests
    {
        [Fact]
        public void InitiallyContainsThreeMembers()
        {
            // Assume
            var intershipService = new InternshipService();

            // Act

            // Assert
            Assert.Equal(3, intershipService.GetMembers().Count);
        }

        [Fact]
        public void WhenAddMemberItShouldBeThere()
        {
            // Assume
            var intershipService = new InternshipService();
            Intern intern = new Intern();
            intern.Name = "Marko";

            // Act
            intershipService.AddMember(intern);

            // Assert
            Assert.Equal(4, intershipService.GetMembers().Count);
            Assert.Contains("Marko", intershipService.GetClass().Members.Select(member => member.Name));
        }
    }
}