namespace SERVPRO.API.Models
{
    public class UserLoginModel
    {
        public required string Cpf { get; set; }
        public required string Password{ get; set; }
    }
}
