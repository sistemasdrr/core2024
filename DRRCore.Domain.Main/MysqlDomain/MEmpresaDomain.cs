using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Interfaces.MysqlDomain;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;

namespace DRRCore.Domain.Main.MysqlDomain
{
    public class MEmpresaDomain : IMEmpresaDomain
    {
        private readonly IMEmpresaRepository _repository;

        public MEmpresaDomain(IMEmpresaRepository repository)
        {
            _repository = repository;
        }
        public async Task<MEmpresa> GetmEmpresaByCodigoAsync(string codigo)
        {
            return await _repository.GetmEmpresaByCodigoAsync(codigo);
        }

        public async Task<List<MEmpresa>> GetNotMigratedEmpresa()
        {
            return await _repository.GetNotMigratedEmpresa();
        }

        public async Task<bool> MigrateEmpresa(string code)
        {
            return await _repository.MigrateEmpresa(code);
        }
    }
}
