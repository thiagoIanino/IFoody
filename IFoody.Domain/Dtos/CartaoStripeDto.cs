using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class CartaoStripeDto
    {
        public CartaoStripeDto(string idStripeCliente, string numero, DateTime validade, string nomeTitular, string cpf)
        {
            IdStripeCliente = idStripeCliente;
            Numero = numero;
            Validade = validade;
            NomeTitular = nomeTitular;
            Cpf = cpf;
        }

        public string IdStripeCliente { get; set; }
        public string Numero { get; set; }
        public DateTime Validade { get; set; }
        public string NomeTitular { get; set; }
        public string Cpf { get; set; }
    }
}
