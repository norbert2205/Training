using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Training.Data;
using Training.Models;

namespace Training.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<LoginResponse> _loginRepository;
        private readonly IRepository<RefreshToken> _tokenRepository;

        public AccountService(IRepository<LoginResponse> loginRepository, IRepository<RefreshToken> tokenRepository)
        {
            _loginRepository = loginRepository;
            _tokenRepository = tokenRepository;
        }

        // record the user login+accesstoken+refresh token 
        public async Task RecordUserLogin(LoginResponse user)
        {
            var existingUser = await GetUser(_ => _.UserId == user.UserId);
            var refreshToken = await GetRefreshTokenDetail(_ => _.User == user.UserId);

            if (existingUser == null)
            {
                await _loginRepository.CreateAsync(user);
            }
            else
            {
                await _loginRepository.DeleteAsync(existingUser);
                await _loginRepository.CreateAsync(user);
            }

            if (refreshToken == null)
            {
                await _tokenRepository.CreateAsync(user.RefreshToken);

            }
        }

        public async Task<LoginResponse> GetUser(Expression<Func<LoginResponse, bool>> match)
        {
            return await _loginRepository.FindAsync(match);
        }

        // Return a refresh token detail for a user
        public async Task<RefreshToken> GetRefreshTokenDetail(Expression<Func<RefreshToken, bool>> match)
        {
            return await _tokenRepository.FindAsync(match);
        }

        // Remove refresh token from the database
        public async Task RemoveRefreshToken(string username)
        {

            var entity = await GetRefreshTokenDetail(_ => _.User == username);

            if (entity != null)
            {
                await _tokenRepository.DeleteAsync(entity);
            }
        }
    }
}