using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface IPersonApplication
    {
        Task<Response<GetPersonResponseDto>> GetPersonById(int id);
        Task<Response<List<GetListPersonResponseDto>>> GetListPerson(string fullname, string form, int idCountry, bool haveReport);
        Task<Response<int>> AddOrUpdatePerson(AddOrUpdatePersonRequestDto obj);
        Task<Response<bool>> DeletePerson(int id);
        Task<Response<bool>> ActivateWebPerson(int id);
        Task<Response<bool>> DesactivateWebPerson(int id);
    }
}
