using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface ITicketRepository:IBaseRepository<Ticket>
    {
        Task<List<Ticket>> GetTicketByCompany(int id);
        Task<List<Ticket>> GetAllPendingTickets();

        Task<List<Ticket>> GetTicketByPerson(int id);
        Task<List<Ticket>> GetAllByAsync(string ticket, string name, string subscriber, string type, string procedure);
    }
}
