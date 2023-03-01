using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface IUserService
    {
        Task<User> UpdateUserAsync(User user, CancellationToken token);

        Task<int> DeleteUserAsync(User user, CancellationToken token);

        Task<User> FindUserAsync(Expression<Func<User, bool>> match, CancellationToken token);

        Task<User> CreateUserAsync(User user, CancellationToken token);

        Task<IEnumerable<User>> GetUsersAsync(CancellationToken token);
    }
}