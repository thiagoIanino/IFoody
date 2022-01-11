using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Repositories
{
    public interface ICartaoCreditoRepository
    {
        Task CadastrarCartao(CartaoCredito cartao);
        Task<IEnumerable<CartaoCredito>> ListarCartoesCliente(Guid idCliente);
    }
}
