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
        Response<bool> AddAsync(EmailConfiguration obj);
        Response<bool> DeleteAsync(int id);
        Response<bool> UpdateAsync(EmailConfiguration obj);
        Response<List<EmailConfiguration>> GetByNameAsync(string name);
        Response<List<EmailConfiguration>> GetAllAsync();
    }
}