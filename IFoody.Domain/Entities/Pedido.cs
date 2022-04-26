using IFoody.Domain.Dtos;
using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFoody.Domain.Entities
{
    public class Pedido
    {
        public Pedido()
        {

        }
        public Pedido(PratoDto prato, Guid idCliente,EnderecoCliente enderecoCliente)
        {
            IdPedido = Guid.NewGuid();
            IdRestaurante = prato.IdRestaurante;
            IdCliente = idCliente;
            Itens = new List<PratoDto> {
                prato
            };
            ValorTotal = prato.ValorTotal;
            Status = StatusPedido.EmProcessamento;
            EnderecoCliente = enderecoCliente;
        }

        public Guid IdPedido {get; set; }
        public Guid IdRestaurante { get; set; }
        public string NomeRestaurante { get; set; }
        public string UrlImagemRestaurante { get; set; }
        public DateTime TempoPrevistoEntrega { get; set; }
        public Guid IdCliente { get; set; }
        public List<PratoDto> Itens { get; set; }
        public double? ValorTotal { get; set; }
        public StatusPedido Status { get; set; }
        public EnderecoCliente EnderecoCliente { get; set; }


        public void AtribuirDadosBasicosRestaurante(List<RestaurantePedidoDto> dadosBasicosRestaurantes)
        {
            var dadosBasicos = dadosBasicosRestaurantes.FirstOrDefault(x => x.Id == IdRestaurante);

            NomeRestaurante = dadosBasicos.NomeRestaurante;
            UrlImagemRestaurante = dadosBasicos.UrlLogo;
            TempoPrevistoEntrega = CalcularTempoMedioEntrega(dadosBasicos.TempoMedioEntrega);
        }

        private DateTime CalcularTempoMedioEntrega(int previsaoEmMinutos)
        {
            var horaPrevista = DateTime.Now.AddMinutes(previsaoEmMinutos);
            return horaPrevista;
        }
    }
}
