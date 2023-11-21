using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface ICompanyApplication
    {
        Task<Response<bool>> AddOrUpdateAsync(AddOrUpdateCompanyRequestDto obj);
        Task<Response<GetCompanyResponseDto>> GetCompanyById(int id);
        Task<Response<List<GetCompanyResponseDto>>> GetAllCompanys(string name,string form,int idCountry);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<bool>> AddOrUpdateCompanyBackGroundAsync(AddOrUpdateCompanyBackgroundRequestDto obj);
    }
}
