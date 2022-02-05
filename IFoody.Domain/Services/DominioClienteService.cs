using IFoody.Domain.Core.Services;
using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores.Cliente;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Services
{
   public class DominioClienteService : DomainService, IDominioClienteService
    {
        private readonly IClienteRepository _clienteService;
        private readonly IPagamentoRepository _pagamentoService;
        public DominioClienteService(IClienteRepository clienteService, IPagamentoRepository pagamentoService)
        {
            _clienteService = clienteService;
            _pagamentoService = pagamentoService;
        }

        public void ValidarDadosCadastroCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                throw new Exception("O campo Nome não pode ser vazio");
            } 
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                throw new Exception("O campo Email não pode ser vazio");
            } if (string.IsNullOrWhiteSpace(cliente.Senha))
            {
                throw new Exception("O campo Senha não pode ser vazio");
            } 
        }

        public void VerificarSeClienteEstaAutenticado(Cliente cliente) 
        {
            if(cliente is null)
            {
                throw new Exception("Email ou senha incorretos");
            }
        }

        public void ValidarDadosCartaoCliente(CartaoCredito cartao)
        {
            if (cartao.IdCliente == Guid.Empty)
            {
                throw new Exception("O campo Nome não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(cartao.Cpf))
            {
                throw new Exception("O campo Email não pode ser vazio");
            }
            if (cartao.Validade == DateTime.MinValue)
            {
                throw new Exception("O campo Senha não pode ser vazio");
            }  
            if (string.IsNullOrWhiteSpace(cartao.NomeTitular))
            {
                throw new Exception("O campo Senha não pode ser vazio");
            }  if (string.IsNullOrWhiteSpace(cartao.Numero))
            {
                throw new Exception("O campo Senha não pode ser vazio");
            }
        }

        public Task<Cliente> BuscarCliente(Guid id)
        {
            var cliente = _clienteService.BuscarCliente(id);

            if(cliente is null)
            {
                throw new Exception("Cliente não existe");
            }
            return cliente;
        }

        public async Task<string> CadastrarCartaoStripe(CartaoCredito cartao, Cliente cliente)
        {
            var cartaoStripe = new CartaoStripeDto(cliente.IdStripe,cartao.Numero,cartao.Validade,cartao.NomeTitular,cartao.Cpf);
            return await _pagamentoService.CadastrarCartaoStripe(cartaoStripe);
        }
    }
}
