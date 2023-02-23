﻿using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Models;

namespace Training.Controllers
{
    public interface IUserController
    {
        Task<IHttpActionResult> Create(string firstName, string lastName, string phoneNumber, Type type, CancellationToken token);

        Task<IHttpActionResult> Delete(int id, CancellationToken token);

        Task<IHttpActionResult> Update([FromBody] User newUser);

        Task<IHttpActionResult> Get(int id, CancellationToken token);

        Task<IHttpActionResult> GetAll(CancellationToken token);
    }
}