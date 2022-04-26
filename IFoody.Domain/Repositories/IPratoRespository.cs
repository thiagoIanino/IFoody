using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Repositories
{
    public interface IPratoRespository
    {
        Task GravarPrato(Prato prato);
        Task<IEnumerable<Prato>> ListarPratosRestaurante(Guid idRestaurante);
        Task<IEnumerable<Prato>> ListarPratosPedido(List<Guid> IdsPratos);
        Task DeletarPrato(Guid idPrato);
    }
}
