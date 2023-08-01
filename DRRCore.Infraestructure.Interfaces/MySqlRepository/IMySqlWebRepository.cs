using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Infraestructure.Interfaces.MySqlRepository
{
    public interface IMySqlWebRepository
    {
        Task<List<ViewConsultaWeb>> Get();       

    }
}
