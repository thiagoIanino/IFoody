using IFoody.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Repositories
{
    public interface IPagamentoRepository
    {
        void EnviarCobrancaFila(PedidoGeralDto pedidos);
        Task<string> CadastrarUsuarioStripe(UsuarioStripeDto restaurante);
        Task<string> CadastrarCartaoStripe(CartaoStripeDto cartao);
    }
}