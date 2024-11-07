using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoStarCTF.Service.IService
{
    public interface IAuthService
    {
        ValueTask<string> GenerateToken(string email, string password);
    }
}
