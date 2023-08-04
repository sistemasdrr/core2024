using DRRCore.Domain.Entities.SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Domain.Interfaces
{
    public interface IEmailHistoryDomain
    {
        Task<bool> AddAsync(EmailHistory obj);
        Task<bool> DeleteAsync(int id);       
        Task<List<EmailHistory>> GetAllAsync();
        Task<List<EmailHistory>> GetByUser(string user);
        Task<List<EmailHistory>> GetEmailNotSendAsync();
        Task<bool> UpdateAsync(EmailHistory obj);
    }
}