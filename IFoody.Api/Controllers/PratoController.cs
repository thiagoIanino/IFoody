using IFoody.Application.Interfaces;
using IFoody.Application.Models;
using IFoody.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "restaurante")]
        public async Task<IActionResult> CadastrarPrato([FromBody]PratoInput pratoInput)
        {
            await _pratoservice.CadastrarPrato(pratoInput);
            return Ok("Prato cadastrado com sucesso");
        } 
        [Route("{idPrato}")]
        [HttpDelete]
        [Authorize(Roles = "restaurante")]
        public async Task<IActionResult> CadastrarPrato(Guid idPrato)
        {
            await _pratoservice.DeletarPrato(idPrato);
            return Ok("Prato cadastrado com sucesso");
        }

        [HttpGet]
        [Route("{idRestaurante}")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarPratosPorRestaurante(Guid idRestaurante)
        {
            var pratos = await _pratoservice.ListarPratosPorRestaurante(idRestaurante);
            return Ok(pratos);
            
        }
    }
}
