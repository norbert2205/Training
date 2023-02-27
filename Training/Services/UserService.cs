using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
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

        public async Task<User> GetSchoolAsync(int id)
        {
            return await _userRepository.FindAsync(_ => _.Id == id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.CreateAsync(user);
        }

        public async Task<bool> IsValidLoginAsync(LoginRequest loginRequest)
        {
            return await _userRepository.FindAsync(_ =>
                _.Username == loginRequest.Username && _.Password == loginRequest.Password) != null;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<int> DeleteUserAsync(User user)
        {
            return await _userRepository.DeleteAsync(user);
        }

        public async Task<User> FindUserAsync(Expression<Func<User, bool>> match)
        {
            return await _userRepository.FindAsync(match);
        }
    }
}