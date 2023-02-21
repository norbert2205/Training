using System.Linq;
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

        public IQueryable<User> GetUsers()
        {
            return _userRepository.Table;
        }

        public User GetUser(int id)
        {
            return _userRepository.GetById(id);
        }

        public void InsertUser(User user)
        {
            _userRepository.Insert(user);
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