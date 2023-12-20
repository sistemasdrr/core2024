using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Infraestructure.Interfaces.CoreRepository;

namespace DRRCore.Domain.Main.CoreDomain
{
    public class CompanySBSDomain : ICompanySBSDomain
    {
        private readonly ICompanySBSRepository _companySBSRepository;
        public CompanySBSDomain(ICompanySBSRepository companySBSRepository)
        {
            _companySBSRepository = companySBSRepository;
        }

        public Task<bool> AddAsync(CompanySb obj)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddCompanySBS(CompanySb companySb)
        {
            return await _companySBSRepository.AddCompanySBS(companySb);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _companySBSRepository.DeleteAsync(id);
        }

        public Task<List<CompanySb>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CompanySb> GetByIdAsync(int id)
        {
            return await _companySBSRepository.GetByIdAsync(id);
        }

        public async Task<CompanySb> GetByIdCompany(int idCompany)
        {
            return await _companySBSRepository.GetByIdCompany(idCompany);
        }

        public Task<List<CompanySb>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CompanySb obj)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateCompanySBS(CompanySb companySb)
        {
            return await _companySBSRepository.UpdateCompanySBS(companySb);
        }
    }
}
