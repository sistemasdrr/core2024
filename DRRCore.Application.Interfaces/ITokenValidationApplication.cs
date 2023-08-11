using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces
{
    public interface ITokenValidationApplication
    {
        Task<Response<bool>> ValidationTokenAsync(string token);
        Task<Response<string>> EncriptTokenAsync(string token);
    }
}
