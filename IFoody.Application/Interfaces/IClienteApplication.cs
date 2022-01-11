using IFoody.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Application.Interfaces
{
    public interface IClienteApplication
    {
        Task CadastrarCliente(ClienteInput clienteInput);
        Task<ClienteDto> AutenticarCliente(string email, string senha);
        Task CadastrarCartaoCliente(CartaoCreditoInput cartaoInput);
    }
}
