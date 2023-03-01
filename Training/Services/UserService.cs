using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading;
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

        public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken token)
        {
            return await _userRepository.GetAll
                .ToListAsync(token);
        }

        public async Task<User> CreateUserAsync(User user, CancellationToken token)
        {
            return await _userRepository.CreateAsync(user, token);
        }

        public async Task<User> UpdateUserAsync(User user, CancellationToken token)
        {
            return await _userRepository.UpdateAsync(user, token);
        }

        public async Task<int> DeleteUserAsync(User user, CancellationToken token)
        {
            return await _userRepository.DeleteAsync(user, token);
        }

        public async Task<User> FindUserAsync(Expression<Func<User, bool>> match, CancellationToken token)
        {
            return await _userRepository.FindAsync(match, token);
        }
    }
}