using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class Usuario
    {
        [JsonPropertyOrder(1)]
        public string CPF { get; set; }
        [JsonPropertyOrder(2)]
        public string Nome { get; set; }
        [JsonPropertyOrder(3)]
        public string? Email { get; set; }
        [JsonPropertyOrder(4)]
        public string Senha { get; set; }
        private string _tipoUsuario;
        [JsonPropertyOrder(5)]
        public string TipoUsuario
        {
            get => _tipoUsuario;
            set
            {
                if (value != "Cliente" && value != "Tecnico" && value != "Administrador")
                    throw new ArgumentException("TipoUsuario deve ser 'Cliente', 'Tecnico' ou 'Administrador'.");
                _tipoUsuario = value;
            }
        }
    }
}
