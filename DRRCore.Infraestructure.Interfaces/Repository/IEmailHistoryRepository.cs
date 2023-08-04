using DRRCore.Domain.Entities.SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Infraestructure.Interfaces.Repository
{
    public interface IEmailHistoryRepository
    {
        Task<bool> AddAsync(EmailHistory obj);
        Task<bool> UpdateAsync(EmailHistory obj);
        Task<bool> DeleteAsync(int id);
        Task<List<EmailHistory>> GetAllAsync();
        Task<List<EmailHistory>> GetByUserAsync(string user);
        Task<EmailHistory> GetByIdAsync(int id);
        Task<List<EmailHistory>> GetEmailNotSendAsync();
    }
}
