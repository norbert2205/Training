using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface IAccountService
    {
        Task RecordUserLogin(LoginResponse user);

        Task RemoveRefreshToken(string username);

        Task<LoginResponse> GetUser(Expression<Func<LoginResponse, bool>> match);

        Task<RefreshToken> GetRefreshTokenDetail(Expression<Func<RefreshToken, bool>> match);
    }
}