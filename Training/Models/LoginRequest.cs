﻿namespace Training.Models
{
    public class LoginRequest
    {
        public string User { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}