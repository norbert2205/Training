using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Training.Data;
using Training.Models;

namespace Training.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAll
                .ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _userRepository.GetById(id)
                .FirstOrDefaultAsync();
        }

        public void CreateUser(User user)
        {
            _userRepository.Create(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
        }
    }
}