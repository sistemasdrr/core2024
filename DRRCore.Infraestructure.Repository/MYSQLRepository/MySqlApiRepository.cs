using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.MYSQLRepository
{
    public class MySqlApiRepository : IMySqlApiRepository
    {
        public async Task<MEmpresa> GetEmpresaByCodigo(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    var empresa = context.MEmpresas.Where(x => x.EmCodigo == codigo).FirstOrDefault();
                    return empresa;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<REmpVsAspLeg> GetRempByCodigo(string codigo)
        {
            throw new NotImplementedException();
        }

        public Task<REmpVsInfFin> GetREmpVsInfByCodigo(string codigo)
        {
            throw new NotImplementedException();
        }

        public Task<REmpVsRamNeg> GetREmpVsRamNegByCodigo(string codigo)
        {
            throw new NotImplementedException();
        }
    }
}
