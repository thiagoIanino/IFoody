using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores.Cliente;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Interfaces.Services
{
    public interface IDominioClienteService
    {
       void ValidarDadosCadastroCliente(Cliente cliente);
       void ValidarDadosAutenticacao(string email, string senha);
        string AutenticarCliente(Cliente cliente);
        void ValidarDadosCartaoCliente(CartaoCredito cartao);
        Task<Cliente> BuscarCliente(Guid id);
        Task<string> CadastrarCartaoStripe(CartaoCredito cartao, Cliente cliente);
        EnderecoCliente FormatarEnderecoCliente(Guid idCliente, string rua, double? numero, double? apto, string bairro, string cidade, string estado);
    }
}
