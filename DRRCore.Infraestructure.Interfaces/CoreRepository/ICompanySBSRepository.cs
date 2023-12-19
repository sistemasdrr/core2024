using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface ICompanySBSRepository: IBaseRepository<CompanySb>
    {
        Task<int> AddCompanySBS(CompanySb companySb);
        Task<int> UpdateCompanySBS(CompanySb companySb);
        Task<CompanySb> GetByIdCompany(int idCompany);
    }
}
