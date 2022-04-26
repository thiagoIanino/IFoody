using IFoody.Application.Interfaces;
using IFoody.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IFoody.Api.Controllers
{
    [Route("api/pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoApplication _pedidoService;
        public PedidoController(IPedidoApplication pedidoServic)
        {
            _pedidoService = pedidoServic;
        }


        [HttpPost]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> CadastrarPedido([FromBody] PedidoInput pedido)
        {
            await _pedidoService.CadastrarPedido(pedido);
            return Ok();
        }

        [Route("pagamento/confirmar")]
        [HttpPost]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> ConfirmarPagamento([FromBody]ConfirmacaoPagamentoDto confirmacaoPagamento)
        {
            await _pedidoService.ConfirmarPagamento(confirmacaoPagamento);
            return Ok();
        }

        [HttpPatch]
        [Authorize(Roles = "restaurante")]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizacaoPedidoDto confirmacaoPagamento)
        {
            await _pedidoService.AtualizarPedido(confirmacaoPagamento);
            return Ok();
        }

    }
}
