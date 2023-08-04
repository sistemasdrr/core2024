using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IAttachmentsNotSendDomain
    {
        Task<bool> AddAsync(AttachmentsNotSend obj);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(AttachmentsNotSend obj);
        Task<AttachmentsNotSend> GetByIdAsync(int id);
        Task<List<AttachmentsNotSend>> GetAllAsync();
    }
}
