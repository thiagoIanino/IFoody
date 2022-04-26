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
        private readonly ITokenRepository _tokenRepository;
        public DominioClienteService(IClienteRepository clienteService,
            IPagamentoRepository pagamentoService,
            ITokenRepository tokenRepository)
        {
            _clienteService = clienteService;
            _pagamentoService = pagamentoService;
            _tokenRepository = tokenRepository;
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

        public string AutenticarCliente(Cliente cliente) 
        {
            if(cliente is null)
            {
                throw new Exception("Email ou senha incorretos");
            }
            return _tokenRepository.GenerateToken(cliente.Role,cliente.Email,cliente.Id);

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

        public EnderecoCliente FormatarEnderecoCliente(Guid idCliente, string rua, double? numero, double? apto, string bairro, string cidade, string estado)
        {
            if (idCliente == Guid.Empty)
            {
                throw new Exception("O Id do cliente não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(rua))
            {
                throw new Exception("O campo rua não pode ser vazio");
            }
            if (numero is null)
            {
                throw new Exception("O campo numero não pode ser vazio");
            }

            if (string.IsNullOrWhiteSpace(bairro))
            {
                throw new Exception("O campo bairro não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(cidade))
            {
                throw new Exception("O campo cidade não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(estado))
            {
                throw new Exception("O campo estado não pode ser vazio");
            }

            string aptoForamatado = "";
            if (apto != null)
                aptoForamatado = string.Concat(" apto " + apto);
            var linha1Endereco = string.Concat(rua + " " + numero + aptoForamatado);
            var linha2Endereco = string.Concat(" Bairro " + bairro + "," + cidade + "," + estado);

            return new EnderecoCliente
            (
                 Guid.NewGuid(),
                idCliente,
                linha1Endereco,
                linha2Endereco
            );
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
