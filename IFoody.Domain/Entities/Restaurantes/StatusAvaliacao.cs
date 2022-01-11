using IFoody.Domain.Enumeradores.Avaliacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities.Restaurantes
{
    public class StatusAvaliacao
    {

        public StatusAvaliacao(Guid idRestaurante )
        {
            IdRestaurante = idRestaurante;
            Status = StatusAvaliacaoRestaurante.ClassificacaoNaoLiberada;
        }
        public Guid IdRestaurante { get; set; }
        public StatusAvaliacaoRestaurante Status { get; set; } 
    }
}
