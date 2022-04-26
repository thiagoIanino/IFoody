using IFoody.Application.Interfaces;
using IFoody.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarCliente([FromBody] ClienteInput clienteInput)
        {
            await _clienteService.CadastrarCliente(clienteInput);
            return Ok("Cadastrado com sucesso");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> AutenticarCliente([FromBody] AutenticacaoInput autenticacaoInput)
        {
            var cliente = await _clienteService.AutenticarCliente(autenticacaoInput.Email, autenticacaoInput.Senha);
            return Ok(cliente);
        }

        [HttpPost]
        [Route("cartao")]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> CadastrarCartao([FromBody] CartaoCreditoInput cartaoInput)
        {
            var cartao = await _clienteService.CadastrarCartaoCliente(cartaoInput);
            return Ok(cartao);
        }     
        
        [HttpGet]
        [Route("cartao")]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> ListarCartoes()
        {
            var cartoes = await _clienteService.ListarCartoesCliente();
            return Ok(cartoes);
        }    
        [HttpPost]
        [Route("endereco")]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> CadastrarEndereco([FromBody] EnderecoInput endereco)
        {
            
            var enderecoFormatado = await _clienteService.CadastrarEndereco(endereco);
            
            return Ok(enderecoFormatado);
        }

        [HttpGet]
        [Route("endereco")]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> ListarEndereco()
        {
            var enderecos = await _clienteService.ListarEnderecosCliente();

            return Ok(enderecos);
        }

    }
}