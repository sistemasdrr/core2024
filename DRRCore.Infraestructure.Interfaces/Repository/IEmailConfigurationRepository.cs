using DRRCore.Domain.Entities.SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Infraestructure.Interfaces.Repository
{
    public interface IEmailConfigurationRepository
    {
        Task<bool> AddAsync(EmailConfiguration obj);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(EmailConfiguration obj);
        Task<List<EmailConfiguration>> GetByNameAsync(string name);
        Task<List<EmailConfiguration>> GetAllAsync();
        Task<EmailConfiguration> GetValueByNameAsync(string name);
    }
}
