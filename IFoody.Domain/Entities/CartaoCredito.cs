using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities
{
    public class CartaoCredito
    {
        public CartaoCredito(Guid idCliente,string numero,DateTime validade, string nomeTitular,string cpf)
        {
            IdCartao = Guid.NewGuid();
            IdCliente = idCliente;
            Numero = numero;
            NumeroMascarado = MascararNumeroCartao();
            Validade = validade;
            NomeTitular = nomeTitular;
            Cpf = cpf;
        }

        public CartaoCredito(Guid idCliente,Guid idCartao, string numeroMascarado)
        {
            IdCartao = idCartao;
            IdCliente = idCliente;
            NumeroMascarado = numeroMascarado;
        }

        public Guid IdCartao { get; set;}
        public Guid IdCliente { get; set;}
        public string Numero { get; set;}
        public string NumeroMascarado { get; set;}
        public DateTime Validade { get; set;}
        public string NomeTitular  { get; set;}
        public string Cpf { get; set;}

        public string MascararNumeroCartao()
        {
            var ultimosDigitos = Numero.Substring(12, 4);
            var mascara = new String('*', 12);

            return String.Concat(mascara, ultimosDigitos);
        }

    }
}
