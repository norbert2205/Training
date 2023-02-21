using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Training.Data;
using Training.Models;

namespace Training.Tests
{
    [TestClass]
    public class SchoolServiceTests
    {
        [TestMethod]
        public void Given_School_When_Insert_Then_CreateSchool()
        {
            var dbSet = Substitute.For<DbSet<School>>();
            var ctx = Substitute.For<ISchoolDbContext>();
            ctx.Set<School>().Returns(dbSet);
            ctx.Set<School>().Add(Arg.Any<School>())
                .Returns(new School ());

            var service = new Repository<School>(ctx);
            service.Create(new School());

            dbSet.Received(1).Add(Arg.Any<School>());
            ctx.Received(1).SaveChangesAsync();
        }
    }
}