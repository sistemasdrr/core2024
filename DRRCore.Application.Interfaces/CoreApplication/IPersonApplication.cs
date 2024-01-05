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

        Task<Response<int?>> AddOrUpdatePersonHomeAsync(AddOrUpdatePersonHomeRequestDto obj);
        Task<Response<GetPersonHomeResponseDto>> GetPersonHomeByIdPerson(int idPerson);
        Task<Response<int?>> AddOrUpdatePersonJobAsync(AddOrUpdatePersonJobRequestDto obj);
        Task<Response<GetPersonJobResponseDto>> GetPersonJobByIdPerson(int idPerson);
        Task<Response<int?>> AddOrUpdatePersonActivitiesAsync(AddOrUpdatePersonActivitiesRequestDto obj);
        Task<Response<GetPersonActivitiesResponseDto>> GetPersonActivitiesByIdPerson(int idPerson);
        Task<Response<int?>> AddOrUpdatePersonPropertyAsync(AddOrUpdatePersonPropertyRequestDto obj);
        Task<Response<GetPersonPropertyResponseDto>> GetPersonPropertyByIdPerson(int idPerson);
        Task<Response<int?>> AddOrUpdatePersonSBSAsync(AddOrUpdatePersonSbsRequestDto obj);
        Task<Response<GetPersonSbsResponseDto>> GetPersonSBSById(int idPerson);
        Task<Response<int?>> AddOrUpdatePersonHistoryAsync(AddOrUpdatePersonHistoryRequestDto obj);
        Task<Response<GetPersonHistoryResponseDto>> GetPersonHistoryByIdPerson(int idPerson);
        Task<Response<int?>> AddOrUpdatePersonGeneralInfoAsync(AddOrUpdatePersonGeneralInfoRequestDto obj);
        Task<Response<GetPersonGeneralInfoResponseDto>> GetPersonGeneralInfoByIdPerson(int idPerson);
    }
}
