﻿namespace Models.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string FullName => Name + LastName;
        public string? Email { get; set; }
        public string? Password { get => _password; set => _password = BCrypt.Net.BCrypt.HashPassword(value) ; }

        private string? _password;
    }
}
