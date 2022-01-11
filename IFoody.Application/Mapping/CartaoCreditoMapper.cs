using IFoody.Application.Models;
using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFoody.Application.Mapping
{
    public static class CartaoCreditoMapper
    {

        public static IEnumerable<CartaoCreditoDto> ToCartaoCreditoListModel(this IEnumerable<CartaoCredito> cartoes)
        {
            return cartoes.Select(c => c.ToCartaoCreditoModel());
        } 

        public static CartaoCreditoDto ToCartaoCreditoModel(this CartaoCredito cartao)
        {
            return new CartaoCreditoDto { 
                IdCartao = cartao.IdCartao,
                IdCliente = cartao.IdCliente,
                NumeroMascarado = cartao.NumeroMascarado 
            };
        }
    }
}
