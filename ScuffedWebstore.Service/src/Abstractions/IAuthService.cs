using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IAuthService
{
    public string Login(string email, string password);
}