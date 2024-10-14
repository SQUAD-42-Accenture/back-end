using SERVPRO.Enums;
namespace SERVPRO.Models
{
    public class HistoricoOS
    {
        public int HistoricoOSId { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string Comentario { get; set; }
        public StatusOS Status { get; set; }
        public OrdemDeServico OrdemDeServico { get; set; }
        public Tecnico Tecnico { get; set; }
    }

}
