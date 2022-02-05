using IFoody.Application.Interfaces;
using IFoody.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFoody.Api.Controllers
{
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteApplication _clienteService;
        public ClienteController(IClienteApplication clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCliente([FromBody] ClienteInput clienteInput)
        {
            await _clienteService.CadastrarCliente(clienteInput);
            return Ok("Cadastrado com sucesso");
        }

        [HttpPost]
        [Route("autenticacao")]
        public async Task<IActionResult> AutenticarCliente(string email, string senha)
        {
            var cliente = await _clienteService.AutenticarCliente(email,senha);
            return Ok(cliente);
        }

        [HttpPost]
        [Route("cartao")]
        public async Task<IActionResult> CadastrarCartao([FromBody]CartaoCreditoInput cartao)
        {
            await _clienteService.CadastrarCartaoCliente(cartao);
            return Ok("Cartao cadastrado com sucesso");
        }
    }
}