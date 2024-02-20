using DRRCore.Application.DTO.Enum;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ILogger _logger;
        public TicketRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(Ticket obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.Tickets.AddAsync(obj);
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

        public async Task<bool> AddTicketQuery(TicketQuery query)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.TicketQueries.AddAsync(query);
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

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var obj = await context.Tickets.FindAsync(id) ?? throw new Exception("No existe el objeto solicitado");
                    obj.Enable = false;
                    obj.DeleteDate = DateTime.Now;
                    context.Tickets.Update(obj);
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

        public async Task<List<Ticket>> GetAllAsync()
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Include(x=>x.IdSubscriberNavigation)
                    .Include(x => x.IdSubscriberNavigation.IdCountryNavigation)
                    .Include(x => x.IdContinentNavigation).Include(x => x.IdCompanyNavigation)
                    .Include(x => x.IdCompanyNavigation.IdCountryNavigation)
                    .Include(x => x.IdCompanyNavigation.IdCountryNavigation.IdContinentNavigation)
                    .Include(x => x.IdPersonNavigation)
                    .Include(x => x.IdPersonNavigation.IdCountryNavigation)
                    .Include(x => x.IdPersonNavigation.IdCountryNavigation.IdContinentNavigation)
                    .Include(x => x.IdCountryNavigation)
                    .Include(x => x.IdStatusTicketNavigation)
                    .Include(x => x.TicketQuery)
                    .Include(x => x.TicketHistories.OrderByDescending(x=>x.Id)).Where(x => x.Enable == true)
                    .OrderByDescending(x => x.OrderDate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Ticket>> GetAllByAsync(string ticket, string name, string subscriber, string type, string procedure)
        {
            try
            {
                using var context = new SqlCoreContext();
                var list= await context.Tickets.Include(x => x.IdSubscriberNavigation)
                    .Include(x => x.IdSubscriberNavigation.IdCountryNavigation)
                    .Include(x => x.IdContinentNavigation).Include(x => x.IdCompanyNavigation)
                    .Include(x => x.IdCompanyNavigation.IdCountryNavigation)
                    .Include(x => x.IdCompanyNavigation.IdCountryNavigation.IdContinentNavigation)
                    .Include(x => x.IdPersonNavigation)
                    .Include(x => x.IdPersonNavigation.IdCountryNavigation)
                    .Include(x => x.TicketQuery)
                    .Include(x => x.IdPersonNavigation.IdCountryNavigation.IdContinentNavigation)
                    .Include(x => x.IdStatusTicketNavigation)
                    .Include(x => x.IdCountryNavigation)
                    .Include(x => x.TicketHistories.OrderByDescending(x => x.Id))
                    .Where(x => x.Enable == true).OrderByDescending(x => x.OrderDate).ToListAsync();


                if (!string.IsNullOrEmpty(ticket))
                {
                    list = list.Where(x => x.Number == int.Parse(ticket)).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    list = list.Where(x => x.BusineesName.Contains(name)).ToList();
                }
                if (!string.IsNullOrEmpty(subscriber))
                {
                    list = list.Where(x => x.IdSubscriberNavigation.Code==subscriber).ToList();
                }
                if (!string.IsNullOrEmpty(type))
                {
                    list = list.Where(x => x.ReportType == type).ToList();
                }
                if (!string.IsNullOrEmpty(procedure))
                {
                    list = list.Where(x => x.ProcedureType == procedure).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Ticket>> GetAllPendingTickets()
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Include(x => x.IdSubscriberNavigation)
                    .Include(x => x.IdSubscriberNavigation.IdCountryNavigation)
                    .Include(x => x.IdContinentNavigation).Include(x => x.IdCompanyNavigation)
                    .Include(x => x.IdCompanyNavigation.IdCountryNavigation)
                    .Include(x => x.IdCompanyNavigation.IdCountryNavigation.IdContinentNavigation)
                    .Include(x => x.IdPersonNavigation)
                    .Include(x => x.IdPersonNavigation.IdCountryNavigation)
                    .Include(x => x.TicketQuery)
                    .Include(x => x.IdPersonNavigation.IdCountryNavigation.IdContinentNavigation)
                    .Include(x => x.IdStatusTicketNavigation)
                    .Include(x => x.IdCountryNavigation)
                    .Include(x => x.TicketHistories.OrderByDescending(x => x.Id))
                    .Include(x=>x.TicketAssignation)
                    .Include(x => x.TicketFiles)
                    .Where(x => x.IdStatusTicket == (int?)TicketStatusEnum.Pendiente && x.Enable == true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Include(x => x.IdCountryNavigation)
                    .Include(x => x.IdCompanyNavigation)
                    .Include(x => x.IdSubscriberNavigation)
                    .Include(x => x.IdCompanyNavigation.IdCountryNavigation)
                    .Include(x => x.IdSubscriberNavigation).Where(x=>x.Id==id).FirstOrDefaultAsync() ?? throw new Exception("No existe el objeto solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Ticket>> GetByNameAsync(string name)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Where(x => x.RequestedName.Contains(name)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Ticket>> GetTicketByCompany(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Include(x=>x.IdSubscriberNavigation).Where(x => x.IdCompany == id).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Ticket>> GetTicketByPerson(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Where(x => x.IdPerson == id).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<TicketQuery> GetTicketQuery(int idTicket)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.TicketQueries.Include(x => x.IdSubscriberNavigation)
                    .Include(x => x.IdTicketNavigation)
                    .Include(x => x.IdEmployeeNavigation).Where(x=>x.IdTicket==idTicket).FirstOrDefaultAsync()??throw new Exception("No se ha encontrado valores");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> TicketQueryAnswered(int idTicket)
        {
            try
            {
                using var context = new SqlCoreContext();
                var query= await context.TicketQueries.Where(x => x.IdTicket == idTicket).FirstOrDefaultAsync() ?? throw new Exception("No se ha encontrado valores");

                query.Status = (int)TicketQueryEnum.Resuelta;
                query.UpdateDate= DateTime.Now;

                context.TicketQueries.Update(query);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Ticket obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {

                    obj.UpdateDate = DateTime.Now;
                    context.Tickets.Update(obj);
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
