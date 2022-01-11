using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Exceptions
{
    public class RequisicaoNaoProcessadaExcecao : Exception
    {
        public string CodigoErro {get; set;}

        public RequisicaoNaoProcessadaExcecao(string mensagem, string codigoErro = null) : base(mensagem)
        {
            CodigoErro = codigoErro;
        }

    }
}
