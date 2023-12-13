using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces
{
    public interface INumerationDomain
    {
        Task<Numeration> GetTicketNumberAsync();
        Task<bool> UpdateTicketNumberAsync();
        Task<Numeration> GetOrderNumberAsync();
    }
}
