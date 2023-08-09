using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.MYSQLRepository
{
    public class TCuponRepository : ITCuponRepository
    {
        public async Task<List<TCupon>> GetAllTCuponAsync()
        {
            try
            {
                using(var context = new MySqlContext())
                {
                    return await context.TCupons.ToListAsync();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TCupon> GetTCuponByCodigoAsync(int codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TCupons.FirstOrDefaultAsync(x => x.CupCodigo == codigo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
