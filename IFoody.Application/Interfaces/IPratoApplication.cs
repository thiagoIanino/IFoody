using IFoody.Application.Models;
using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Application.Interfaces
{
    public interface IPratoApplication
    {
        Task CadastrarPrato(PratoInput pratoInput);
        Task<IEnumerable<Prato>> ListarPratosPorRestaurante(Guid idRestaurante);
    }
}
