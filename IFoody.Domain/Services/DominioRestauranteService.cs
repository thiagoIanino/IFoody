using IFoody.Domain.Core.Services;
using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Entities.Restaurantes;
using IFoody.Domain.Enumeradores.Cliente;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFoody.Domain.Services
{
    public class DominioRestauranteService : DomainService, IDominioRestauranteService
    {
        private readonly ITokenRepository _tokenRepository;
        public DominioRestauranteService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

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
            } if (string.IsNullOrWhiteSpace(restaurante.SubDescricao) || restaurante.SubDescricao.Length > 30)
            {
                throw new Exception("A Subdescrição não pode ser nula ou passar de 30 caracteres");
            }
            if(restaurante.TempoMedioEntrega is null || restaurante.TempoMedioEntrega.ToString().Length > 3)
            {
                throw new Exception("Tempo médio de entrega vazio ou invalido");
            }
        }
        public void ValidarTipoRestaurante(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
            {
                throw new Exception("O tipo do restaurante não pode ser nulo");
            }
        }
        public void ValidarRestornoListaRestaurantes(IEnumerable<RestauranteDto> listaRestaurantes)
        {
            if (listaRestaurantes is null || !listaRestaurantes.Any())
            {
                throw new Exception("Nenhum restaurante com esse tipo foi encontrado");
            }
        }

        public string AutenticarRestaurante(Restaurante restaurante)
        {
            if (restaurante is null)
            {
                throw new Exception("Email ou senha incorretos");
            }
            return _tokenRepository.GenerateToken(restaurante.Role, restaurante.Email, restaurante.Id);
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
