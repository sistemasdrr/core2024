using DRRCore.Application.Interfaces.MigrationApplication;
using DRRCore.Domain.Interfaces;

namespace DRRCore.Application.Main.MigrationApplication
{
    public class MigraUser : IMigraUser
    {
        private readonly IMPersonaDomain _personaDomain;
        public MigraUser(IMPersonaDomain personaDomain)
        {
            _personaDomain = personaDomain;
        }
        public Task MigrateUser()
        {
          throw new NotImplementedException();
        }
    }
}
