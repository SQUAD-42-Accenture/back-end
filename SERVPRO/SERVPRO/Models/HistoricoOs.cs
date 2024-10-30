using SERVPRO.Enums;
namespace SERVPRO.Models
{
    public class HistoricoOS
    {
        public int Id { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string Comentario { get; set; }
        
        public int? OrdemDeServicoId { get; set; }    
        public virtual OrdemDeServico? OrdemDeServico { get; set; }

        public string? TecnicoCPF { get; set; }
        public virtual Tecnico? Tecnico { get; set; }
    }

}
