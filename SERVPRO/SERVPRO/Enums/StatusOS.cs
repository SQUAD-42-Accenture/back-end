using System.ComponentModel;

namespace SERVPRO.Enums
{
    public enum StatusOS
    {
        [Description("Aberta")]
        Aberta = 0,
        [Description("Em Andamento")]
        EmAndamento = 1,
        [Description("Concluida")]
        Concluida = 2,
        [Description("Cancelada")]
        Cancelada = 3

    }
}
