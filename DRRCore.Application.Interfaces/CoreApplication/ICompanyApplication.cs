using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface ICompanyApplication
    {
        Task<Response<int>> AddOrUpdateAsync(AddOrUpdateCompanyRequestDto obj);
        Task<Response<GetCompanyResponseDto>> GetCompanyById(int id);
        Task<Response<List<GetListCompanyResponseDto>>> GetAllCompanys(string name,string form,int idCountry,bool haveReport);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<bool>> AddOrUpdateCompanyBackGroundAsync(AddOrUpdateCompanyBackgroundRequestDto obj);
        Task<Response<GetCompanyBackgroundResponseDto>> GetCompanyBackgroundById(int id);
        Task<Response<bool>> ActiveWebVisionAsync(int id);
        Task<Response<bool>> DesactiveWebVisionAsync(int id);
    }
}
