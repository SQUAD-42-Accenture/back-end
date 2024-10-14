namespace SERVPRO.Models
{
    public class Tecnico
    {
        public string TecnicoCPF { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public List<OrdemDeServico> OrdensDeServico { get; set; }
    }

}
