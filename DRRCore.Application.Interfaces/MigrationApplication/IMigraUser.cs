namespace DRRCore.Application.Interfaces.MigrationApplication
{
    public interface IMigraUser
    {
        Task MigrateUser();
        Task<bool> MigrateCompany();
        Task<bool> MigrateCompanyOthers(int migra);
        
        Task<bool> MigratePerson();
        Task<bool> MigratePersonCorreccion();
        Task<bool> MigratePersonByOldCode(string oldCode);
        Task<bool> MigrateSubscriber();
        Task<bool> MigrateOldTicket();
        Task<bool> MigrateCountry();
        Task<bool> MigrateSubscriberCategory();
        

    }
}
