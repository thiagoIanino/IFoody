using IFoody.Application.Interfaces;
using IFoody.Application.Models;
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
        public async Task<IActionResult> CadastrarPedido([FromBody] PedidoInput pedido)
        {
            await _pedidoService.CadastrarPedido(pedido);
            return Ok();
        }
    }
}
