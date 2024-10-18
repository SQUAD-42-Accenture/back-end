namespace SERVPRO.Models
{
    public class Tecnico: Usuario
    {
       
        public string Especialidade { get; set; }

       public List<OrdemDeServico> OrdensDeServico { get; set; }

    }

}
