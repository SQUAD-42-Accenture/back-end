using SERVPRO.API.User.Models;

namespace SERVPRO.API.Cliente.Models
{
    public class ClienteModels : UserModel
    {
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Rua { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
        public string PontoDeReferencia { get; set; }
    }
}
