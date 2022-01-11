using IFoody.Domain.Core.Services;
using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores.Cliente;
using IFoody.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Services
{
   public class DominioClienteService : DomainService, IDominioClienteService
    {
        public void ValidarDadosCadastroCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                throw new Exception("O campo Nome não pode ser vazio");
            } 
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                throw new Exception("O campo Email não pode ser vazio");
            } if (string.IsNullOrWhiteSpace(cliente.Senha))
            {
                throw new Exception("O campo Senha não pode ser vazio");
            } 
        }

        public void VerificarSeClienteEstaAutenticado(Cliente cliente) 
        {
            if(cliente is null)
            {
                throw new Exception("Email ou senha incorretos");
            }
        }

        public void ValidarDadosCartaoCliente(CartaoCredito cartao)
        {
            if (cartao.IdCliente == Guid.Empty)
            {
                throw new Exception("O campo Nome não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(cartao.Cpf))
            {
                throw new Exception("O campo Email não pode ser vazio");
            }
            if (cartao.Validade == DateTime.MinValue)
            {
                throw new Exception("O campo Senha não pode ser vazio");
            }  
            if (string.IsNullOrWhiteSpace(cartao.NomeTitular))
            {
                throw new Exception("O campo Senha não pode ser vazio");
            }  if (string.IsNullOrWhiteSpace(cartao.Numero))
            {
                throw new Exception("O campo Senha não pode ser vazio");
            }
        }
    }
}
