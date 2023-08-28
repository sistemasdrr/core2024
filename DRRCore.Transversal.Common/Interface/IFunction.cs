using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Transversal.Common.Interface
{
    public interface IFunction
    {
        Task<string> Decrypt (string input);
        Task<string> Encrypt(string token);
        Task<string> GetTokenByHeader();
        
    }
}
