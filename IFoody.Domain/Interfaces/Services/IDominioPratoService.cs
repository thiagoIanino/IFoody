using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Interfaces.Services
{
    public interface IDominioPratoService
    {
        void ValidarDadosCadastroPrato(Prato prato);
        void ValidarIdRestaurante(Guid idRestaurante);
    }
}
