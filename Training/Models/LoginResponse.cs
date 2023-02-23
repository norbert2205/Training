using System;
using Training.Data;

namespace Training.Models
{
    public class LoginResponse : BaseEntity
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public DateTime AccessTokenExpiry { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }
}