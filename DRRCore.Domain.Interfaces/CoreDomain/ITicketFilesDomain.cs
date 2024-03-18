using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ITicketFilesDomain : IBaseDomain<TicketFile>
    {
        Task<List<TicketFile>> GetFilesByIdTicket(int idTicket);
    }
}
