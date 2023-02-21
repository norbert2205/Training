using System.Web.Http;
using DryIoc;
using DryIoc.WebApi;
using Training.Controllers;
using Training.Data;
using Training.Models;
using Training.Services;

namespace Training
{
    public class Bootstrapper
    {
        public static void Bootstrap(HttpConfiguration config)
        {
            var container = new Container(_ => _
                    .WithoutThrowOnRegisteringDisposableTransient())
                    .WithWebApi(config);

            // container.Register<ISchoolDbContext>(
            //     Made.Of(() => new DbContext(nameof(School))),
            //     reuse: Reuse.InCurrentScope);

            container.Register<ISchoolDbContext, SchoolDbContext>();
            container.Register(typeof(IRepository<>), typeof(Repository<>));
            container.Register<ISchool, School>();
            container.Register<ISchoolService, SchoolService>();
            container.Register<ISchoolController, SchoolController>();
            container.Register<ICourse, Course>();
            container.Register<ICourseService, CourseService>();
            container.Register<ICourseController, CourseController>();
            container.Register<IUser, User>();
            container.Register<IUserService, UserService>();
            container.Register<IUserController, UserController>();
            container.Register<IAssignment,Assignment>();
            container.Register<IAssignmentService, AssignmentService>();
            container.Register<IAssignmentController, AssignmentController>();

            config.DependencyResolver = new DryIocDependencyResolver(container);

        }
    }
}