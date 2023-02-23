using System;
using Training.Data;

namespace Training.Models
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }

        public string TokenType { get; set; }

        public DateTime Expiration { get; set; }

        public string User { get; set; }
    }
}