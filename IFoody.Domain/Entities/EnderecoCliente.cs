using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities
{
    public class EnderecoCliente
    {
        public EnderecoCliente(Guid idEndereco, Guid idClientePedido, string primeiraLinhaEnd, string segundaLinhaEnd)
        {
            Id = idEndereco;
            IdCliente = idClientePedido;
            PrimeiraLinhaEnd = primeiraLinhaEnd;
            SegundaLinhaEnd = segundaLinhaEnd;
        }

        public EnderecoCliente()
        {

        }

        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public string PrimeiraLinhaEnd { get; set; }
        public string SegundaLinhaEnd { get; set; }
        
    }
}
