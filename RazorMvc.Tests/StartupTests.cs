using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RazorMvc.Tests
{
    public class StartupTests
    {
        [Fact]
        public void ShouldConvertUrlToHerokuString()
        {
            // Assume
            var url = "postgres://bqjpqcfqbbhepw:54f6615f773ba7613508f0b8fd1ad9a8292e5a2097f2eae2e5e753e239407f1c@ec2-99-80-200-225.eu-west-1.compute.amazonaws.com:5432/d8d5aqlqcjhihh";

            // Act
            var herokuConnectionString = Startup.ConvertDatabaseUrlToHerokuString(url);

            // Assert
            Assert.Equal("Server=ec2-99-80-200-225.eu-west-1.compute.amazonaws.com;Port=5432;Database=d8d5aqlqcjhihh;User Id=bqjpqcfqbbhepw;Password=54f6615f773ba7613508f0b8fd1ad9a8292e5a2097f2eae2e5e753e239407f1c;Pooling=true;SSL Mode=Require;Trust Server Certificate=True;", herokuConnectionString);
        }
    }
}
