using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Interfaces.Services
{
    public interface IDominioClienteService
    {
       void ValidarDadosCadastroCliente(Cliente cliente);
       void ValidarDadosAutenticacao(string email, string senha);
        void VerificarSeClienteEstaAutenticado(Cliente cliente);
        void ValidarDadosCartaoCliente(CartaoCredito cartao);
    }
}
