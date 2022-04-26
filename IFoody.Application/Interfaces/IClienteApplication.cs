using IFoody.Application.Models;
using IFoody.Domain.Entities;
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
        Task<CartaoCreditoDto> CadastrarCartaoCliente(CartaoCreditoInput cartaoInput);
        Task<EnderecoCliente> CadastrarEndereco(EnderecoInput endereco);
        Task<IEnumerable<EnderecoCliente>> ListarEnderecosCliente();
        Task<IEnumerable<CartaoCreditoDto>> ListarCartoesCliente();
    }
}
