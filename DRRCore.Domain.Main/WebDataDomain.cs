using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using DRRCore.Infraestructure.Interfaces.Repository;

namespace DRRCore.Domain.Main
{
    public class WebDataDomain : IWebDataDomain
    {
        private readonly IMySqlWebRepository _mySqlWebRepository;
        private readonly IWebQueryRepository _webQueryRepository;
        public WebDataDomain(IMySqlWebRepository mySqlWebRepository, IWebQueryRepository webQueryRepository)
        {
            _mySqlWebRepository = mySqlWebRepository;
            _webQueryRepository = webQueryRepository;
        }
        public async Task<bool> AddOrUpdateWebDataAsync()
        {
            var count = await _mySqlWebRepository.Count();
            var limit = Math.Ceiling(Convert.ToDecimal(count / 1000));
            for (int i = 0; i <=  limit ; i++)
            {
                var listadoGeneral = await _mySqlWebRepository.Get(i);
                if (listadoGeneral.Count > 0)
                {
                    await _webQueryRepository.UpdateData(listadoGeneral);
                }
            }
             
            return true;
        }

        public async Task<WebQuery> GetByCodeAsync(string code)
        {
            return await _webQueryRepository.GetByCodeAsync(code);
        }

        public async Task<List<WebQuery>> GetByParamAsync(string param, int page)
        {
            return await _webQueryRepository.GetByParamAsync(param,page);           
        }
       
    }
}
