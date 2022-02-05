using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class RespostaPagamentoDto
    {
        public bool Aprovado { get; set; }
        public Guid IdPedido { get; set; }

    }
}
