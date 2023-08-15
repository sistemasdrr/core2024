using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Application.Interfaces
{
    public interface IEmailConfiguration
    {
        Task<Response<bool>> AddAsync(EmailConfiguration obj);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<bool>> UpdateAsync(EmailConfiguration obj);
        Task<Response<List<EmailConfiguration>>> GetByNameAsync(string name);
        Task<Response<List<EmailConfiguration>>> GetAllAsync();
    }
}