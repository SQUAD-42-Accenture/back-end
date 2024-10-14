using System.ComponentModel;

namespace SERVPRO.Enums
{
    public enum StatusOS
    {
        [Description("Aberto")]
        Aberto = 0,
        [Description("Em andamento")]
        EmAndamento = 1,
        [Description("Concluido")]
        Concluido = 2,
        [Description("Pendente")]
        Pendente = 3

    }
}
