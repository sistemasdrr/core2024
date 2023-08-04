using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Application.Interfaces
{
    public interface IEmailHistory
    {
        Response<bool> AddAsync(EmailHistory obj);
        Response<bool> DeleteAsync(int id);
        Response<bool> UpdateAsync(EmailHistory obj);
        Response<List<EmailHistory>> GetAllAsync();
        Response<List<EmailHistory>> GetByUser(string user);
        Response<List<EmailHistory>> GetEmailNotSend();
    }
}
