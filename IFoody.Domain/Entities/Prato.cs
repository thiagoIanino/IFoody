using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities
{
    public class Prato
    {
        public Prato(Guid id,string nomePrato, string descricao, string urlImagem, double valor, Guid idRestaurante)
        {
            Id = id;
            NomePrato = nomePrato;
            Descricao = descricao;
            UrlImagem = urlImagem;
            Valor = valor;
            IdRestaurante = idRestaurante;
        }
        public Prato(string nomePrato, string descricao, string urlImagem, double valor, Guid idRestaurante)
        {
            Id = Guid.NewGuid();
            NomePrato = nomePrato;
            Descricao = descricao;
            UrlImagem = urlImagem;
            Valor = valor;
            IdRestaurante = idRestaurante;
        }
        public Guid Id { get; set; }
        public string NomePrato { get; set; }
        public string Descricao { get; set; }
        public string UrlImagem { get; set; }
        public double? Valor { get; set; }
        public Guid IdRestaurante { get; set; }

    }
}
