using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ILogger _logger;
        public CountryRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(Country obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.Countries.AddAsync(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var obj= await context.Countries.FindAsync(id) ?? throw new Exception("No existe el país solicitado");
                    obj.Enable = false;
                    context.Countries.Update(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Country>> GetAllAsync()
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Countries.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Country>();
            }
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Countries.FindAsync(id) ?? throw new Exception("No existe el país solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Country>> GetByNameAsync(string name)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Countries.Where(x=>x.Name.Contains(name) || x.EnglishName.Contains(name)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Country>();
            }
        }

        public async Task<bool> UpdateAsync(Country obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    context.Countries.Update(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
