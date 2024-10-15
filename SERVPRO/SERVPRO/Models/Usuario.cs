using SERVPRO.Enums;

namespace SERVPRO.Models
{
    public class Usuario
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string? Email { get; set; }
        public string Senha { get; set; }
        private string _tipoUsuario;
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
