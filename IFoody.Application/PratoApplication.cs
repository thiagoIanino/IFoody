using IFoody.Application.Interfaces;
using IFoody.Application.Models;
using IFoody.Domain.Entities;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Application
{
    public class PratoApplication : IPratoApplication
    {
        private readonly IPratoRespository _pratoService;
        private readonly IDominioPratoService _dominioPratoService;
        private readonly IContextoRepository _contextoRepository;

        public PratoApplication(IPratoRespository pratoService, IDominioPratoService dominioPratoService,
            IContextoRepository contextoRepository)
        {
            _pratoService = pratoService;
            _dominioPratoService = dominioPratoService;
            _contextoRepository = contextoRepository;
        }

        public async Task CadastrarPrato(PratoInput pratoInput)
        {
            var idRestaurante = _contextoRepository.ObterIdUsuarioAutenticado();
            var prato = new Prato(
                pratoInput.NomePrato,
                pratoInput.Descricao,
                pratoInput.UrlImagem,
                pratoInput.Valor,
                idRestaurante,
                pratoInput.Classificacao);

            _dominioPratoService.ValidarDadosCadastroPrato(prato);
            await _pratoService.GravarPrato(prato);

        }

        public async Task DeletarPrato(Guid idPrato)
        {
            if (idPrato == Guid.Empty)
                throw new Exception("Identificador do prato vazio");
            await _pratoService.DeletarPrato(idPrato);

        }

        public async Task<IEnumerable<Prato>> ListarPratosPorRestaurante(Guid idRestaurante)
        {
            _dominioPratoService.ValidarIdRestaurante(idRestaurante);
            return await _pratoService.ListarPratosRestaurante(idRestaurante);
        }

    }
}
