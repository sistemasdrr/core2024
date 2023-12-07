using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.MYSQLRepository
{
    public class MEmpresaRepository : IMEmpresaRepository
    {
        public async Task<MEmpresa> GetmEmpresaByCodigoAsync(string codigo)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.MEmpresas.FirstOrDefaultAsync(x => x.EmCodigo == codigo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MEmpresa>> GetNotMigratedEmpresa()
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    return await context.MEmpresas.Where(x=>x.Migra==0).Take(10000).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> MigrateEmpresa(string code)
        {
            try
            {
                using (var context = new MySqlContext())
                {
                    var empresa =await  GetmEmpresaByCodigoAsync(code);
                    if (empresa==null)
                    {
                        throw new Exception("No se encuentra la empresa");
                    }
                    empresa.Migra = 1;
                    context.MEmpresas.Update(empresa);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

    }
}
