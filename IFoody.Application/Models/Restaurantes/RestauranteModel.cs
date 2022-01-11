using IFoody.Domain.Entities.Restaurantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models.Restaurantes
{
    public class RestauranteModel
    {

        public RestauranteModel(Restaurante restaurante)
        {
            Id = restaurante.Id;
            NomeRestaurante = restaurante.NomeRestaurante;
            Tipo = restaurante.Tipo;
            Classificacao = new ClassificacaoDto
            {
                Status = restaurante.Status,
                Nota = restaurante.Nota
            };
        }

        public Guid Id { get; set; }
        public string NomeRestaurante { get; set; }
        public string Tipo { get; set; }
        public ClassificacaoDto Classificacao { get; set; }


    }
}
