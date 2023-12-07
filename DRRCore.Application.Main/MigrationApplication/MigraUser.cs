using DRRCore.Application.Interfaces.MigrationApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Domain.Interfaces.MysqlDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Application.Main.MigrationApplication
{
    public class MigraUser : IMigraUser
    {
       
        private readonly IMEmpresaDomain _mempresaDomain;
        private readonly ICompanyDomain _companyDomain;
        private readonly ILogger _logger;
        public MigraUser( IMEmpresaDomain mempresaDomain, ILogger logger,ICompanyDomain companyDomain)
        {
          
            _mempresaDomain = mempresaDomain;
            _logger = logger;
            _companyDomain = companyDomain;
           
    }

        public async Task<bool> MigrateCompany()
        {
            var empresas =await _mempresaDomain.GetNotMigratedEmpresa();
            foreach (var empresa in empresas)
            {
                try
                {
                   var inserted= await _companyDomain.AddCompanyAsync(new Company
                    {
                        OldCode=empresa.EmCodigo,
                        Name=empresa.EmNombre,
                        SocialName=empresa.EmSiglas,
                        Language = Dictionary.LanguageMigra[empresa.IdiCodigo.Value],
                        Address=empresa.EmDirecc,
                        Cellphone=empresa.EmPrffax,
                        YearFundation=empresa.EmAnofun,
                        TaxTypeCode=empresa.EmRegtri,
                        NewsComentary=string.IsNullOrEmpty(empresa.EmPrensa)?empresa.EmPrensasel:empresa.EmPrensa,
                        Quality=empresa.EmRating?.Trim()=="NN"?null:empresa.EmRating?.Trim(),
                        Email=empresa.EmEmail,
                        IdentificacionCommentary=empresa.EmComide,
                        WebPage=empresa.EmPagweb,
                        Enable=empresa.EmActivo==1,
                        Place=empresa.EmCiudad,
                        LastSearched=empresa.EmFecinf,
                        LastUpdaterUser=1,
                        OnWeb=true,
                        PostalCode=empresa.EmCodpos,
                        WhatsappPhone=empresa.EmFax,
                        Telephone=empresa.EmTelef1,
                        SubTelephone=empresa.EmPrftlf,
                        TypeRegister=empresa.EmTipper==0?"PJ":"PN",
                        ReputationComentary=empresa.EmComrep    
                    });
                    if (inserted != 0)
                    {
                        await _mempresaDomain.MigrateEmpresa(empresa.EmCodigo);
                    }
                    else{
                        _logger.LogError("Error empresa :" + empresa.EmCodigo);
                    }
                    
                   

                }catch (Exception ex)
                {
                    _logger.LogError("Error empresa :"+empresa.EmCodigo);
                    continue;
                }
            }
            return true;
        }

        public Task MigrateUser()
        {
          throw new NotImplementedException();
        }
    }
}
