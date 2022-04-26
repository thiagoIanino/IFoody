using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities
{
    public class CartaoCredito
    {
        public CartaoCredito(Guid idCliente,string numero,DateTime validade, string nomeTitular,string cpf,string bandeira, string cvv)
        {
            IdCartao = Guid.NewGuid();
            IdCliente = idCliente;
            Numero = numero;
            NumeroMascarado = MascararNumeroCartao();
            Validade = validade;
            NomeTitular = nomeTitular;
            Cpf = cpf;
            Bandeira = bandeira;
            Cvv = cvv;
        }

        public CartaoCredito(Guid idCliente,Guid idCartao, string numeroMascarado,string bandeira)
        {
            IdCartao = idCartao;
            IdCliente = idCliente;
            NumeroMascarado = numeroMascarado;
            Bandeira = bandeira;
        }

        public Guid IdCartao { get; set;}
        public Guid IdCliente { get; set;}
        public string IdStripe { get; set;}
        public string Numero { get; set;}
        public string NumeroMascarado { get; set;}
        public DateTime Validade { get; set;}
        public string NomeTitular  { get; set;}
        public string Cpf { get; set;}
        public string Bandeira { get; set;}
        public string Cvv { get; set;}

        public string MascararNumeroCartao()
        {
            var ultimosDigitos = Numero.Substring(12, 4);
            var mascara = new String('*', 12);

            return String.Concat(mascara, ultimosDigitos);
        }

        public void AdicionarIdCartaoStripe(string idCartao)
        {
            IdStripe = idCartao;
        }

    }
}
