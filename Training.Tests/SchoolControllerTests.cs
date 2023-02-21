using System.Web.Http.Results;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Training.Controllers;
using Training.Models;
using Training.Services;

namespace Training.Tests
{
    [TestClass]
    public class SchoolControllerTests
    {
        [TestMethod]
        public void Given_ProperNameAndDescription_When_CreateSchool_Then_Success()
        {
            var schoolService = Substitute.For<ISchoolService>();
            var controller = new SchoolController(schoolService);

            var actionResult = controller.Create("name", "desc", new byte[1]);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<School>;

            createdResult.Should().NotBeNull();
            createdResult?.RouteName.Should().Be("DefaultApi");
            createdResult?.RouteValues["id"].Should().Be(0);
        }
    }
}
