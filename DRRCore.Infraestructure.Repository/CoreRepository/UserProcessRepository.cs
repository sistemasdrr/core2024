using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class UserProcessRepository: IUserProcessRepository
    {
        private readonly ILogger _logger;
        public UserProcessRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(UserProcess obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.UserProcesses.AddAsync(obj);
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
                    var obj = await context.UserProcesses.FindAsync(id) ?? throw new Exception("No existe el país solicitado");
                    obj.Enable = false;
                    context.UserProcesses.Update(obj);
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

        public Task<List<UserProcess>> GetAllAsync()
        {
           throw new NotImplementedException();
        }

        public async Task<UserProcess> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.UserProcesses.FindAsync(id) ?? throw new Exception("No existe el objeto solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<UserProcess>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(UserProcess obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    context.UserProcesses.Update(obj);
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
