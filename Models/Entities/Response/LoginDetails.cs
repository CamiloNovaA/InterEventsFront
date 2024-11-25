using System.IdentityModel.Tokens.Jwt;

namespace Models.Entities.Response
{
    public class LoginDetails
    {
        public bool IsSuccess { get; set; }
        public int IdUser { get; set; }
        public string? FullName { get; set; }
        public string? Token { get; set; }
    }
}