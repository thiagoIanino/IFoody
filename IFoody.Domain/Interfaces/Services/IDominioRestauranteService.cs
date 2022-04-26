using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Entities.Restaurantes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Interfaces.Services
{
    public interface IDominioRestauranteService
    {
       void ValidarDadosCadastroRestaurante(Restaurante restaurante);
        void ValidarDadosAutenticacao(string email, string senha);
        string AutenticarRestaurante(Restaurante restaurante);
        void ValidarDadosAvaliacaoRestaurante(Avaliacao avaliacao);
        void ValidarTipoRestaurante(string tipo);
        void ValidarRestornoListaRestaurantes(IEnumerable<RestauranteDto> listaRestaurantes);
    }
}
