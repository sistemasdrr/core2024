﻿using DRRCore.Application.DTO.Enum;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ILogger _logger;
        public TicketRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(Ticket obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    await context.Tickets.AddAsync(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var obj = await context.Tickets.FindAsync(id) ?? throw new Exception("No existe el objeto solicitado");
                    obj.Enable = false;
                    obj.DeleteDate = DateTime.Now;
                    context.Tickets.Update(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Include(x=>x.IdSubscriberNavigation).Include(x => x.IdSubscriberNavigation.IdCountryNavigation)
                    .Include(x => x.IdContinentNavigation).Include(x => x.IdCompanyNavigation).Include(x => x.IdCompanyNavigation.IdCountryNavigation).Include(x => x.IdCompanyNavigation.IdCountryNavigation.IdContinentNavigation)
                    .Include(x => x.IdCountryNavigation).Where(x => x.Status!=(int?)TicketStatusEnum.Despachado && x.Enable == true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Ticket>();
            }
        }

        public async Task<List<Ticket>> GetAllPendingTickets()
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Include(x=>x.TicketAssignation).Include(x => x.TicketFiles).Where(x => x.Status == (int?)TicketStatusEnum.Pendiente && x.Enable == true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Ticket>();
            }
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.FindAsync(id) ?? throw new Exception("No existe el objeto solicitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public Task<List<Ticket>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Ticket>> GetTicketByCompany(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Include(x=>x.IdSubscriberNavigation).Where(x => x.IdCompany == id).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Ticket>();
            }
        }

        public async Task<List<Ticket>> GetTicketByPerson(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Tickets.Where(x => x.IdPerson == id).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Ticket>();
            }
        }

      

        public async Task<bool> UpdateAsync(Ticket obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {

                    obj.UpdateDate = DateTime.Now;
                    context.Tickets.Update(obj);
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
