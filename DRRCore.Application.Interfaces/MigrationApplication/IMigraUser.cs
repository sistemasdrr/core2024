namespace DRRCore.Application.Interfaces.MigrationApplication
{
    public interface IMigraUser
    {
        Task MigrateUser();
        Task<bool> MigrateCompany();
        Task<bool> MigrateCompanyOthers(int migra);
        Task<bool> CorrecPersona(int migra);
        Task<bool> MigratePerson();
        Task<bool> MigratePersonByMigra(int migra);
        Task<bool> MigratePersonCorreccion();
        Task<bool> MigratePersonByOldCode(string oldCode);
        Task<bool> MigrateSubscriber();
        Task<bool> MigrateOldTicket();
        Task<bool> MigrateCountry();
        Task<bool> MigrateSubscriberCategory();
        Task<bool> MigratePersonal();
        Task<bool> MigrateAgent();


    }
}
