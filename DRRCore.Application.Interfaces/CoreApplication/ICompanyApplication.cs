using DRRCore.Application.DTO.Core.Request;
using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface ICompanyApplication
    {
        Task<Response<bool>> AddOrUpdateAsync(AddOrUpdateCompanyRequestDto obj);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
