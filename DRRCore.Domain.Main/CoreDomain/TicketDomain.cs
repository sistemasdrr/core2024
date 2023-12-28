using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Infraestructure.Interfaces.CoreRepository;

namespace DRRCore.Domain.Main.CoreDomain
{
    public class TicketDomain : ITicketDomain
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketDomain(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<bool> AddAsync(Ticket obj)
        {
            return await _ticketRepository.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int id)
        {
           return await _ticketRepository.DeleteAsync(id);
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
           return await _ticketRepository.GetAllAsync();
        }

        public async Task<List<Ticket>> GetAllPendingTickets()
        {
            return await _ticketRepository.GetAllPendingTickets();
           
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
          return await _ticketRepository.GetByIdAsync(id);
        }

        public async Task<List<Ticket>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Ticket>> GetTicketByCompany(int id)
        {
            return await _ticketRepository.GetTicketByCompany(id);
        }

        public async Task<List<Ticket>> GetTicketByPerson(int id)
        {
           return await _ticketRepository.GetTicketByPerson(id);
        }

        public async Task<bool> UpdateAsync(Ticket obj)
        {
            return await _ticketRepository.UpdateAsync(obj);
        }
    }
}
