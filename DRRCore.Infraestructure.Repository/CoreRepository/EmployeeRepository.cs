using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger _logger;
        public EmployeeRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(Employee obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.Employees.AddAsync(obj);
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
                    var obj = await context.Employees.FindAsync(id) ?? throw new Exception("No existe el empleado solicitado");
                    obj.Enable = false;
                    context.Employees.Update(obj);
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

        public async Task<List<Employee>> GetAllAsync()
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Employee>();
            }
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Employees.FindAsync(id) ?? throw new Exception("No existe el empleado solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Employees.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Employee>();
            }
        }

        public async Task<bool> UpdateAsync(Employee obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    context.Employees.Update(obj);
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
