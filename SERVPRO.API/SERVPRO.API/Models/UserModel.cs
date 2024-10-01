namespace SERVPRO.API.Models
{
    public class UserModel
    {
        public required string Name { get; set; }
        public required string Cpf { get; set; }
        public string Email { get; set; }
        public required string Role { get; set; }
        public required string Password { get; set; }
    }
}
