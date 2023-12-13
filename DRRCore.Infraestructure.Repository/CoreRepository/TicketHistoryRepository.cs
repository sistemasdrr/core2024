using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class TicketHistoryRepository : ITicketHistoryRepository
    {

        private readonly ILogger _logger;
        public TicketHistoryRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(TicketHistory obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.TicketHistories.AddAsync(obj);
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
                    var obj = await context.TicketHistories.FindAsync(id) ?? throw new Exception("No existe el objeto solicitado");
                    obj.Enable = false;
                    obj.DeleteDate = DateTime.Now;
                    context.TicketHistories.Update(obj);
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

        public async Task<List<TicketHistory>> GetAllAsync()
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.TicketHistories.Where(x => x.Enable == true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<TicketHistory>();
            }
        }

        public async Task<TicketHistory> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.TicketHistories.FindAsync(id) ?? throw new Exception("No existe el objeto solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public Task<List<TicketHistory>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TicketHistory obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {

                    obj.UpdateDate = DateTime.Now;
                    context.TicketHistories.Update(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
