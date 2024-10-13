namespace SERVPRO.API.User.Models
{
    public enum TipoDeUsuario
    {
        Admin,
        Tecnico,
        Cliente
    }
    public class UserModel
    {
        public int Id { get; set; }  // Chave primária
        public required string Name { get; set; }
        public required string Cpf { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
        public TipoDeUsuario UserType { get; set; }  // Enum para tipos de usuário
    }
}
