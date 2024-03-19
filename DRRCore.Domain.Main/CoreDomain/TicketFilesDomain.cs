using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Infraestructure.Interfaces.CoreRepository;

namespace DRRCore.Domain.Main.CoreDomain
{
    public class TicketFilesDomain : ITicketFilesDomain
    {
        //private readonly ITicketFilesRepository _ticketFilesRepository;
        //public TicketFilesDomain(ITicketFilesRepository ticketFilesRepository)
        //{
        //    _ticketFilesRepository = ticketFilesRepository;
        //}
        //public async Task<bool> AddAsync(TicketFile obj)
        //{
        //    return await _ticketFilesRepository.AddAsync(obj);
        //}

        //public async Task<bool> DeleteAsync(int id)
        //{
        //    return await _ticketFilesRepository.DeleteAsync(id);
        //}

        //public Task<List<TicketFile>> GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<TicketFile> GetByIdAsync(int id)
        //{
        //    return await _ticketFilesRepository.GetByIdAsync(id);
        //}

        public Task<List<TicketFile>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TicketFile>> GetFilesByIdTicket(int idTicket)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TicketFile obj)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBaseDomain<TicketFile>.AddAsync(TicketFile obj)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBaseDomain<TicketFile>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<TicketFile>> IBaseDomain<TicketFile>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<TicketFile> IBaseDomain<TicketFile>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<TicketFile>> IBaseDomain<TicketFile>.GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        Task<List<TicketFile>> ITicketFilesDomain.GetFilesByIdTicket(int idTicket)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBaseDomain<TicketFile>.UpdateAsync(TicketFile obj)
        {
            throw new NotImplementedException();
        }
    }
}
