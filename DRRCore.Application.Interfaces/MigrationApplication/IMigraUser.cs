namespace DRRCore.Application.Interfaces.MigrationApplication
{
    public interface IMigraUser
    {
        Task MigrateUser();
        Task<bool> MigrateCompany();
        Task<bool> MigratePerson();
        Task<bool> MigrateSubscriber();
        Task<bool> MigrateOldTicket();
        Task<object> MigrateCountry();
        Task<bool> MigrateCompanyOthers(int migra);
        Task<bool> ModificarCompanyOthers(int migra, int nivel);
    }
}
