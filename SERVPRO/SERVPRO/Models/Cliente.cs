namespace SERVPRO.Models
{
    public class Cliente
    {
        public string ClienteCPF { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string? Email { get; set; }
        public string Senha { get; set; }


        public List<Equipamento> Equipamentos { get; set; }
        // public List<OrdemDeServico> OrdensDeServico { get; set; }
    }

}
