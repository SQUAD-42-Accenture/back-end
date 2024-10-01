using System.ComponentModel.DataAnnotations;

namespace SERVPRO.API.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Cpf { get; set; } 
        public string? Email { get; set; }
        public required string ContactNumber { get; set; }
        public required string Role { get; set; }  // "Admin", "Técnico", "Cliente"
        public required string Password { get; set; }
    }
}
