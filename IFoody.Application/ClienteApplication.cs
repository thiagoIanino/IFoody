using IFoody.Application.Interfaces;
using IFoody.Application.Mapping;
using IFoody.Application.Models;
using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores;
using IFoody.Domain.Enumeradores.Cliente;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Application
{
    public class ClienteApplication : IClienteApplication
    {
        private readonly IDominioClienteService _dominioClienteService;
        private readonly IClienteRepository _clienteService;
        private readonly ICartaoCreditoRepository _cartaoCreditoService;
        private readonly IPagamentoRepository _pagamentoService;
        private readonly IEnderecoRepository _enderecoService;
        private readonly IContextoRepository _contextoService;
        public ClienteApplication(
            IDominioClienteService dominioClienteService,
            IClienteRepository clienteService,
            ICartaoCreditoRepository cartaoCreditoService,
            IPagamentoRepository pagamentoService,
            IEnderecoRepository enderecoService,
            IContextoRepository contextoService)
        {
            _dominioClienteService = dominioClienteService;
            _clienteService = clienteService;
            _cartaoCreditoService = cartaoCreditoService;
            _pagamentoService = pagamentoService;
            _enderecoService = enderecoService;
            _contextoService = contextoService;
        }

        public async Task CadastrarCliente(ClienteInput clienteInput)
        {
            var cliente = new Cliente(
                clienteInput.Nome,
                clienteInput.Email,
                clienteInput.Senha
                );

            _dominioClienteService.ValidarDadosCadastroCliente(cliente);

            var usuarioStripe = new UsuarioStripeDto(
              cliente.Nome,
              cliente.Email,
              CategoriaStripe.Cliente);

            var idStripe = await _pagamentoService.CadastrarUsuarioStripe(usuarioStripe);
            cliente.AdicionarIdStripe(idStripe);

            await _clienteService.GravarCliente(cliente);
        }

        public async Task<ClienteDto> AutenticarCliente(string email, string senha)
        {
            _dominioClienteService.ValidarDadosAutenticacao(email, senha);
            var cliente = await _clienteService.ObterClientePorEmailESenha(email, senha);
            var token = _dominioClienteService.AutenticarCliente(cliente);

            var cartoesCliente = await _cartaoCreditoService.ListarCartoesCliente(cliente.Id);

            var clienteDto = new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cartoes = cartoesCliente.ToCartaoCreditoListModel(),
                Token = token
            };

            return clienteDto;
        }

        public async Task<CartaoCreditoDto> CadastrarCartaoCliente(CartaoCreditoInput cartaoInput)
        {
            var idCliente = _contextoService.ObterIdUsuarioAutenticado();

            var cartao = new CartaoCredito(
                idCliente,
                cartaoInput.Numero,
                cartaoInput.Validade,
                cartaoInput.NomeTitular,
                cartaoInput.Cpf,
                cartaoInput.Bandeira,
                cartaoInput.Cvv
                );

            _dominioClienteService.ValidarDadosCartaoCliente(cartao);

            var cliente = await _dominioClienteService.BuscarCliente(idCliente);

            var idCartaoStripe = await _dominioClienteService.CadastrarCartaoStripe(cartao, cliente);

            cartao.AdicionarIdCartaoStripe(idCartaoStripe);

            await _cartaoCreditoService.CadastrarCartao(cartao);

            return cartao.ToCartaoCreditoModel();
        }

        public async Task<EnderecoCliente> CadastrarEndereco(EnderecoInput endereco)
        {
            var idCliente = _contextoService.ObterIdUsuarioAutenticado();

            var enderecoFormatado = _dominioClienteService.FormatarEnderecoCliente(
                idCliente,
                endereco.Rua,
                endereco.Numero,
                endereco.Apto,
                endereco.Bairro,
                endereco.Cidade,
                endereco.Estado);

            await _enderecoService.CadastrarEndereco(enderecoFormatado);

            return enderecoFormatado;

        }

        public async Task<IEnumerable<EnderecoCliente>> ListarEnderecosCliente()
        {
            var idCliente = _contextoService.ObterIdUsuarioAutenticado();

            return await _enderecoService.ObterEnderecos(idCliente);
        }   
        public async Task<IEnumerable<CartaoCreditoDto>> ListarCartoesCliente()
        {
            var idCliente = _contextoService.ObterIdUsuarioAutenticado();

            var cartoes = await _cartaoCreditoService.ListarCartoesCliente(idCliente);
            return cartoes.ToCartaoCreditoListModel();
        }
    }
}
