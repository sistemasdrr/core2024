using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ITicketDomain:IBaseDomain<Ticket>
    {
        Task<List<Ticket>> GetTicketByCompany(int id);
        Task<List<OldTicket>> GetOldTicketByCompany(string oldCode);
        Task<List<OldTicket>> GetOldTicketByPerson(string oldCode);
        Task<List<Ticket>> GetAllPendingTickets();
        Task<List<Ticket>> GetTicketByPerson(int id);
        Task<List<Ticket>> GetAllByAsync(string ticket, string name, string subscriber, string type, string procedure);
        Task<bool> AddTicketQuery(TicketQuery query);
        Task<TicketQuery> GetTicketQuery(int idTicket);
        Task<bool> TicketQueryAnswered(int idTicket);
        Task<List<OldTicket>> GetSimilarByNameAsync(string name, string empresaPersona);
        Task<List<Ticket>> GetByNameAsync(string name, string empresaPersona);

        Task<List<TicketFile>> GetFilesByIdTicket(int idTicket);
        Task<TicketFile> GetFileByPath(string path);
        Task<TicketFile> GetTicketFileById(int id);
        Task<bool?> AddTicketFile(TicketFile obj);
        Task<bool?> UpdateTicketFile(TicketFile obj);
        Task<bool?> DeleteTicketFile(int id);
    }
}
