using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class CompanySBSRepository : ICompanySBSRepository
    {
        private readonly ILogger _logger;
        public CompanySBSRepository(ILogger logger)
        {
            _logger = logger;
        }

        public Task<bool> AddAsync(CompanySb obj)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddCompanySBS(CompanySb companySb)
        {
            try
            {
                using var context = new SqlCoreContext();
                await context.CompanySbs.AddAsync(companySb);
                await context.SaveChangesAsync();
                return companySb.Id;
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return 0; 
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                var companySbs = await context.CompanySbs.FindAsync(id);
                if(companySbs != null)
                {
                    companySbs.Enable = false;
                    companySbs.DeleteDate = DateTime.Now;
                    companySbs.LastUpdateUser = 1;
                    context.CompanySbs.Update(companySbs);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public Task<List<CompanySb>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CompanySb> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                var companySbs = await context.CompanySbs.FindAsync(id);
                if (companySbs != null)
                {
                    return companySbs;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<CompanySb> GetByIdCompany(int idCompany)
        {
            try
            {
                using var context = new SqlCoreContext();
                var companySbs = await context.CompanySbs.FirstOrDefaultAsync(x => x.IdCompany == idCompany);
                if (companySbs != null)
                {
                    return companySbs;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
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
            try
            {
                using var context = new SqlCoreContext();
                context.CompanySbs.Update(companySb);
                await context.SaveChangesAsync();
                return companySb.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return 0;
            }
        }
    }
}
