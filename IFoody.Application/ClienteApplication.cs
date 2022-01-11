using IFoody.Application.Interfaces;
using IFoody.Application.Mapping;
using IFoody.Application.Models;
using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores.Cliente;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Application
{
    public class ClienteApplication : IClienteApplication
    {
        private readonly IDominioClienteService _dominioClienteService;
        private readonly IClienteRepository _clienteService;
        private readonly ICartaoCreditoRepository _cartaoCreditoService;
        public ClienteApplication(
            IDominioClienteService dominioClienteService,
            IClienteRepository clienteService,
            ICartaoCreditoRepository cartaoCreditoService)
        {
            _dominioClienteService = dominioClienteService;
            _clienteService = clienteService;
            _cartaoCreditoService = cartaoCreditoService;
        }

        public async Task CadastrarCliente(ClienteInput clienteInput)
        {
            var cliente = new Cliente(
                clienteInput.Nome,
                clienteInput.Email,
                clienteInput.Senha
                );

            _dominioClienteService.ValidarDadosCadastroCliente(cliente);
            await _clienteService.GravarCliente(cliente);
        }

        public async Task<ClienteDto> AutenticarCliente(string email, string senha)
        {
            _dominioClienteService.ValidarDadosAutenticacao(email, senha);
            var cliente = await _clienteService.AutenticarCliente(email, senha);
            _dominioClienteService.VerificarSeClienteEstaAutenticado(cliente);

            var cartoesCliente = await _cartaoCreditoService.ListarCartoesCliente(cliente.Id);

            var clienteDto = new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cartoes = cartoesCliente.ToCartaoCreditoListModel()
            };

            return clienteDto;
        }

        public async Task CadastrarCartaoCliente(CartaoCreditoInput cartaoInput)
        {
            var cartao = new CartaoCredito(
                cartaoInput.IdCliente,
                cartaoInput.Numero,
                cartaoInput.Validade,
                cartaoInput.NomeTitular,
                cartaoInput.Cpf
                );

            _dominioClienteService.ValidarDadosCartaoCliente(cartao);

            await _cartaoCreditoService.CadastrarCartao(cartao);
        }
    }
}
