namespace SERVPRO.Models
{
    public class Cliente: Usuario
    {
        
        public string Endereco { get; set; }
        public string Telefone { get; set; }
   


        public List<Equipamento>? Equipamentos { get; set; }
        public List<OrdemDeServico>? OrdensDeServico { get; set; }
    }

}
