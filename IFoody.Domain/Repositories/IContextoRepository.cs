using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Repositories
{
    public interface IContextoRepository
    {
        string ObterTokenAutenticacaoHeader();
        Guid ObterIdUsuarioAutenticado();
    }
}
