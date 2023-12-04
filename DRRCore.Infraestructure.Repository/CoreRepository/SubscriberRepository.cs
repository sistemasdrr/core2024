using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly ILogger _logger;
        public SubscriberRepository(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<int> AddSubscriberAsync(Subscriber subscriber)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.Subscribers.AddAsync(subscriber);
                    await context.SaveChangesAsync();
                    return subscriber.Id;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;

            }
        }

        public async Task<List<Subscriber>> GetSubscriber(string code, string name, bool enable)
        {
            try
            {
                using var context = new SqlCoreContext();
                if (enable)
                {
                    return await context.Subscribers.Where(x => x.Code.Contains(code) && x.Name.Contains(name) && x.Enable == true).ToListAsync();

                }
                else
                {
                    return await context.Subscribers.Where(x => x.Code.Contains(code) && x.Name.Contains(name)).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Subscriber>();
            }
        }

        public async Task<Subscriber> GetSubscriberById(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
               
                return await context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateSubscriberAsync(Subscriber subscriber)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    subscriber.UpdateDate = DateTime.Now;
                    context.Subscribers.Update(subscriber);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
