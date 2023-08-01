using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Infraestructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.SQLRepository
{
    public class EmailRepository : IEmailRepository
    {
        public async Task<EmailConfiguration> GetByKey(string key)
        {
            try
            {
                using (var context = new SqlContext())
                {
                    var email = await context.EmailConfigurations.Where(x => x.Name == key).FirstOrDefaultAsync();
                    if (email == null)
                    {
                        throw new Exception("No existe configuración para ese tipo de correo");
                    }
                    return email;
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddEmailHistory(EmailHistory emailHistory)
        {
            try
            {
                using (var context = new SqlContext())
                {
                    var email = await context.EmailHistories.AddAsync(emailHistory);
                    if (email.State != EntityState.Added)
                    {                       
                        throw new Exception("No se pudo insertar el objeto");
                    }
                    return emailHistory.Id;
                }
            }
            catch (Exception ex)
            {               
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddEmailConfiguration(EmailConfiguration emailConfiguration)
        {
            try
            {
                using (var context = new SqlContext())
                {
                    var email = await context.EmailConfigurations.AddAsync(emailConfiguration);
                    if (email.State != EntityState.Added)
                    {
                        throw new Exception("No se pudo insertar el objeto");
                    }
                    return emailConfiguration.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
    }
}
