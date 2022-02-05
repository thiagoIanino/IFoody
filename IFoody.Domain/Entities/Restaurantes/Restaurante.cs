using IFoody.Domain.Enumeradores.Avaliacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities.Restaurantes
{
    public class Restaurante
    {
        public Restaurante(string nomeRestaurante,string nomeDonoRestaurante,string tipo,string cnpj,string email, string senha)
        {
            Id = Guid.NewGuid();
            NomeRestaurante = nomeRestaurante;
            NomeDonoRestaurante = nomeDonoRestaurante;
            Tipo = tipo;
            CNPJ = cnpj;
            Email = email;
            Senha = senha;
        }

        public Restaurante(Guid id, string nomeRestaurante, string tipo, int status, double nota)
        {
            Id = id;
            NomeRestaurante = nomeRestaurante;
            Tipo = tipo;
            Nota = nota;
            Status = (StatusAvaliacaoRestaurante)status;
        }

        public Guid Id { get; set; }
        public string IdStripe { get; set; }
        public string NomeRestaurante { get; set;}
        public string NomeDonoRestaurante { get; set; }
        public string Tipo { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public double Nota { get; set; }
        public StatusAvaliacaoRestaurante Status { get; set; }


        public void AdicionarIdStripe(string id)
        {
            IdStripe = id;
        }
    }
}
