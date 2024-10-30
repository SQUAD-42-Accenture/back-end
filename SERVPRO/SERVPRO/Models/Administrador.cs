using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class Administrador : Usuario
    {
        [JsonPropertyOrder(6)]
        public string Departamento { get; set; }
        [JsonPropertyOrder(7)]
        public DateTime DataContratacao { get; set; }
        [JsonPropertyOrder(8)]
        public string? Telefone { get; set; } 
    }
}
