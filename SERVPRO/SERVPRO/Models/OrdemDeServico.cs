using SERVPRO.Enums;

namespace SERVPRO.Models
{
    public class OrdemDeServico
    {
        public int OrdemDeServicoId { get; set; }
        public DateTime dataAbertura { get; set; }
        public DateTime? dataConclusao { get; set; }
        public string DescricaoProblema { get; set; }
        public Cliente Cliente { get; set; }
        public Tecnico Tecnico { get; set; }
        public StatusOS Status { get; set; }
      
    }

}
