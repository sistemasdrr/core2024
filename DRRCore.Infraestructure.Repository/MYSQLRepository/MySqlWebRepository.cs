using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.MYSQLRepository
{
    public class MySqlWebRepository : IMySqlWebRepository
    {
        public async Task<int> Count()
        {
            using var mysqlContext = new MySqlContext();
            return await mysqlContext.ViewConsultaWebs.CountAsync();
        }

        public async Task<List<ViewConsultaWeb>> Get(int i)
        {           
            using var mysqlContext = new MySqlContext();     
            return await mysqlContext.ViewConsultaWebs.Skip(i * 1000).Take(1000).ToListAsync();           
        }
    }
}


