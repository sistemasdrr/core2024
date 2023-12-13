using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Domain.Main.CoreDomain
{
    public class NumerationDomain : INumerationDomain
    {
        private INumerationRepository _numerationRepository;
        public NumerationDomain(INumerationRepository numerationRepository) { 
         _numerationRepository = numerationRepository;
        }
        public async Task<Numeration> GetOrderNumberAsync()
        {
            return await _numerationRepository.GetOrderNumberAsync();
        }

        public async Task<Numeration> GetTicketNumberAsync()
        {
            return await _numerationRepository.GetTicketNumberAsync();
        }

        public async Task<bool> UpdateTicketNumberAsync()
        {
            return await _numerationRepository.UpdateTicketNumberAsync();
        }
    }
}
