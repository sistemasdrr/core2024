﻿namespace DRRCore.Application.Interfaces.MigrationApplication
{
    public interface IMigraUser
    {
        Task MigrateUser();
        Task<bool> MigrateCompany();
        Task<bool> MigratePerson();
        Task<bool> MigrateSubscriber();
        Task<bool> MigrateOldTicket();
        Task<bool> MigrateCountry();

    }
}
