using IFoody.Application.Interfaces;
using IFoody.Application.Models;
using IFoody.Application.Models.Restaurantes;
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
        public async Task<IActionResult> CadastrarRestaurante([FromBody] RestauranteInput restauranteInput)
        {
            await _restauranteService.CadastrarRestaurante(restauranteInput);
            return Ok("Restaurante cadastrado com sucesso");
        }

        [HttpGet]
        [Route("tipo")]
        public async Task<IActionResult> ListarRestaurantesPorTipo(string tipo)
        {
            var restaurantes = await _restauranteService.ListarRestaurantesPorTipo(tipo);
            return Ok(restaurantes);
        }
        [HttpGet]
        [Route("classificacao")]
        public async Task<IActionResult> ListarRestaurantesPorClassificacao()
        {
            var restaurantes = await _restauranteService.ListarRestaurantesPorClassificacao();
            return Ok(restaurantes);
        }

        [HttpPost]
        [Route("Autenticacao")]
        public async Task<IActionResult> AutenticarCliente(string email, string senha)
        {
            await _restauranteService.AutenticarRestaurante(email, senha);
            return Ok("Cliente autenticado com sucesso");
        }

        [HttpPost]
        [Route("Avaliacao")]
        public async Task<IActionResult> AvaliarRestaurante(AvaliacaoInput avaliacaoInput)
        {
            await _restauranteService.AvaliarRestaurante(avaliacaoInput);
            return Ok();
        }
    }
}
