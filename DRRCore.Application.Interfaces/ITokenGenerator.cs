using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces
{
    public interface ITokenGenerator
    {
        Task<Response<string>> GetTokenAsync();
    }
}
