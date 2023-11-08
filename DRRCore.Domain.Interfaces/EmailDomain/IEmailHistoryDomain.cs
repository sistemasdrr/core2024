using DRRCore.Domain.Entities.SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Domain.Interfaces.EmailDomain
{
    public interface IEmailHistoryDomain
    {
        Task<bool> AddAsync(EmailHistory obj);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(EmailHistory obj);
        Task<List<EmailHistory>> GetAllAsync();
        Task<List<EmailHistory>> GetByUserAsync(string user);
        Task<EmailHistory> GetByIdAsync(int id);
        Task<List<EmailHistory>> GetEmailNotSendAsync();
    }
}