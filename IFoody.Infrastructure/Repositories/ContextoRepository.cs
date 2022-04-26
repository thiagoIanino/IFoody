using IFoody.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IFoody.Infrastructure.Repositories
{
    public class ContextoRepository : IContextoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextoRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string ObterTokenAutenticacaoHeader()
        {
            var teste = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var teste2 = _httpContextAccessor.HttpContext.Request.Headers["Pepino"];
            return _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        }
        public Guid ObterIdUsuarioAutenticado()
        {
           
            var id = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Guid.Parse(id);
        }
       
    }
}
