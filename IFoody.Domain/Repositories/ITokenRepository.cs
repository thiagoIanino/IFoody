using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Repositories
{
    public interface ITokenRepository
    {
        string GenerateToken(string role, string email, Guid id);
    }
}
