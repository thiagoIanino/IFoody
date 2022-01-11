using IFoody.Application.Models;
using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFoody.Application.Mapping
{
    public static class PratoMapper
    {

        public static List<PratoDto> ToPratoListDto(this IEnumerable<Prato> pratos, List<PratoPedidoInput> pratosInput)
        {
            var pratosListDto = new List<PratoDto>();
           foreach(var prato in pratos)
            {
                var quantidade = pratosInput.FirstOrDefault(pi => pi.Id == prato.Id).Quantidade;
                var pratoDto = prato.ToPratoDto(quantidade);
                pratosListDto.Add(pratoDto);
            }
           return pratosListDto;
        }

        public static PratoDto ToPratoDto (this Prato prato, int quantidade)
        {
            return new PratoDto
            {
                Id = prato.Id,
                IdRestaurante = prato.IdRestaurante,
                Nome = prato.NomePrato,
                Quantidade = quantidade,
                Valor = prato.Valor,
                ValorTotal = quantidade * prato.Valor
            };
        }
    }
}
