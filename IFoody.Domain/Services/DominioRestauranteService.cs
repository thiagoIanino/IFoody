using IFoody.Domain.Core.Services;
using IFoody.Domain.Entities;
using IFoody.Domain.Entities.Restaurantes;
using IFoody.Domain.Enumeradores.Cliente;
using IFoody.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFoody.Domain.Services
{
    public class DominioRestauranteService : DomainService, IDominioRestauranteService
    {
        public void ValidarDadosCadastroRestaurante(Restaurante restaurante)
        {
            if (string.IsNullOrWhiteSpace(restaurante.NomeRestaurante))
            {
                throw new Exception("O campo nome do restaurante não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(restaurante.NomeDonoRestaurante))
            {
                throw new Exception("O campo nome do dono do restaurante não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(restaurante.Tipo))
            {
                throw new Exception("O campo tipo não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(restaurante.CNPJ))
            {
                throw new Exception("O campo CNPJ não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(restaurante.Email))
            {
                throw new Exception("O campo Email não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(restaurante.Senha))
            {
                throw new Exception("O campo Senha não pode ser vazio");
            }
        }
        public void ValidarTipoRestaurante(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
            {
                throw new Exception("O tipo do restaurante não pode ser nulo");
            }
        }
        public void ValidarRestornoListaRestaurantes(IEnumerable<Restaurante> listaRestaurantes)
        {
            if (listaRestaurantes is null || !listaRestaurantes.Any())
            {
                throw new Exception("Nenhum restaurante com esse tipo foi encontrado");
            }
        }

        public void VerificarSeRestauranteEstaAutenticado(int situacaoAutenticacao)
        {
            if ((SituacaoAutenticacao)situacaoAutenticacao == SituacaoAutenticacao.NaoAutenticado)
            {
                throw new Exception("Email ou senha incorretos");
            }
        }
        public void ValidarDadosAvaliacaoRestaurante(Avaliacao avaliacao)
        {
            if (avaliacao.Nota is null)
            {
                throw new Exception("O nota tem que ser preenchida");
            }
            if (avaliacao.IdCliente == Guid.Empty)
            {
                throw new Exception("Falha ao recuperar identificador do Cliente");
            }
            if (avaliacao.IdRestaurante == Guid.Empty)
            {
                throw new Exception("Falha ao recuperar identificador do Restaurante");
            }
        }
    }
}
