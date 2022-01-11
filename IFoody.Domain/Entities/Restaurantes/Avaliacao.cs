using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities.Restaurantes
{
    public class Avaliacao
    {
        public Avaliacao(int nota,string descricao,Guid idRestaurante, Guid idCliente)
        {
            Id = Guid.NewGuid();
            Nota = nota;
            Descricao = descricao;
            Data = DateTime.Now;
            IdRestaurante = idRestaurante;
            IdCliente = idCliente;
        
        }

        public Avaliacao(Guid idRestaurante)
        {
            Id = Guid.NewGuid();
            Nota = 1;
            Descricao = null;
            Data = DateTime.Now;
            IdRestaurante = idRestaurante;

        }

        public Guid Id { get; set; }
        public int? Nota { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public Guid IdRestaurante { get; set; }
        public Guid IdCliente { get; set; }

    }
}
