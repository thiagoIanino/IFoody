using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> AutenticarCliente(string email, string senha);
        Task GravarCliente(Cliente cliente);
        Task<Cliente> BuscarCliente(Guid id);
    }
}
