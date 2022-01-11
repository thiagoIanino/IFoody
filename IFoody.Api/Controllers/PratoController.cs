using IFoody.Application.Interfaces;
using IFoody.Application.Models;
using IFoody.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFoody.Api.Controllers
{
    [Route("api/pratos")]
    public class PratoController : ControllerBase
    {
        private readonly IPratoApplication _pratoservice;
        public PratoController(IPratoApplication pratoservice)
        {
            _pratoservice = pratoservice;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarPrato([FromBody]PratoInput pratoInput)
        {
            await _pratoservice.CadastrarPrato(pratoInput);
            return Ok("Prato cadastrado com sucesso");
        }

        [HttpGet]
        [Route("{idRestaurante}")]
        public async Task<IActionResult> ListarPratosPorRestaurante(Guid idRestaurante)
        {
            var pratos = await _pratoservice.ListarPratosPorRestaurante(idRestaurante);
            return Ok(pratos);
            
        }
    }
}
