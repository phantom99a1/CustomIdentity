﻿namespace WebUI.Entities
{
    public class Login
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
