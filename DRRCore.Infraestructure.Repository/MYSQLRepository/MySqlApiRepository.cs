using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.MYSQLRepository
{
    public class MySqlApiRepository : IMySqlApiRepository
    {
        public Task<REmpVsRep> DeleteRepComercialEmpresaAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    var obj = await context.REmpVsReps.FindAsync(codigo);
                    if (obj != null)
                    {
                        context.REmpVsReps.Remove(obj);
                        await context.SaveChangesAsync();
        }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TClasif>> GetAllClasifAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TClasifs.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public async Task<List<TCalidad>> GetAllTCalidadAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TCalidads.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TTemporalPersona>> GetAllTemporalPersonaAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TTemporalPersonas.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TJuridi>> GetAllTJuridiAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TJuridis.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TPago>> GetAllTPagoAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TPagos.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TPai>> GetAllTPaisAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TPais.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TRepCom>> GetAllTRepComAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TRepComs.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TSituacion>> GetAllTSituacionAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TSituacions.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TTipoTramite>> GetAllTTipoTramiteAsync()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.TTipoTramites.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TClasif> GetClasifByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.TClasifs.Where(x => x.CrCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<MEmpresa> GetmEmpresaByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.MEmpresas.Where(x => x.EmCodigo == codigo).FirstOrDefault();
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
                    return context.MPersonas.Where(x => x.PeCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RCupVsAbo> GetRCupVsAboByCodigoAsync(int codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.RCupVsAbos.Where(x => x.EcCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<REmpVsAspLeg> GetRempByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.REmpVsAspLegs.Where(x => x.EmCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<REmpVsInfFin> GetREmpVsInfByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.REmpVsInfFins.Where(x => x.EmCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<REmpVsRamNeg> GetREmpVsRamNegByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.REmpVsRamNegs.Where(x => x.EmCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RPerVsEr> GetRPerVsErByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.RPerVsErs.Where(x => x.PeCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TCalidad> GetTCalidadByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.TCalidads.Where(x => x.CalCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TContinente> GetTContinenteByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.TContinentes.Where(x => x.ConCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
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
                    return context.TCupons.Where(x => x.CupCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TTemporalPersona> GetTemporalPersonaByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.TTemporalPersonas.Where(x => x.PeCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<TJuridi> GetTJuridiByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.TJuridis.Where(x => x.JuCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TPago> GetTPagoByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return context.TPagos.Where(x => x.PaCodigo == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TPai> GetTPaiByCodigoAsync(string codigo)
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
