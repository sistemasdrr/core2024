﻿using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface ISubscriberRepository
    {
        Task <Subscriber> GetSubscriberById(int id);
        Task<int> AddSubscriberAsync(Subscriber subscriber);
        Task<Boolean> UpdateSubscriberAsync(Subscriber subscriber);
        Task<List<Subscriber>> GetSubscriber(string code, string name, string enable);
        Task<bool> DeleteSubscriberAsync(int id);
        Task<bool> ActiveSubscriberAsync(int id);
    }
}
