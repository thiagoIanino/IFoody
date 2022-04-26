using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class GrupoPratos
    {
        public ClassificacaoPrato Classificacao { get; set; }
        public List<Prato> Pratos { get; set; }
    }
}
