using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFoody.Domain.Services
{
    public class DominioPratoService : IDominioPratoService
    {

        public void ValidarDadosCadastroPrato(Prato prato)
        {
            if (string.IsNullOrWhiteSpace(prato.NomePrato))
            {
                throw new Exception("O campo Prato não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(prato.Descricao))
            {
                throw new Exception("O campo Descricao não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(prato.UrlImagem))
            {
                throw new Exception("O campo de imagem não pode ser vazio");
            }
            if (prato.Valor is null)
            {
                throw new Exception("O valor não pode ser vazio");
            }
            if (prato.IdRestaurante == Guid.Empty)
            {
                throw new Exception("Não foi possivel recuperar o identificador do Restaurante");
            }
        }

        public void ValidarIdRestaurante(Guid idRestaurante)
        {
            if(idRestaurante == Guid.Empty)
            {
                throw new Exception("Id do restaurante inválido");
            }
        }

        public List<GrupoPratos> AgruparPratosPorClassificacao(IEnumerable<Prato> pratos)
        {
            var gruposPratos = new List<GrupoPratos>();
            
            foreach(var prato in pratos)
            {
                var grupo = gruposPratos.FirstOrDefault(x => x.Classificacao == prato.Classificacao);
                if (grupo is null)
                {
                    gruposPratos.Add(
                        new GrupoPratos {
                            Classificacao = prato.Classificacao,
                            Pratos = new List<Prato> {
                                prato 
                            }
                        }
                        );
                }
                else
                {
                    grupo.Pratos.Add(prato);
                }
            }

            return gruposPratos.OrderBy(x => (int)x.Classificacao).ToList();
        }
    }

}
