using IFoody.Domain.Entities.Restaurantes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Repositories.Restaurantes
{
    public interface IStatusAvaliacaoRepository
    {
        Task GravarStatusAvaliacaoRestaurante(StatusAvaliacao statusAvaliacao);
        Task AtualizarStatusAvaliacaoRestaurante(StatusAvaliacao statusAvaliacao);

    }
}
