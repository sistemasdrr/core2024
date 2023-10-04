using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class RolRepository
    {
        private readonly ILogger _logger;
        public RolRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(Rol obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.Rols.AddAsync(obj);
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

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var obj = await context.Rols.FindAsync(id) ?? throw new Exception("No existe el país solicitado");
                    obj.Enable = false;
                    context.Rols.Update(obj);
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

        public Task<List<Rol>> GetAllAsync()
        {
          throw new NotImplementedException();
        }

        public async Task<Rol> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Rols.FindAsync(id) ?? throw new Exception("No existe el país solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public Task<List<Rol>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Rol obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    context.Rols.Update(obj);
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
