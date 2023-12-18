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
        Task<Response<int>> AddOrUpdateCompanyFinancialInformationAsync(AddOrUpdateCompanyFinancialInformationRequestDto obj);
        Task<Response<GetCompanyBackgroundResponseDto>> GetCompanyBackgroundById(int id);
        Task<Response<GetCompanyFinancialInformationResponseDto>> GetCompanyFinancialInformationById(int id);
        Task<Response<GetCompanyFinancialInformationResponseDto>> GetCompanyFinancialInformationByIdCompany(int idCompany);
        Task<Response<bool>> ActiveWebVisionAsync(int id);
        Task<Response<bool>> DesactiveWebVisionAsync(int id);
        Task<Response<bool>> AddOrUpdateSaleHistoryAsync(AddOrUpdateSaleHistoryRequestDto obj);
        Task<Response<List<GetListSalesHistoryResponseDto>>> GetListSalesHistoriesByIdCompany(int idCompany);
        Task<Response<GetSaleHistoryResponseDto>> GetSaleHistoryById(int id);
        Task<Response<bool>> DeleteSaleHistory(int id);
        Task<Response<bool>> AddOrUpdateFinancialBalanceAsync(AddOrUpdateFinancialBalanceRequestDto obj);
        Task<Response<List<GetComboValueResponseDto>>> GetListFinancialBalanceAsync(int idCompany, string balanceType);
        Task<Response<GetFinancialBalanceResponseDto>> GetFinancialBalanceById(int id);
        Task<Response<bool>> DeleteFinancialBalance(int id);
        Task<Response<bool>> AddOrUpdateProviderAsync(AddOrUpdateProviderRequestDto obj);
        Task<Response<List<GetListProviderResponseDto>>> GetListProvidersAsync(int idCompany);
        Task<Response<GetProviderResponseDto>> GetProviderById(int id);
        Task<Response<bool>> DeleteProvider(int id);
        Task<Response<bool>> AddOrUpdateComercialLatePaymentAsync(AddOrUpdateComercialLatePaymentRequestDto obj);
        Task<Response<List<GetListComercialLatePaymentResponseDto>>> GetListComercialLatePaymentAsync(int idCompany);
        Task<Response<GetComercialLatePaymentResponseDto>> GetComercialLatePaymentById(int id);
        Task<Response<bool>> DeleteComercialLatePayment(int id);
        Task<Response<bool>> AddOrUpdateBankDebtAsync(AddOrUpdateBankDebtRequestDto obj);
        Task<Response<List<GetListBankDebtResponseDto>>> GetListBankDebtAsync(int idCompany);
        Task<Response<GetBankDebtResponseDto>> GetBankDebtById(int id);
        Task<Response<bool>> DeleteBankDebt(int id);
    }
}
