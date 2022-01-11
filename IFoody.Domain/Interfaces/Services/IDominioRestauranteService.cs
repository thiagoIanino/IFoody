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
        void VerificarSeRestauranteEstaAutenticado(int situacaoAutenticacao);
        void ValidarDadosAvaliacaoRestaurante(Avaliacao avaliacao);
        void ValidarTipoRestaurante(string tipo);
        void ValidarRestornoListaRestaurantes(IEnumerable<Restaurante> listaRestaurantes);
    }
}
