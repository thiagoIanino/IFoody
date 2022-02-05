using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class UsuarioStripeDto
    {
        public UsuarioStripeDto(string nome, string email, CategoriaStripe categoria)
        {
            Nome = nome;
            Email = email;
            Categoria = categoria;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public CategoriaStripe Categoria { get; set; }
    }
}
