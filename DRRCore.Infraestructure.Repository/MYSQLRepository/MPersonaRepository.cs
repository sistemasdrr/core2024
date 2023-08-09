using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.MYSQLRepository
{
    public class MPersonaRepository : IMPersonaRepository
    {
        public async Task<List<MPersona>> GetAllMPersonaAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.MPersonas.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MPersona> GetMPersonaByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.MPersonas.FirstOrDefaultAsync(x => x.PeCodigo == codigo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
