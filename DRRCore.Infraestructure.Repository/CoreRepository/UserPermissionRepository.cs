using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class UserPermissionRepository: IUserPermissionRepository
    {
        private readonly ILogger _logger;
        public UserPermissionRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(UserPermission obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.UserPermissions.AddAsync(obj);
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
                    var obj = await context.UserPermissions.FindAsync(id) ?? throw new Exception("No existe el país solicitado");
                    obj.Enable = false;
                    context.UserPermissions.Update(obj);
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

        public Task<List<UserPermission>> GetAllAsync()
        {
           throw new NotImplementedException();
        }

        public async Task<UserPermission> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.UserPermissions.FindAsync(id) ?? throw new Exception("No existe el objeto solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<UserPermission>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(UserPermission obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    context.UserPermissions.Update(obj);
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
