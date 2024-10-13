
using SERVPRO.API.User.Models;

namespace SERVPRO.API.Tecnico.Models
{
    public class TecnicoModels : UserModel
    {
        public string Telefone { get; set; }
        public string Especialidade { get; set; }
        public string Rua { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
        public string PontoDeReferencia { get; set; }
    }
}
