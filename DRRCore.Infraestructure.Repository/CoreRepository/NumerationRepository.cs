using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class NumerationRepository : INumerationRepository
    {
        private readonly ILogger _logger;
        public NumerationRepository(ILogger logger)
        {
            _logger = logger;
        }

        public Task<Numeration> GetOrderNumberAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Numeration> GetTicketNumberAsync()
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Numerations.Where(x=>x.Name== "NUM_TICKET").FirstOrDefaultAsync() ?? throw new Exception("No existe el objeto solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateTicketNumberAsync()
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var number = await context.Numerations.Where(x => x.Name == "NUM_TICKET").FirstOrDefaultAsync() ?? throw new Exception("No existe el objeto solicitado");

                    number.UpdateDate = DateTime.Now;
                    number.Number++;
                    context.Numerations.Update(number);
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
