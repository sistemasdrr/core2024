using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Infraestructure.Interfaces.CoreRepository;

namespace DRRCore.Domain.Main.CoreDomain
{
    public class CompanyDomain : ICompanyDomain
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyDomain(ICompanyRepository companyRepository) { 
          _companyRepository = companyRepository;
        }
        public async Task<bool> AddAsync(Company obj)
        {
            return await _companyRepository.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _companyRepository.DeleteAsync(id);
        }

        public Task<List<Company>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _companyRepository.GetByIdAsync(id);
        }

        public async Task<List<Company>> GetByNameAsync(string name, string form, int idCountry)
        {
            return await _companyRepository.GetByNameAsync(name,form,idCountry);
        }

        public async Task<bool> UpdateAsync(Company obj)
        {
            return await _companyRepository.UpdateAsync(obj);

        }

       
    }
}
