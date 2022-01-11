using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Core.Services
{
    public class DomainService
    {
        public void ValidarDadosAutenticacao(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("O campo Email não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new Exception("O campo Senha não pode ser vazio");
            }
        }

    }
}
