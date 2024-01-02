using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.Abstractions
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}