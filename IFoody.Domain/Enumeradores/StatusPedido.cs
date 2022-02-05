using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Enumeradores
{
    public enum StatusPedido
    {
        EmProcessamento = 1,
        Aberto = 2,
        Confirmado = 3,
        SaiuParaEntrega = 4,
        Finalizado = 5,
        NaoAprovado = 6
    }
}
