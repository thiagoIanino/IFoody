using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Exceptions
{
    public class RequisicaoFalha : Exception
    {
        public RequisicaoFalha(Exception ex) : base("Erro na integração com serviço de Pagamento",ex)
        {

        }
    }
}
