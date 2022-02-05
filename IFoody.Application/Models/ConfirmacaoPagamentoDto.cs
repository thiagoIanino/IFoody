using IFoody.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class ConfirmacaoPagamentoDto
    {
        public PedidoGeralDto PedidoGeral { get; set; }
        public RespostaPagamentoDto RespostaPagamento { get; set; }
    }
}
