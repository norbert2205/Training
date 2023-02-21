using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserAsync(int id);

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);
    }
}