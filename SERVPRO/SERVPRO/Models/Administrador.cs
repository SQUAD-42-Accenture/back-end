namespace SERVPRO.Models
{
    public class Administrador : Usuario
    {
        public string Departamento { get; set; } 
        public DateTime DataContratacao { get; set; } 
        public string? Telefone { get; set; } 
    }
}
