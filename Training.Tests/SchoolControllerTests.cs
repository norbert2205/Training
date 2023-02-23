using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Serilog;
using System.Web.Http.Results;
using Training.Controllers;
using Training.Models;
using Training.Services;

namespace Training.Tests
{
    [TestClass]
    public class SchoolControllerTests
    {
        [TestMethod]
        public void Given_NameAndDescription_When_CreateSchool_Then_Success()
        {
            var controller = new SchoolController(Substitute.For<ISchoolService>(), Substitute.For<ILogger>());
            var actionResult = controller.Create("name", "desc", new byte[1]);
            var createdResult = actionResult.Result as CreatedAtRouteNegotiatedContentResult<School>;

            createdResult.Should().NotBeNull();
            createdResult?.RouteName.Should().Be("DefaultApi");
            // createdResult?.RouteValues["id"].Should().Be(0);
        }
    }
}
