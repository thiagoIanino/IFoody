using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class CobrancaDto
    {
        public PedidoGeralDto PedidoGeral { get; set; }
        public string TokenAutenticacao { get; set; }
    }
}
