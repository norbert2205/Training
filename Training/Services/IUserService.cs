using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface IUserService
    {
        Task<User> UpdateUserAsync(User user);

        Task<int> DeleteUserAsync(User user);

        Task<User> FindUserAsync(Expression<Func<User, bool>> match);

        Task<User> CreateUserAsync(User user);

        Task<bool> IsValidLoginAsync(LoginRequest loginRequest);

        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetSchoolAsync(int id);
    }
}