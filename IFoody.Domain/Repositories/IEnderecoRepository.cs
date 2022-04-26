using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Repositories
{
    public interface IEnderecoRepository
    {
        Task CadastrarEndereco(EnderecoCliente endereco);
        Task<IEnumerable<EnderecoCliente>> ObterEnderecos(Guid idCliente);
        Task<EnderecoCliente> ObterEndereco(Guid idEndereco);
    }
}
