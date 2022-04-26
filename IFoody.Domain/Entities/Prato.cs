using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities
{
    public class Prato
    {
        public Prato(Guid id,string nomePrato, string descricao, string urlImagem, double valor, Guid idRestaurante,int classificacao)
        {
            Id = id;
            NomePrato = nomePrato;
            Descricao = descricao;
            UrlImagem = urlImagem;
            Valor = valor;
            IdRestaurante = idRestaurante;
            Classificacao = (ClassificacaoPrato)classificacao;
        }
        public Prato(string nomePrato, string descricao, string urlImagem, double valor, Guid idRestaurante,ClassificacaoPrato classificacao)
        {
            Id = Guid.NewGuid();
            NomePrato = nomePrato;
            Descricao = descricao;
            UrlImagem = urlImagem;
            Valor = valor;
            IdRestaurante = idRestaurante;
            Classificacao = classificacao;
        }
        public Guid Id { get; set; }
        public string NomePrato { get; set; }
        public string Descricao { get; set; }
        public string UrlImagem { get; set; }
        public double? Valor { get; set; }
        public Guid IdRestaurante { get; set; }
        public ClassificacaoPrato Classificacao { get; set; }

    }
}
