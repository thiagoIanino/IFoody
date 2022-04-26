using IFoody.Application.Interfaces;
using IFoody.Application.Models;
using IFoody.Application.Models.Restaurantes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFoody.Api.Controllers
{
    [Route("api/restaurantes")]
    public class RestauranteController : ControllerBase
    {
        private readonly IRestauranteApplication _restauranteService;
        public RestauranteController(IRestauranteApplication restauranteService)
        {
            _restauranteService = restauranteService;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarRestaurante([FromBody] RestauranteInput restauranteInput)
        {
            await _restauranteService.CadastrarRestaurante(restauranteInput);
            return Ok("Restaurante cadastrado com sucesso");
        }

        [HttpGet]
        [Route("tipo/{tipo}")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarRestaurantesPorTipo(string tipo)
        {
            var restaurantes = await _restauranteService.ListarRestaurantesPorTipo(tipo);
            return Ok(restaurantes);
        }
        [HttpGet]
        [Route("classificacao")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarRestaurantesPorClassificacao()
        {
            var restaurantes = await _restauranteService.ListarRestaurantesPorClassificacao();
            return Ok(restaurantes);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> AutenticarRestaurante([FromBody] AutenticacaoInput autenticacaoInput)
        {
            var restaurante = await _restauranteService.AutenticarRestaurante(autenticacaoInput.Email, autenticacaoInput.Senha);
            return Ok(restaurante);
        }

        [HttpPost]
        [Route("avaliacao")]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> AvaliarRestaurante([FromBody]AvaliacaoInput avaliacaoInput)
        {
            await _restauranteService.AvaliarRestaurante(avaliacaoInput);
            return Ok();
        }

        [HttpGet]
        [Route("{idRestaurante}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterRestaurnte(Guid idRestaurante)
        {
            var restaurante = await _restauranteService.ObterRestaurante(idRestaurante);
            return Ok(restaurante);
        }
    }
}
