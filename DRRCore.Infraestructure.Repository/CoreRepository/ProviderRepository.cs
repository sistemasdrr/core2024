using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ILogger _logger;
        public ProviderRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(Provider obj)
        {
            try
            {
                using var context = new SqlCoreContext();
                await context.Providers.AddAsync(obj);
                await context.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                var provider = await context.Providers.FindAsync(id);
                if(provider != null)
                {
                    provider.Enable = false;
                    provider.DeleteDate = DateTime.Now; 
                    context.Providers.Update(provider);
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

        public Task<List<Provider>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Provider> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                var provider = await context.Providers.FindAsync(id);
                if (provider != null)
                {
                    return provider;
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

        public Task<List<Provider>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Provider>> GetProviderByIdCompany(int idCompany)
        {
            try
            {
                using var context = new SqlCoreContext();
                var list = await context.Providers.Include(x => x.IdCountryNavigation).Where(x => x.IdCompany == idCompany && x.Enable == true).ToListAsync();
                return list;
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Provider obj)
        {
            try
            {
                using var context = new SqlCoreContext();
                obj.UpdateDate = DateTime.Now;
                obj.LastUpdateUser = 1;
                context.Providers.Update(obj);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
