using DRRCore.Application.DTO.Web;
using DRRCore.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Application.Interfaces
{
    public interface IWebDataApplication
    {   
        Task<Response<bool>> AddOrUpdateWebDataAsync();
        Task<Response<List<WebDataDto>>> GetByParamAsync(string param,int page);
        Task<Response<WebDataDto>> GetByCodeAsync(string code);

    }
}
