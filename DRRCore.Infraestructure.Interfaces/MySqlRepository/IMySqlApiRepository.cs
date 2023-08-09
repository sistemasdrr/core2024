using DRRCore.Domain.Entities.MYSQLContext;
namespace DRRCore.Infraestructure.Interfaces.MySqlRepository

{
    public interface IMySqlApiRepository
    {
        Task<MEmpresa> GetEmpresaByCodigo(string codigo);
        Task<REmpVsAspLeg> GetRempByCodigo(string codigo);
        Task<REmpVsRamNeg> GetREmpVsRamNegByCodigo(string codigo); //Persona Juridica Empresa
        Task<REmpVsInfFin> GetREmpVsInfByCodigo(string codigo);

    }
}
