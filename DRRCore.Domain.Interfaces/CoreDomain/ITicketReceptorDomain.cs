﻿using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ITicketReceptorDomain:IBaseDomain<TicketReceptor>
    {
        Task<TicketReceptor> GetReceptorDoubleDate();
        Task<TicketReceptor> GetReceptorInDate(int idCountry);
        Task<TicketReceptor> GetReceptorOtherCase(int idCountry);
    }
}
