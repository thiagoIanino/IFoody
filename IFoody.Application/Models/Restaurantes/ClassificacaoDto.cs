using IFoody.Domain.Enumeradores.Avaliacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models.Restaurantes
{
    public class ClassificacaoDto
    {
        public StatusAvaliacaoRestaurante Status { get; set; }
        public double Nota { get; set; }
    }
}
