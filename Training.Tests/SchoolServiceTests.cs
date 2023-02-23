using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Data.Entity;
using System.Threading.Tasks;
using Training.Data;
using Training.Models;

namespace Training.Tests
{
    [TestClass]
    public class SchoolServiceTests
    {
        [TestMethod]
        public async Task Given_School_When_CreateAsync_Then_CreateSchool()
        {
            var dbSet = Substitute.For<DbSet<School>>();
            var ctx = Substitute.For<ISchoolDbContext>();
            ctx.Set<School>().Returns(dbSet);
            ctx.Set<School>().Add(Arg.Any<School>())
                .Returns(new School ());

            var service = new Repository<School>(ctx);
            await service.CreateAsync(new School());

            dbSet.Received(1).Add(Arg.Any<School>());
            await ctx.Received(1).SaveChangesAsync();
        }
    }
}