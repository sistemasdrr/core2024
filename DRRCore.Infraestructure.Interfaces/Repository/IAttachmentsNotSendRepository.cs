using DRRCore.Domain.Entities.SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Infraestructure.Interfaces.Repository
{
    public interface IAttachmentsNotSendRepository
    {
        Task<bool> AddAsync(AttachmentsNotSend obj);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(AttachmentsNotSend obj);
        Task<AttachmentsNotSend> GetByIdAsync(int id);
        Task<List<AttachmentsNotSend>> GetAllAsync();
    }
}
