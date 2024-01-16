using DRRCore.Domain.Entities.SqlContext;

namespace DRRCore.Domain.Interfaces.EmailDomain
{
    public interface IAttachmentsNotSendDomain
    {
        Task<bool> AddAsync(AttachmentsNotSend obj);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(AttachmentsNotSend obj);
        Task<List<AttachmentsNotSend>> GetByEmailHistoryIdAsync(int id);
        Task<List<AttachmentsNotSend>> GetAllAsync();
    }
}
