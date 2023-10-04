using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class PermissionRepository:IPermissionRepository
    {
        private readonly ILogger _logger;
        public PermissionRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(Permission obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.Permissions.AddAsync(obj);
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
                    var obj = await context.Permissions.FindAsync(id) ?? throw new Exception("No existe el país solicitado");
                    obj.Enable = false;
                    context.Permissions.Update(obj);
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

        public async Task<List<Permission>> GetAllAsync()
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Permissions.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Permission>();
            }
        }

        public async Task<Permission> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Permissions.FindAsync(id) ?? throw new Exception("No existe el país solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public Task<List<Permission>> GetByNameAsync(string name)
        {
           throw new NotImplementedException(); 
        }

        public async Task<List<Permission>> GetByRol(int idRol)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Permissions.Where(x=>x.IdRol==idRol).ToListAsync() ?? throw new Exception("No existe el país solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Permission>();
            }
        }

        public async Task<bool> UpdateAsync(Permission obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    context.Permissions.Update(obj);
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
