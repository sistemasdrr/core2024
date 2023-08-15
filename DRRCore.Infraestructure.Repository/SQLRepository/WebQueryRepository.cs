using AutoMapper;
using DRRCore.Application.DTO.Web;
using DRRCore.Domain.Entities;
using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Infraestructure.Interfaces.Repository;
using DRRCore.Transversal.Common;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System.Drawing.Printing;

namespace DRRCore.Infraestructure.Repository.SQLRepository
{
    public class WebQueryRepository : IWebQueryRepository
    {
        private IMapper Mapper { get; }
        public WebQueryRepository(IMapper mapper)
        {
            Mapper = mapper;
        }
        public async Task<bool> UpdateData(List<ViewConsultaWeb> listWebData)
        {
            int i = 0;
            using (var context = new SqlContext())
            {
                foreach (var webData in listWebData)
            {
                    i++;
                    var data = await context.WebQueries.Where(x => x.CodigoEmpresa == webData.CodigoEmpresa).FirstOrDefaultAsync();
                    if (data != null)
                    {                        
                       context.WebQueries.Update(Mapper.Map(webData,data));
                    }
                    else
                    {
                        await context.WebQueries.AddAsync(Mapper.Map<WebQuery>(webData));
                    }
                }
                await context.SaveChangesAsync();
            }
            
            return false;
        }
        public async Task<List<WebQuery>> GetByParamAsync(string param, int page)
        {
            var skip = (page - 1) * 10;
            using (var context = new SqlContext())
            {
                var list = await context.WebQueries.Where(x => x.NombreEmpresa.Contains(param))
                                                   .OrderBy(x=>x.NombreEmpresa)
                                                   .Skip(skip)
                                                   .Take(10)
                                                   .ToListAsync();
                return list;                
            }
        }
        public async Task<List<WebQuery>> GetByCountryAndBranchAsync(int country, string branch,int page)
        {
            var skip = (page - 1) * 10;
            using (var context = new SqlContext())
            {
                var list = await context.WebQueries.Where(x => x.PaisCodigo== country.ToString("D3") && x.RamoCodigo==branch)
                                                   .OrderBy(x => x.NombreEmpresa)
                                                   .Skip(skip)
                                                   .Take(10)
                                                   .ToListAsync();
                return list;
            }
        }
        public async Task<List<WebQuery>> GetSimilarBrunchAsync(string code)
        {           
            using (var context = new SqlContext())
            {
                var obj= await context.WebQueries.Where(x => x.CodigoEmpresaWeb == code).FirstOrDefaultAsync();

                var list = await context.WebQueries.Where(x => x.Sector == obj.Sector && x.RamoCodigo == obj.RamoCodigo)
                                                   .OrderBy(x => x.NombreEmpresa)                                                  
                                                   .Take(10)
                                                   .ToListAsync();
                return list;
            }
        }


        public async Task<WebQuery> GetByCodeAsync(string code)
        {
            using var context = new SqlContext();
            return await context.WebQueries.Where(x => x.CodigoEmpresaWeb == code).FirstOrDefaultAsync();
        }
    }
}
