using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class Usuario
    {
        [JsonPropertyOrder(1)]
        public required string CPF { get; set; }
        [JsonPropertyOrder(2)]
        public required string Nome { get; set; }
        [JsonPropertyOrder(3)]
        public required string? Email { get; set; }
        [JsonPropertyOrder(4)]
        public required string Senha { get; set; }
        private required string _tipoUsuario;
        [JsonPropertyOrder(5)]
        public required string TipoUsuario
        {
            get => _tipoUsuario;
            set => _tipoUsuario = value;
            /*{
                if (value != "Cliente" && value != "Tecnico" && value != "Administrador")
                    throw new ArgumentException("TipoUsuario deve ser 'Cliente', 'Tecnico' ou 'Administrador'.");
                _tipoUsuario = value;
            }*/
        }
    }
}