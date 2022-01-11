using IFoody.Application.Interfaces;
using IFoody.Application.Mapping;
using IFoody.Application.Models;
using IFoody.Application.Models.Restaurantes;
using IFoody.Domain.Entities;
using IFoody.Domain.Entities.Restaurantes;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using IFoody.Domain.Repositories.Restaurantes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Application
{
    public class RestauranteApplication : IRestauranteApplication
    {
        private readonly IRestauranteRepository _restauranteService;
        private readonly IDominioRestauranteService _dominioRestauranteService;
        private readonly IAvaliacaoRepository _avaliacaoService;
        private readonly IStatusAvaliacaoRepository _statusAvaliacaoService;
        public RestauranteApplication(IRestauranteRepository restauranteService, IDominioRestauranteService dominioRestauranteService, IAvaliacaoRepository avaliacaoService, IStatusAvaliacaoRepository statusAvaliacaoService)
        {
            _restauranteService = restauranteService;
            _dominioRestauranteService = dominioRestauranteService;
            _avaliacaoService = avaliacaoService;
            _statusAvaliacaoService = statusAvaliacaoService;
        }

        public async Task CadastrarRestaurante(RestauranteInput restauranteInput)
        {
            var restaurante = new Restaurante(
                restauranteInput.NomeRestaurante,
                restauranteInput.NomeDonoRestaurante,
                restauranteInput.Tipo,
                restauranteInput.CNPJ,
                restauranteInput.Email,
                restauranteInput.Senha);
            _dominioRestauranteService.ValidarDadosCadastroRestaurante(restaurante);

            var avaliacaoPadrao = new Avaliacao(restaurante.Id);
            var statusAvaliacaoRestaurante = new StatusAvaliacao(restaurante.Id);


            await _restauranteService.GravarRestaurante(restaurante);
            await _avaliacaoService.RegistrarInicioRestauranteAvaliacao(avaliacaoPadrao);
            await _statusAvaliacaoService.GravarStatusAvaliacaoRestaurante(statusAvaliacaoRestaurante);
        }

        public async Task AutenticarRestaurante(string email, string senha)
        {
            _dominioRestauranteService.ValidarDadosAutenticacao(email, senha);
            var situacaoAutenticacaoCliente = await _restauranteService.AutenticarRestaurante(email, senha);
            _dominioRestauranteService.VerificarSeRestauranteEstaAutenticado(situacaoAutenticacaoCliente);
        }

        public async Task AvaliarRestaurante(AvaliacaoInput avaliacaoInput)
        {
            var avaliacao = new Avaliacao(
                avaliacaoInput.Nota,
                avaliacaoInput.Descricao,
                avaliacaoInput.IdRestaurante,
                avaliacaoInput.IdCliente);

            _dominioRestauranteService.ValidarDadosAvaliacaoRestaurante(avaliacao);
            await _avaliacaoService.AvaliarRestaurante(avaliacao);
        }

        public async Task<IEnumerable<RestauranteModel>> ListarRestaurantesPorTipo(string tipo)
        {

            _dominioRestauranteService.ValidarTipoRestaurante(tipo);
            var restaurantes = await _restauranteService.ListarRestaurantesPorTipo(tipo);
            _dominioRestauranteService.ValidarRestornoListaRestaurantes(restaurantes);         

            return restaurantes.ToRestauranteListModel();
        }

        public async Task<IEnumerable<RestauranteModel>> ListarRestaurantesPorClassificacao()
        {
            var restaurantes = await _restauranteService.ListarRestaurantesPorClassificacaoConsiderandoCache();
            _dominioRestauranteService.ValidarRestornoListaRestaurantes(restaurantes);

            return restaurantes.ToRestauranteListModel();
        }
    }
}
