using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class ClienteDto
    {

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public IEnumerable<CartaoCreditoDto> Cartoes { get; set; }
    }
}
