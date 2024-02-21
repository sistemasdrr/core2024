using DRRCore.Application.Interfaces.MigrationApplication;
using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Domain.Interfaces.MysqlDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DRRCore.Application.Main.MigrationApplication
{
    public class MigraUser : IMigraUser
    {
       
        private readonly IMEmpresaDomain _mempresaDomain;
        private readonly IMPersonaDomain _impersonaDomain;
        private readonly ICompanyDomain _companyDomain;
        private readonly ICompanyBackgroundDomain _companyBackgroundDomain;
        private readonly ICompanyBranchDomain _companyBranchDomain;
        private readonly ICompanyFinancialInformationDomain _companyFinancialInformationDomain;
        private readonly ICompanySBSDomain _companySBSDomain;
        private readonly ICompanyCreditOpinionDomain _companyCreditOpinionDomain;
        private readonly ICompanyGeneralInformationDomain _companyGeneralInformationDomain;
        private readonly IFinancialBalanceDomain _financialBalanceDomain;
        private readonly IFinancialSalesHistoryDomain _financialSalesHistoryDomain;
        private readonly IImportsAndExportsDomain _importsAndExportsDomain;
        private readonly IProviderDomain _providerDomain;
        private readonly IComercialLatePaymentDomain _comercialLatePaymentDomain;
        private readonly IBankDebtDomain _bankDebtDomain;

        private readonly IPersonDomain _personDomain;
        private readonly IPersonHomeDomain _personHomeDomain;
        private readonly IPersonJobDomain _personJobDomain;
        private readonly IPersonActivitiesDomain _personActivitiesDomain;
        private readonly IPersonPropertyDomain _personPropertyDomain;
        private readonly IPersonSBSDomain _personSBSDomain;
        private readonly IPersonHistoryDomain _personHistoryDomain;
        private readonly IPersonGeneralInfoDomain _personGeneralInfoDomain;

        private readonly ILogger _logger;
        public MigraUser( IMEmpresaDomain mempresaDomain, ILogger logger,ICompanyDomain companyDomain, 
            ICompanyBackgroundDomain companyBackgroundDomain, ICompanyBranchDomain companyBranchDomain,
            ICompanyFinancialInformationDomain companyFinancialInformationDomain, ICompanySBSDomain companySBSDomain,
            ICompanyCreditOpinionDomain companyCreditOpinionDomain, ICompanyGeneralInformationDomain companyGeneralInformationDomain,
            IFinancialBalanceDomain financialBalanceDomain, IImportsAndExportsDomain importsAndExportsDomain,
            IProviderDomain providerDomain, IComercialLatePaymentDomain comercialLatePaymentDomain,
            IBankDebtDomain bankDebtDomain
            , IMPersonaDomain mPersonaDomain, IPersonDomain personDomain, IPersonHomeDomain personHomeDomain, 
            IPersonJobDomain personJobDomain, IPersonActivitiesDomain personActivitiesDomain, IPersonPropertyDomain personPropertyDomain,
            IPersonSBSDomain personSBSDomain, IPersonHistoryDomain personHistoryDomain, IPersonGeneralInfoDomain personGeneralInfoDomain,
            IFinancialSalesHistoryDomain financialSalesHistoryDomain
            )
        {
          
            _mempresaDomain = mempresaDomain;
            _logger = logger;
            _companyDomain = companyDomain;
            _companyBackgroundDomain = companyBackgroundDomain;
            _companyBranchDomain = companyBranchDomain;
            _companyFinancialInformationDomain = companyFinancialInformationDomain;
            _companySBSDomain = companySBSDomain;
            _companyCreditOpinionDomain = companyCreditOpinionDomain;
            _companyGeneralInformationDomain = companyGeneralInformationDomain;
            _financialBalanceDomain = financialBalanceDomain;
            _importsAndExportsDomain = importsAndExportsDomain;
            _providerDomain = providerDomain;
            _comercialLatePaymentDomain = comercialLatePaymentDomain;
            _bankDebtDomain = bankDebtDomain;
            _impersonaDomain = mPersonaDomain;
            _personDomain = personDomain;
            _personHomeDomain = personHomeDomain;
            _personJobDomain = personJobDomain;
            _personActivitiesDomain = personActivitiesDomain;
            _personPropertyDomain = personPropertyDomain;
            _personHistoryDomain = personHistoryDomain;
            _personGeneralInfoDomain = personGeneralInfoDomain;
            _financialSalesHistoryDomain = financialSalesHistoryDomain;
            _personSBSDomain = personSBSDomain;
    }

        public async Task<bool> MigrateCompany()
        {
            for(int i = 0; i < 360; i++)
            {
                var empresas = await _mempresaDomain.GetNotMigratedEmpresa();
                foreach (var empresa in empresas)
                {
                    var reputacion = await _mempresaDomain.GetmEmpresaReputacionByCodigoAsync(empresa.EmCodigo);
                    int idReputacion = 0;
                    if (reputacion != null)
                    {
                        idReputacion = ObtenerReputacion(reputacion.RcCodigo);                       
                    }
                    try
                    {
                        var inserted = await _companyDomain.AddCompanyAsync(new Company
                        {
                            OldCode = empresa.EmCodigo,
                            Name = empresa.EmNombre,
                            SocialName = empresa.EmSiglas,
                            LastSearched = empresa.EmFecinf,
                            Language = Dictionary.LanguageMigra[empresa.IdiCodigo.Value],
                            TypeRegister = empresa.EmTipper == 0 ? "PJ" : "PN",
                            YearFundation = empresa.EmAnofun,
                            Quality = ObtenerCalidad(empresa.CalCodigo),                           
                            IdLegalPersonType = ObtenerPersoneriaLegal(empresa.JuCodigo),
                            TaxTypeCode = empresa.EmRegtri,
                            IdCountry = ObtenerCodigoPais(empresa.PaiCodigo),    
                            IdLegalRegisterSituation = empresa.SitCodigo == "03" ? 4 : empresa.SitCodigo == "04" ? 3 :
                            empresa.SitCodigo == "05" ? 16 : empresa.SitCodigo == "02" ? 1 : empresa.SitCodigo == "07" ? 2 :
                            empresa.SitCodigo == "06" ? 13 : empresa.SitCodigo == "08" ? 5 : empresa.SitCodigo == "09" ? 14 :
                            empresa.SitCodigo == "10" ? 6 : empresa.SitCodigo == "11" ? 8 : empresa.SitCodigo == "12" ? 11 :
                            empresa.SitCodigo == "13" ? 10 : empresa.SitCodigo == "14" ? 7 : empresa.SitCodigo == "15" ? 12 :
                            empresa.SitCodigo == "16" ? 15 : empresa.SitCodigo == "17" ? 9 : null,

                            Address = empresa.EmDirecc,
                            Place = empresa.EmCiudad,

                            Telephone = empresa.EmTelef1,
                            SubTelephone = empresa.EmPrftlf,
                            Cellphone = empresa.EmPrffax,
                            PostalCode = empresa.EmCodpos,
                            WhatsappPhone = empresa.EmFax,
                            Email = empresa.EmEmail,
                            WebPage = empresa.EmPagweb,
                            IdCreditRisk = empresa.CrCodigo == "0005" ? 1 : empresa.CrCodigo == "0000" ? 2 : empresa.CrCodigo == "0001" ? 3 :
                            empresa.CrCodigo == "0002" ? 4 : empresa.CrCodigo == "0003" ? 5 : empresa.CrCodigo == "0011" ? 6 : empresa.CrCodigo == "0004" ? 7 : null,
                            IdPaymentPolicy = empresa.PaCodigo == "01" ? 8 : empresa.PaCodigo == "02" ? 9 : empresa.PaCodigo == "03" ? 10 : empresa.PaCodigo == "04" ? 11 :
                            empresa.PaCodigo == "05" ? 12 : empresa.PaCodigo == "06" ? 13 : empresa.PaCodigo == "07" ? 14 : null,
                            IdReputation = idReputacion != 0 ? idReputacion : null,
                            NewsComentary = string.IsNullOrEmpty(empresa.EmPrensa) ? empresa.EmPrensasel : empresa.EmPrensa,
                            IdentificacionCommentary = empresa.EmComide,
                            Enable = empresa.EmActivo == 1,
                            LastUpdaterUser = 1,
                            OnWeb = empresa.EmOnline == "SI" ? true : false,
                            ReputationComentary = empresa.EmComrep,
                            Print = empresa.EmLogpre == 1 ? true : false
                        });
                        if (inserted != 0)
                        {

                            await _mempresaDomain.MigrateEmpresa(empresa.EmCodigo);
                            using var context = new SqlCoreContext();
                            var listTraductionCompany = new List<Traduction>();
                            listTraductionCompany.AddRange(await context.Traductions.Where(x => x.IdCompany == inserted && x.Identifier.Contains("_E_")).ToListAsync());
                            foreach (var item in listTraductionCompany)
                            {
                                if (item.Identifier == "L_E_COMIDE")
                                {
                                    if (empresa.EmComideIng != null)
                                    {
                                        item.LargeValue = empresa.EmComideIng;
                                    }
                                }
                                else if (item.Identifier == "L_E_REPUTATION")
                                {
                                    if (empresa.EmComrepIng != null)
                                    {
                                        item.LargeValue = empresa.EmComrepIng;
                                    }
                                }
                                else if (item.Identifier == "L_E_NEW")
                                {
                                    if (empresa.EmPrensaIng != null || empresa.EmPrensaselIng != null)
                                    {
                                        item.LargeValue = string.IsNullOrEmpty(empresa.EmPrensaIng) ? empresa.EmPrensaselIng : empresa.EmPrensaIng;
                                    }
                                }
                            }
                            var emp = await _companyDomain.GetByIdAsync(inserted);
                            emp.Traductions = listTraductionCompany;
                            var ins = await _companyDomain.UpdateAsync(emp);
                            if (ins == true)
                            {

                            }

                            //antecedentes
                            var antecedentes = await _mempresaDomain.GetmEmpresaAspLegByCodigoAsync(empresa.EmCodigo);


                            if (antecedentes != null)
                            {
                                var listTraductionBack = new List<Traduction>();
                                listTraductionBack.AddRange(await context.Traductions.Where(x => x.IdCompany == inserted && x.Identifier.Contains("_B_")).ToListAsync());

                                foreach (var item in listTraductionBack)
                                {
                                    if (item.Identifier == "S_B_DURATION")
                                    {
                                        if (antecedentes.EmDuraciIng != null)
                                        {
                                            item.ShortValue = antecedentes.EmDuraciIng;
                                        }
                                    }
                                    else if (item.Identifier == "S_B_REGISTERIN")
                                    {
                                        if (antecedentes.EmRegenIng != null)
                                        {
                                            item.ShortValue = antecedentes.EmRegenIng;
                                        }
                                    }
                                    else if (item.Identifier == "S_B_PUBLICREGIS")
                                    {
                                        if (antecedentes.EmRegistIng != null)
                                        {
                                            item.ShortValue = antecedentes.EmRegistIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_B_PAIDCAPITAL")
                                    {
                                        if (antecedentes.EmDuraciIng != null)
                                        {
                                            item.LargeValue = "";
                                        }
                                    }
                                    else if (item.Identifier == "S_B_INCREASEDATE")
                                    {
                                        if (antecedentes.EmFecaumIng != null)
                                        {
                                            item.ShortValue = antecedentes.EmFecaumIng;
                                        }
                                    }
                                    else if (item.Identifier == "S_B_TAXRATE")
                                    {
                                        if (antecedentes.EmTipcamIng != null)
                                        {
                                            item.ShortValue = antecedentes.EmTipcamIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_B_LEGALBACK")
                                    {
                                        if (antecedentes.EmComentIng != null)
                                        {
                                            item.LargeValue = antecedentes.EmComentIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_B_HISTORY")
                                    {
                                        if (antecedentes.EmAnteceIng != null)
                                        {
                                            item.LargeValue = antecedentes.EmAnteceIng;
                                        }
                                    }
                                }
                                try
                                {
                                    var insertBack = await _companyBackgroundDomain.AddAsync(new CompanyBackground
                                    {
                                        Id = 0,
                                        IdCompany = inserted,
                                        ConstitutionDate = StaticFunctions.VerifyDate(antecedentes.EmFecest),
                                        StartFunctionYear = antecedentes.EmIniope,
                                        OperationDuration = antecedentes.EmDuraci,
                                        RegisterPlace = antecedentes.EmRegen,
                                        NotaryRegister = antecedentes.EmNotari,
                                        PublicRegister = antecedentes.EmRegist,
                                        CurrentPaidCapitalComentary = antecedentes.EmCapac1,
                                        Origin = antecedentes.EmOrigen,
                                        IncreaceDateCapital = antecedentes.EmFecaum,
                                        Traded = antecedentes.EmCotbol == "SI" ? "Si" : antecedentes.EmCotbol == "Si" ? "Si" : antecedentes.EmCotbol == "SÍ" ? "Si" :
                                   antecedentes.EmCotbol == "Sí" ? "Si" : antecedentes.EmCotbol == "NO" ? "No" : antecedentes.EmCotbol == "No" ? "No" : "",
                                        TradedBy = antecedentes.EmTipfecaum,
                                        TradedByEng = antecedentes.EmTipfecaumIng,
                                        CurrentExchangeRate = antecedentes.EmTipcam,
                                        LastQueryRrpp = StaticFunctions.VerifyDate(antecedentes.EmRrppFecha),
                                        LastQueryRrppBy = antecedentes.EmRrppPor,
                                        Background = antecedentes.EmComent,
                                        History = antecedentes.EmAntece

                                    }, listTraductionBack);
                                }
                                catch(Exception ex)
                                {
                                    _logger.LogError("Error empresa antecedentes :" + empresa.EmCodigo);
                                    continue;
                                }
                               
                            }

                            //ramo
                            var ramo = await _mempresaDomain.GetmEmpresaRamoByCodigoAsync(empresa.EmCodigo);

                            if (ramo != null)
                            {
                                var listTraductionBranch = new List<Traduction>();
                                listTraductionBranch.AddRange(await context.Traductions.Where(x => x.IdCompany == inserted && x.Identifier.Contains("_R_")).ToListAsync());

                                foreach (var item in listTraductionBranch)
                                {

                                    if (item.Identifier == "S_R_TOTALAREA")
                                    {
                                        if (ramo.EmAreaIng != null)
                                        {
                                            item.ShortValue = ramo.EmAreaIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_R_OTRHERLOCALS")
                                    {
                                        if (ramo.EmObservIng != null)
                                        {
                                            item.LargeValue = ramo.EmObservIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_R_PRINCACT")
                                    {
                                        if (ramo.EmActiviIng != null)
                                        {
                                            item.LargeValue = ramo.EmActiviIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_R_ADIBUS")
                                    {
                                        if (ramo.EmComenIng != null)
                                        {
                                            item.LargeValue = ramo.EmComenIng;
                                        }
                                    }
                                }
                                try
                                {
                                    var insertBranch = await _companyBranchDomain.AddAsync(new CompanyBranch
                                    {
                                        Id = 0,
                                        IdCompany = inserted,
                                        IdBranchSector = ramo.EmCatciiu1 == "A" ? 3 : ramo.EmCatciiu1 == "B" ? 4 : ramo.EmCatciiu1 == "C" ? 5 : null,
                                        IdBusinessBranch = ramo.CsCodigo == "0" ? 1 : ramo.CsCodigo == "1" ? 3 : ramo.CsCodigo == "2" ? 4 : ramo.CsCodigo == "3" ? 5 :
                                    ramo.CsCodigo == "4" ? 6 : ramo.CsCodigo == "5" ? 7 : ramo.CsCodigo == "6" ? 8 : ramo.CsCodigo == "7" ? 9 : ramo.CsCodigo == "8" ? 10 :
                                    ramo.CsCodigo == "9" ? 11 : ramo.CsCodigo == "10" ? 12 : ramo.CsCodigo == "11" ? 13 : ramo.CsCodigo == "12" ? 14 : ramo.CsCodigo == "13" ? 15 :
                                    ramo.CsCodigo == "14" ? 16 : ramo.CsCodigo == "15" ? 17 : ramo.CsCodigo == "16" ? 18 : ramo.CsCodigo == "17" ? 19 : ramo.CsCodigo == "18" ? 20 :
                                    ramo.CsCodigo == "19" ? 21 : ramo.CsCodigo == "20" ? 22 : ramo.CsCodigo == "21" ? 23 : ramo.CsCodigo == "22" ? 24 : ramo.CsCodigo == "23" ? 25 :
                                    ramo.CsCodigo == "24" ? 26 : ramo.CsCodigo == "25" ? 27 : ramo.CsCodigo == "26" ? 28 : ramo.CsCodigo == "27" ? 29 : ramo.CsCodigo == "28" ? 30 :
                                    ramo.CsCodigo == "29" ? 31 : ramo.CsCodigo == "30" ? 32 : null,
                                        SpecificActivities = await _mempresaDomain.GetActividadesByCodigo(empresa.EmCodigo),
                                        Import = ramo.EmLogimp == "Si" ? true : ramo.EmLogimp == "No" ? false : null,
                                        Export = ramo.EmLogexp == "Si" ? true : ramo.EmLogexp == "No" ? false : null,
                                        CountriesImport = ramo.EmImport1,
                                        CountriesExport = ramo.EmExport1,
                                        CountriesImportEng = ramo.EmImport1Ing,
                                        CountriesExportEng = ramo.EmExport1Ing,
                                        CashSaleComentary = ramo.EmVencon,
                                        CreditSaleComentary = ramo.EmVencre,
                                        TerritorySaleComentary = ramo.EmTervta,
                                        AbroadSaleComentary = ramo.EmVtaext,
                                        NationalPurchasesComentary = ramo.EmComnac,
                                        InternationalPurchasesComentary = ramo.EmComext,
                                        WorkerNumber =string.IsNullOrEmpty(ramo.EmTraba1)?null:int.Parse(ramo.EmTraba1),
                                        IdLandOwnership = ramo.EmTiploc == "Alquilado" ? 2 : ramo.EmTiploc == "Comodato" ? 3 :
                                     ramo.EmTiploc == "Compartido" ? 4 : ramo.EmTiploc == "No Revelado" ? 5 : ramo.EmTiploc == "Oficina Virtual" ? 6 :
                                      ramo.EmTiploc == "Propio Cancelado" ? 7 : ramo.EmTiploc == "Propio Pagandolo" ? 8 : null,
                                        TotalArea = ramo.EmArea,
                                        OtherLocations = ramo.EmObserv,
                                        PreviousAddress = ramo.EmDomant,
                                        ActivityDetailCommentary = ramo.EmActivi,
                                        AditionalCommentary = ramo.EmComen,
                                        TabCommentary = ramo.EmComenTab
                                    }, listTraductionBranch);
                                }
                                catch(Exception ex)
                                {
                                    _logger.LogError("Error empresa ramo :" + empresa.EmCodigo);
                                    continue;
                                }
                               
                                
                                //finanzas
                                var finanzas = await _mempresaDomain.GetmEmpresaFinanzasByCodigoAsync(empresa.EmCodigo);

                                if (finanzas != null)
                                {
                                    var listTraductionFinantial = new List<Traduction>();
                                    listTraductionFinantial.AddRange(await context.Traductions.Where(x => x.IdCompany == inserted && x.Identifier.Contains("_F_")).ToListAsync());

                                    foreach (var item in listTraductionFinantial)
                                    {

                                        if (item.Identifier == "S_F_JOB")
                                        {
                                            if (finanzas.EmCargosIng != null)
                                            {
                                                item.ShortValue = finanzas.EmCargosIng;
                                            }
                                        }
                                        else if (item.Identifier == "L_F_COMENT")
                                        {
                                            if (finanzas.EmConinfIng != null)
                                            {
                                                item.LargeValue = finanzas.EmConinfIng;
                                            }
                                        }
                                        else if (item.Identifier == "L_F_PRINCACTIV")
                                        {
                                            if (finanzas.EmPropieIng != null)
                                            {
                                                item.LargeValue = finanzas.EmPropieIng;
                                            }
                                        }
                                        else if (item.Identifier == "L_F_SELECTFIN")
                                        {
                                            if (finanzas.EmSitfinIng != null)
                                            {
                                                item.LargeValue = finanzas.EmSitfinIng;
                                            }
                                        }
                                        else if (item.Identifier == "L_F_ANALISTCOM")
                                        {
                                            if (finanzas.EmAnalistaIng != null)
                                            {
                                                item.LargeValue = finanzas.EmAnalistaIng;
                                            }
                                        }
                                    }
                                    try
                                    {
                                        var insertFinantial = await _companyFinancialInformationDomain.AddCompanyFinancialInformation(new CompanyFinancialInformation
                                        {
                                            Id = 0,
                                            IdCompany = inserted,
                                            Interviewed = finanzas.EmEntrev,
                                            WorkPosition = finanzas.EmCargos,
                                            IdCollaborationDegree = finanzas.GcCodigo == 1 ? 1 : finanzas.GcCodigo == 2 ? 3 : finanzas.GcCodigo == 3 ? 4 :
                                        finanzas.GcCodigo == 4 ? 5 : finanzas.GcCodigo == 5 ? 6 : finanzas.GcCodigo == 6 ? 7 : finanzas.GcCodigo == 7 ? 10 :
                                        finanzas.GcCodigo == 8 ? 8 : finanzas.GcCodigo == 9 ? 9 : finanzas.GcCodigo == 13 ? 2 : null,
                                            InterviewCommentary = finanzas.EmConinf,
                                            Auditors = empresa.EmAudito,
                                            IdFinancialSituacion = finanzas.SfCodigo == "00" ? 8 : finanzas.SfCodigo == "01" ? 9 : finanzas.SfCodigo == "02" ? 10 :
                                         finanzas.SfCodigo == "03" ? 11 : finanzas.SfCodigo == "04" ? 12 : finanzas.SfCodigo == "05" ? 13 : finanzas.SfCodigo == "06" ? 14 :
                                          finanzas.SfCodigo == "07" ? 15 : null,
                                            FinancialCommentarySelected = finanzas.EmSitfin,
                                            MainFixedAssets = finanzas.EmPropie,
                                            AnalystCommentary = finanzas.EmAnalista
                                        }, listTraductionFinantial);
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError("Error empresa ramo :" + empresa.EmCodigo);
                                        continue;
                                    }
                                }
                                //sbs
                                var listTraductionSbs = new List<Traduction>();
                                listTraductionSbs.AddRange(await context.Traductions.Where(x => x.IdCompany == inserted && x.Identifier.Contains("_S_")).ToListAsync());
                                var aval = await _mempresaDomain.GetmEmpresaAvalByCodigoAsync(empresa.EmCodigo);
                                foreach (var item in listTraductionSbs)
                                {

                                    if (item.Identifier == "L_S_COMENTARY")
                                    {
                                        if (empresa.EmCenrieIng != null)
                                        {
                                            item.LargeValue = empresa.EmCenrieIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_S_BANCARIOS")
                                    {
                                        if (empresa.EmSupbanIng != null)
                                        {
                                            item.LargeValue = empresa.EmSupbanIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_S_AVALES")
                                    {
                                        if (aval != null)
                                        {
                                            item.LargeValue = aval.AvObservacionIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_S_LITIG")
                                    {
                                        if (empresa.EmComlitIng != null)
                                        {
                                            item.LargeValue = empresa.EmComlitIng;
                                        }
                                    }
                                    else if (item.Identifier == "L_S_CREDHIS")
                                    {
                                        if (empresa.EmAntcreIng != null)
                                        {
                                            item.LargeValue = empresa.EmAntcreIng;
                                        }
                                    }
                                }
                                if (empresa != null)
                                {
                                    try
                                    {
                                        var insertSbs = await _companySBSDomain.AddCompanySBS(new CompanySb
                                        {
                                            Id = 0,
                                            IdCompany = inserted,
                                            IdOpcionalCommentarySbs = 1,
                                            AditionalCommentaryRiskCenter = empresa.EmCenrie,
                                            DebtRecordedDate = StaticFunctions.VerifyDate(empresa.EmFecreg),
                                            ExchangeRate = (decimal)empresa.EmTcsbs,
                                            BankingCommentary = empresa.EmSupban,
                                            EndorsementsObservations = aval != null ? aval.AvObservacion : "",
                                            ReferentOrAnalyst = empresa.PerCodref,
                                            Date = empresa.EmFecref,
                                            LitigationsCommentary = empresa.EmComlit,
                                            CreditHistoryCommentary = empresa.EmAntcre,
                                            GuaranteesOfferedNc = (decimal)empresa.EmGaomn,
                                            GuaranteesOfferedFc = (decimal)empresa.EmGaome

                                        }, listTraductionSbs);
                                    }
                                    catch(Exception ex)
                                    {
                                        _logger.LogError("Error empresa ramo :" + empresa.EmCodigo);
                                        continue;
                                    }
                                }
                                //opinion de credito

                                if (empresa != null)
                                {
                                    var listTraductionOpCred = new List<Traduction>();
                                    listTraductionOpCred.AddRange(await context.Traductions.Where(x => x.IdCompany == inserted && x.Identifier.Contains("_O_")).ToListAsync());

                                    foreach (var item in listTraductionOpCred)
                                    {

                                        if (item.Identifier == "S_O_QUERYCREDIT")
                                        {
                                            if (empresa.EmMtopcrIng != null)
                                            {
                                                item.ShortValue = empresa.EmMtopcrIng;
                                            }
                                        }
                                        else if (item.Identifier == "S_O_SUGCREDIT")
                                        {
                                            if (empresa.EmCrerecIng != null)
                                            {
                                                item.LargeValue = empresa.EmCrerecIng;
                                            }
                                        }
                                        else if (item.Identifier == "L_O_COMENTARY")
                                        {
                                            if (empresa.EmOcDescriIng != null)
                                            {
                                                item.LargeValue = empresa.EmOcDescriIng;
                                            }
                                        }
                                    }
                                    try
                                    {
                                        var insertOpinionCredito = await _companyCreditOpinionDomain.AddCreditOpinion(new CompanyCreditOpinion
                                        {
                                            Id = 0,
                                            IdCompany = inserted,
                                            CreditRequest = true,
                                            ConsultedCredit = empresa.EmMtopcr,
                                            SuggestedCredit = empresa.EmCrerec,
                                            CurrentCommentary = empresa.EmOcDescri,
                                            PreviousCommentary = empresa.EmOpicre

                                        }, listTraductionOpCred);
                                    }
                                    catch(Exception ex)
                                    {
                                        _logger.LogError("Error empresa opinion credito :" + empresa.EmCodigo);
                                        continue;
                                    }
                                }

                                //opinion de credito

                                if (empresa != null)
                                {
                                    var listTraductionInfGen = new List<Traduction>();
                                    listTraductionInfGen.AddRange(await context.Traductions.Where(x => x.IdCompany == inserted && x.Identifier.Contains("_I_")).ToListAsync());

                                    foreach (var item in listTraductionInfGen)
                                    {

                                        if (item.Identifier == "L_I_GENERAL")
                                        {
                                            if (empresa.EmInfgenIng != null)
                                            {
                                                item.ShortValue = empresa.EmInfgenIng;
                                            }
                                        }
                                    }
                                    try
                                    {
                                        var insertInfGen = await _companyGeneralInformationDomain.AddGeneralInformation(new CompanyGeneralInformation
                                        {
                                            Id = 0,
                                            IdCompany = inserted,
                                            GeneralInfo = empresa.EmInfgen

                                        }, listTraductionInfGen);
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError("Error empresa info general:" + empresa.EmCodigo);
                                        continue;
                                    }
                                }

                                //balance G
                                var balance = await _mempresaDomain.GetmEmpresaBalanceByCodigoAsync(empresa.EmCodigo);


                                if (balance != null)
                                {
                                    if (balance.BaFecbal1 != null && balance.BaFecbal1 != "")
                                    {
                                        try
                                        {
                                            var insertBalance = await _financialBalanceDomain.AddAsync(new FinancialBalance
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                Date = StaticFunctions.VerifyDate(balance.BaFecbal1),
                                                BalanceType = "GENERAL",
                                                BalanceTypeEng = balance.BaTipBal1Ing,
                                                Duration = balance.BaTimbal1,
                                                DurationEng = balance.BaTimBal1Ing,
                                                IdCurrency = balance.BaMoneda1 == "USD" ? 1 : balance.BaMoneda1 == "PEN" ? 31 :
                                            balance.BaMoneda1 == "UYU" ? 154 : balance.BaMoneda1 == "MXN" ? 15 :
                                            balance.BaMoneda1 == "PAB" ? 120 : balance.BaMoneda1 == "DOP" ? 126 :
                                            balance.BaMoneda1 == "GTQ" ? 80 : balance.BaMoneda1 == "COP" ? 63 :
                                            balance.BaMoneda1 == "BOB" ? 51 : balance.BaMoneda1 == "ARS" ? 38 :
                                            balance.BaMoneda1 == "CRC" ? 66 : balance.BaMoneda1 == "PYG" ? 122 :
                                            balance.BaMoneda1 == "CLP" ? 29 : balance.BaMoneda1 == "BRL" ? 20 :
                                            balance.BaMoneda1 == "HNL" ? 84 : balance.BaMoneda1 == "NIO" ? 115 :
                                            balance.BaMoneda1 == "JMD" ? 93 : balance.BaMoneda1 == "MYR" ? 105 :
                                            balance.BaMoneda1 == "EUR" ? 2 : balance.BaMoneda1 == "COL" ? 63 :
                                            balance.BaMoneda1 == "VND" ? 156 : balance.BaMoneda1 == "GYD" ? 82 :
                                            balance.BaMoneda1 == "UDS" ? 1 : balance.BaMoneda1 == "BBD" ? 44 :
                                            balance.BaMoneda1 == "BZD" ? 46 : balance.BaMoneda1 == "GBP" ? 4 :
                                            balance.BaMoneda1 == "TTD" ? 148 : balance.BaMoneda1 == "KYD" ? 88 :
                                            balance.BaMoneda1 == "INR" ? 16 : balance.BaMoneda1 == "PKR" ? 119 :
                                            balance.BaMoneda1 == "BSD" ? 42 : balance.BaMoneda1 == "SRD" ? 146 :
                                            balance.BaMoneda1 == "TRY" ? 19 : balance.BaMoneda1 == "SAR" ? 37 :
                                            balance.BaMoneda1 == "CNY" ? 8 : balance.BaMoneda1 == "XCD" ? 36 :
                                            balance.BaMoneda1 == "HUF" ? 26 : balance.BaMoneda1 == "AWG" ? 40 :
                                            balance.BaMoneda1 == "CHF" ? 7 : balance.BaMoneda1 == "TWD" ? 21 :
                                            balance.BaMoneda1 == "DKK" ? 22 : balance.BaMoneda1 == "ANG" ? 69 :
                                            balance.BaMoneda1 == "PLN" ? 23 : balance.BaMoneda1 == "MNX" ? 15 : null,
                                                ExchangeRate = (decimal)balance.BaTipcam1,
                                                Sales = (decimal)balance.BaVentas1,
                                                Utilities = (decimal)balance.BaUtiper1,
                                                TotalAssets = (decimal)balance.BaTotact1,
                                                TotalCurrentAssets = (decimal)balance.BaTotcor1,
                                                ACashBoxBank = (decimal)balance.BaCajban1,
                                                AToCollect = (decimal)balance.BaPorcob1,
                                                AInventory = (decimal)balance.BaInvent1,
                                                AOtherCurrentAssets = (decimal)balance.BaActcorotr1,
                                                TotalNonCurrentAssets = (decimal)balance.BaFijo1 + (decimal)balance.BaActotr1,
                                                AFixed = (decimal)balance.BaFijo1,
                                                AOtherNonCurrentAssets = (decimal)balance.BaActotr1,
                                                TotalLliabilities = (decimal)balance.BaPastot1,
                                                TotalCurrentLiabilities = (decimal)balance.BaTotcrr1,
                                                LCashBoxBank = (decimal)balance.BaBanpro1,
                                                LOtherCurrentLiabilities = (decimal)balance.BaPasotr1,
                                                TotalNonCurrentLiabilities = (decimal)balance.BaLarpla1 + (decimal)balance.BaCorotr1,
                                                LLongTerm = (decimal)balance.BaLarpla1,
                                                LOtherNonCurrentLiabilities = (decimal)balance.BaCorotr1,
                                                TotalPatrimony = (decimal)balance.BaTotpat1,
                                                PCapital = (decimal)balance.BaCapita1,
                                                PStockPile = (decimal)balance.BaReser1,
                                                PUtilities = (decimal)balance.BaUtili1,
                                                POther = (decimal)balance.BaPatotr1,
                                                TotalLiabilitiesPatrimony = (decimal)balance.BaTotpas1,
                                                LiquidityRatio = (decimal)balance.BaTotcor1 / (decimal)balance.BaTotcrr1,
                                                DebtRatio = ((decimal)balance.BaTotpat1 / (decimal)balance.BaTotcrr1) * 100,
                                                ProfitabilityRatio = ((decimal)balance.BaUtiper1 / (decimal)balance.BaVentas1) * 100,
                                                WorkingCapital = (decimal)balance.BaTotcor1 - (decimal)balance.BaTotcrr1
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                        
                                    }

                                    if (balance.BaFecbal2 != null && balance.BaFecbal2 != "")
                                    {
                                        try
                                        {
                                            var insertBalance = await _financialBalanceDomain.AddAsync(new FinancialBalance
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                Date = StaticFunctions.VerifyDate(balance.BaFecbal2),
                                                BalanceType = "GENERAL",
                                                BalanceTypeEng = balance.BaTipBal2Ing,
                                                Duration = balance.BaTimbal2,
                                                DurationEng = balance.BaTimBal2Ing,
                                                IdCurrency = balance.BaMoneda2 == "USD" ? 1 : balance.BaMoneda2 == "PEN" ? 31 :
                                            balance.BaMoneda2 == "UYU" ? 154 : balance.BaMoneda2 == "MXN" ? 15 :
                                            balance.BaMoneda2 == "PAB" ? 120 : balance.BaMoneda2 == "DOP" ? 126 :
                                            balance.BaMoneda2 == "GTQ" ? 80 : balance.BaMoneda2 == "COP" ? 63 :
                                            balance.BaMoneda2 == "BOB" ? 51 : balance.BaMoneda2 == "ARS" ? 38 :
                                            balance.BaMoneda2 == "CRC" ? 66 : balance.BaMoneda2 == "PYG" ? 122 :
                                            balance.BaMoneda2 == "CLP" ? 29 : balance.BaMoneda2 == "BRL" ? 20 :
                                            balance.BaMoneda2 == "HNL" ? 84 : balance.BaMoneda2 == "NIO" ? 115 :
                                            balance.BaMoneda2 == "JMD" ? 93 : balance.BaMoneda2 == "MYR" ? 105 :
                                            balance.BaMoneda2 == "EUR" ? 2 : balance.BaMoneda2 == "COL" ? 63 :
                                            balance.BaMoneda2 == "VND" ? 156 : balance.BaMoneda2 == "GYD" ? 82 :
                                            balance.BaMoneda2 == "UDS" ? 1 : balance.BaMoneda2 == "BBD" ? 44 :
                                            balance.BaMoneda2 == "BZD" ? 46 : balance.BaMoneda2 == "GBP" ? 4 :
                                            balance.BaMoneda2 == "TTD" ? 148 : balance.BaMoneda2 == "KYD" ? 88 :
                                            balance.BaMoneda2 == "INR" ? 16 : balance.BaMoneda2 == "PKR" ? 119 :
                                            balance.BaMoneda2 == "BSD" ? 42 : balance.BaMoneda2 == "SRD" ? 146 :
                                            balance.BaMoneda2 == "TRY" ? 19 : balance.BaMoneda2 == "SAR" ? 37 :
                                            balance.BaMoneda2 == "CNY" ? 8 : balance.BaMoneda2 == "XCD" ? 36 :
                                            balance.BaMoneda2 == "HUF" ? 26 : balance.BaMoneda2 == "AWG" ? 40 :
                                            balance.BaMoneda2 == "CHF" ? 7 : balance.BaMoneda2 == "TWD" ? 21 :
                                            balance.BaMoneda2 == "DKK" ? 22 : balance.BaMoneda2 == "ANG" ? 69 :
                                            balance.BaMoneda2 == "PLN" ? 23 : balance.BaMoneda2 == "MNX" ? 15 : null,
                                                ExchangeRate = (decimal)balance.BaTipcam2,
                                                Sales = (decimal)balance.BaVentas2,
                                                Utilities = (decimal)balance.BaUtiper2,
                                                TotalAssets = (decimal)balance.BaTotact2,
                                                TotalCurrentAssets = (decimal)balance.BaTotcor2,
                                                ACashBoxBank = (decimal)balance.BaCajban2,
                                                AToCollect = (decimal)balance.BaPorcob2,
                                                AInventory = (decimal)balance.BaInvent2,
                                                AOtherCurrentAssets = (decimal)balance.BaActCorOtr2,
                                                TotalNonCurrentAssets = (decimal)balance.BaFijo2 + (decimal)balance.BaActotr2,
                                                AFixed = (decimal)balance.BaFijo2,
                                                AOtherNonCurrentAssets = (decimal)balance.BaActotr2,
                                                TotalLliabilities = (decimal)balance.BaPastot2,
                                                TotalCurrentLiabilities = (decimal)balance.BaTotcrr2,
                                                LCashBoxBank = (decimal)balance.BaBanpro2,
                                                LOtherCurrentLiabilities = (decimal)balance.BaPasotr2,
                                                TotalNonCurrentLiabilities = (decimal)balance.BaLarpla2 + (decimal)balance.BaCorotr2,
                                                LLongTerm = (decimal)balance.BaLarpla2,
                                                LOtherNonCurrentLiabilities = (decimal)balance.BaCorotr2,
                                                TotalPatrimony = (decimal)balance.BaTotpat2,
                                                PCapital = (decimal)balance.BaCapita2,
                                                PStockPile = (decimal)balance.BaReser2,
                                                PUtilities = (decimal)balance.BaUtili2,
                                                POther = (decimal)balance.BaPatOtr2,
                                                TotalLiabilitiesPatrimony = (decimal)balance.BaTotpas2,
                                                LiquidityRatio = (decimal)balance.BaTotcor2 / (decimal)balance.BaTotcrr2,
                                                DebtRatio = ((decimal)balance.BaTotpat2 / (decimal)balance.BaTotcrr2) * 100,
                                                ProfitabilityRatio = ((decimal)balance.BaUtiper2 / (decimal)balance.BaVentas2) * 100,
                                                WorkingCapital = (decimal)balance.BaTotcor2 - (decimal)balance.BaTotcrr2
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }
                                    if (balance.BaFecbal3 != null && balance.BaFecbal3 != "")
                                    {
                                        try
                                        {
                                            var insertBalance = await _financialBalanceDomain.AddAsync(new FinancialBalance
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                Date = StaticFunctions.VerifyDate(balance.BaFecbal3),
                                                BalanceType = "GENERAL",
                                                BalanceTypeEng = balance.BaTipBal3Ing,
                                                Duration = balance.BaTimbal3,
                                                DurationEng = balance.BaTimBal3Ing,
                                                IdCurrency = balance.BaMoneda3 == "USD" ? 1 : balance.BaMoneda3 == "PEN" ? 31 :
                                            balance.BaMoneda3 == "UYU" ? 154 : balance.BaMoneda3 == "MXN" ? 15 :
                                            balance.BaMoneda3 == "PAB" ? 120 : balance.BaMoneda3 == "DOP" ? 126 :
                                            balance.BaMoneda3 == "GTQ" ? 80 : balance.BaMoneda3 == "COP" ? 63 :
                                            balance.BaMoneda3 == "BOB" ? 51 : balance.BaMoneda3 == "ARS" ? 38 :
                                            balance.BaMoneda3 == "CRC" ? 66 : balance.BaMoneda3 == "PYG" ? 122 :
                                            balance.BaMoneda3 == "CLP" ? 29 : balance.BaMoneda3 == "BRL" ? 20 :
                                            balance.BaMoneda3 == "HNL" ? 84 : balance.BaMoneda3 == "NIO" ? 115 :
                                            balance.BaMoneda3 == "JMD" ? 93 : balance.BaMoneda3 == "MYR" ? 105 :
                                            balance.BaMoneda3 == "EUR" ? 2 : balance.BaMoneda3 == "COL" ? 63 :
                                            balance.BaMoneda3 == "VND" ? 156 : balance.BaMoneda3 == "GYD" ? 82 :
                                            balance.BaMoneda3 == "UDS" ? 1 : balance.BaMoneda3 == "BBD" ? 44 :
                                            balance.BaMoneda3 == "BZD" ? 46 : balance.BaMoneda3 == "GBP" ? 4 :
                                            balance.BaMoneda3 == "TTD" ? 148 : balance.BaMoneda3 == "KYD" ? 88 :
                                            balance.BaMoneda3 == "INR" ? 16 : balance.BaMoneda3 == "PKR" ? 119 :
                                            balance.BaMoneda3 == "BSD" ? 42 : balance.BaMoneda3 == "SRD" ? 146 :
                                            balance.BaMoneda3 == "TRY" ? 19 : balance.BaMoneda3 == "SAR" ? 37 :
                                            balance.BaMoneda3 == "CNY" ? 8 : balance.BaMoneda3 == "XCD" ? 36 :
                                            balance.BaMoneda3 == "HUF" ? 26 : balance.BaMoneda3 == "AWG" ? 40 :
                                            balance.BaMoneda3 == "CHF" ? 7 : balance.BaMoneda3 == "TWD" ? 21 :
                                            balance.BaMoneda3 == "DKK" ? 22 : balance.BaMoneda3 == "ANG" ? 69 :
                                            balance.BaMoneda3 == "PLN" ? 23 : balance.BaMoneda3 == "MNX" ? 15 : null,
                                                ExchangeRate = (decimal)balance.BaTipcam3,
                                                Sales = (decimal)balance.BaVentas3,
                                                Utilities = (decimal)balance.BaUtiper3,
                                                TotalAssets = (decimal)balance.BaTotact3,
                                                TotalCurrentAssets = (decimal)balance.BaTotcor3,
                                                ACashBoxBank = (decimal)balance.BaCajban3,
                                                AToCollect = (decimal)balance.BaPorcob3,
                                                AInventory = (decimal)balance.BaInvent3,
                                                AOtherCurrentAssets = (decimal)balance.BaActCorOtr3,
                                                TotalNonCurrentAssets = (decimal)balance.BaFijo3 + (decimal)balance.BaActotr3,
                                                AFixed = (decimal)balance.BaFijo3,
                                                AOtherNonCurrentAssets = (decimal)balance.BaActotr3,
                                                TotalLliabilities = (decimal)balance.BaPastot3,
                                                TotalCurrentLiabilities = (decimal)balance.BaTotcrr3,
                                                LCashBoxBank = (decimal)balance.BaBanpro3,
                                                LOtherCurrentLiabilities = (decimal)balance.BaPasotr3,
                                                TotalNonCurrentLiabilities = (decimal)balance.BaLarpla3 + (decimal)balance.BaCorotr3,
                                                LLongTerm = (decimal)balance.BaLarpla3,
                                                LOtherNonCurrentLiabilities = (decimal)balance.BaCorotr3,
                                                TotalPatrimony = (decimal)balance.BaTotpat3,
                                                PCapital = (decimal)balance.BaCapita3,
                                                PStockPile = (decimal)balance.BaReser3,
                                                PUtilities = (decimal)balance.BaUtili3,
                                                POther = (decimal)balance.BaPatOtr3,
                                                TotalLiabilitiesPatrimony = (decimal)balance.BaTotpas3,
                                                LiquidityRatio = (decimal)balance.BaTotcor3 / (decimal)balance.BaTotcrr3,
                                                DebtRatio = ((decimal)balance.BaTotpat3 / (decimal)balance.BaTotcrr3) * 100,
                                                ProfitabilityRatio = ((decimal)balance.BaUtiper3 / (decimal)balance.BaVentas3) * 100,
                                                WorkingCapital = (decimal)balance.BaTotcor3 - (decimal)balance.BaTotcrr3
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }
                                    if (balance.BaFecbal4 != null && balance.BaFecbal4 != "")
                                    {
                                        try
                                        {
                                            var insertBalance = await _financialBalanceDomain.AddAsync(new FinancialBalance
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                Date = StaticFunctions.VerifyDate(balance.BaFecbal4),
                                                BalanceType = "GENERAL",
                                                BalanceTypeEng = balance.BaTipBal4Ing,
                                                Duration = balance.BaTimbal4,
                                                DurationEng = balance.BaTimBal4Ing,
                                                IdCurrency = balance.BaMoneda4 == "USD" ? 1 : balance.BaMoneda4 == "PEN" ? 31 :
                                            balance.BaMoneda4 == "UYU" ? 154 : balance.BaMoneda4 == "MXN" ? 15 :
                                            balance.BaMoneda4 == "PAB" ? 120 : balance.BaMoneda4 == "DOP" ? 126 :
                                            balance.BaMoneda4 == "GTQ" ? 80 : balance.BaMoneda4 == "COP" ? 63 :
                                            balance.BaMoneda4 == "BOB" ? 51 : balance.BaMoneda4 == "ARS" ? 38 :
                                            balance.BaMoneda4 == "CRC" ? 66 : balance.BaMoneda4 == "PYG" ? 122 :
                                            balance.BaMoneda4 == "CLP" ? 29 : balance.BaMoneda4 == "BRL" ? 20 :
                                            balance.BaMoneda4 == "HNL" ? 84 : balance.BaMoneda4 == "NIO" ? 115 :
                                            balance.BaMoneda4 == "JMD" ? 93 : balance.BaMoneda4 == "MYR" ? 105 :
                                            balance.BaMoneda4 == "EUR" ? 2 : balance.BaMoneda4 == "COL" ? 63 :
                                            balance.BaMoneda4 == "VND" ? 156 : balance.BaMoneda4 == "GYD" ? 82 :
                                            balance.BaMoneda4 == "UDS" ? 1 : balance.BaMoneda4 == "BBD" ? 44 :
                                            balance.BaMoneda4 == "BZD" ? 46 : balance.BaMoneda4 == "GBP" ? 4 :
                                            balance.BaMoneda4 == "TTD" ? 148 : balance.BaMoneda4 == "KYD" ? 88 :
                                            balance.BaMoneda4 == "INR" ? 16 : balance.BaMoneda4 == "PKR" ? 119 :
                                            balance.BaMoneda4 == "BSD" ? 42 : balance.BaMoneda4 == "SRD" ? 146 :
                                            balance.BaMoneda4 == "TRY" ? 19 : balance.BaMoneda4 == "SAR" ? 37 :
                                            balance.BaMoneda4 == "CNY" ? 8 : balance.BaMoneda4 == "XCD" ? 36 :
                                            balance.BaMoneda4 == "HUF" ? 26 : balance.BaMoneda4 == "AWG" ? 40 :
                                            balance.BaMoneda4 == "CHF" ? 7 : balance.BaMoneda4 == "TWD" ? 21 :
                                            balance.BaMoneda4 == "DKK" ? 22 : balance.BaMoneda4 == "ANG" ? 69 :
                                            balance.BaMoneda4 == "PLN" ? 23 : balance.BaMoneda4 == "MNX" ? 15 : null,
                                                ExchangeRate = (decimal)balance.BaTipcam4,
                                                Sales = (decimal)balance.BaVentas4,
                                                Utilities = (decimal)balance.BaUtiper4,
                                                TotalAssets = (decimal)balance.BaTotact4,
                                                TotalCurrentAssets = (decimal)balance.BaTotcor4,
                                                ACashBoxBank = (decimal)balance.BaCajban4,
                                                AToCollect = (decimal)balance.BaPorcob4,
                                                AInventory = (decimal)balance.BaInvent4,
                                                AOtherCurrentAssets = (decimal)balance.BaActCorOtr4,
                                                TotalNonCurrentAssets = (decimal)balance.BaFijo4 + (decimal)balance.BaActotr4,
                                                AFixed = (decimal)balance.BaFijo4,
                                                AOtherNonCurrentAssets = (decimal)balance.BaActotr4,
                                                TotalLliabilities = (decimal)balance.BaPastot4,
                                                TotalCurrentLiabilities = (decimal)balance.BaTotcrr4,
                                                LCashBoxBank = (decimal)balance.BaBanpro4,
                                                LOtherCurrentLiabilities = (decimal)balance.BaPasotr4,
                                                TotalNonCurrentLiabilities = (decimal)balance.BaLarpla4 + (decimal)balance.BaCorotr4,
                                                LLongTerm = (decimal)balance.BaLarpla4,
                                                LOtherNonCurrentLiabilities = (decimal)balance.BaCorotr4,
                                                TotalPatrimony = (decimal)balance.BaTotpat4,
                                                PCapital = (decimal)balance.BaCapita4,
                                                PStockPile = (decimal)balance.BaReser4,
                                                PUtilities = (decimal)balance.BaUtili4,
                                                POther = (decimal)balance.BaPatOtr4,
                                                TotalLiabilitiesPatrimony = (decimal)balance.BaTotpas4,
                                                LiquidityRatio = (decimal)balance.BaTotcor4 / (decimal)balance.BaTotcrr4,
                                                DebtRatio = ((decimal)balance.BaTotpat4 / (decimal)balance.BaTotcrr4) * 100,
                                                ProfitabilityRatio = ((decimal)balance.BaUtiper4 / (decimal)balance.BaVentas4) * 100,
                                                WorkingCapital = (decimal)balance.BaTotcor4 - (decimal)balance.BaTotcrr4
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }
                                }


                                //balance S
                                var balanceS = await _mempresaDomain.GetmEmpresaBalanceSitByCodigoAsync(empresa.EmCodigo);


                                if (balanceS != null)
                                {
                                    if (balanceS.BsFecbal1 != null && balanceS.BsFecbal1 != "")
                                    {
                                        try
                                        {
                                            var insertBalance = await _financialBalanceDomain.AddAsync(new FinancialBalance
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                Date = StaticFunctions.VerifyDate(balanceS.BsFecbal1),
                                                BalanceType = "SITUACIONAL",
                                                BalanceTypeEng = balanceS.BsTipBal1Ing,
                                                Duration = balanceS.BsTimbal1,
                                                DurationEng = balanceS.BsTimBal1Ing,
                                                IdCurrency = balanceS.BsMoneda1 == "USD" ? 1 : balanceS.BsMoneda1 == "PEN" ? 31 :
                                            balanceS.BsMoneda1 == "UYU" ? 154 : balanceS.BsMoneda1 == "MXN" ? 15 :
                                            balanceS.BsMoneda1 == "PAB" ? 120 : balanceS.BsMoneda1 == "DOP" ? 126 :
                                            balanceS.BsMoneda1 == "GTQ" ? 80 : balanceS.BsMoneda1 == "COP" ? 63 :
                                            balanceS.BsMoneda1 == "BOB" ? 51 : balanceS.BsMoneda1 == "ARS" ? 38 :
                                            balanceS.BsMoneda1 == "CRC" ? 66 : balanceS.BsMoneda1 == "PYG" ? 122 :
                                            balanceS.BsMoneda1 == "CLP" ? 29 : balanceS.BsMoneda1 == "BRL" ? 20 :
                                            balanceS.BsMoneda1 == "HNL" ? 84 : balanceS.BsMoneda1 == "NIO" ? 115 :
                                            balanceS.BsMoneda1 == "JMD" ? 93 : balanceS.BsMoneda1 == "MYR" ? 105 :
                                            balanceS.BsMoneda1 == "EUR" ? 2 : balanceS.BsMoneda1 == "COL" ? 63 :
                                            balanceS.BsMoneda1 == "VND" ? 156 : balanceS.BsMoneda1 == "GYD" ? 82 :
                                            balanceS.BsMoneda1 == "UDS" ? 1 : balanceS.BsMoneda1 == "BBD" ? 44 :
                                            balanceS.BsMoneda1 == "BZD" ? 46 : balanceS.BsMoneda1 == "GBP" ? 4 :
                                            balanceS.BsMoneda1 == "TTD" ? 148 : balanceS.BsMoneda1 == "KYD" ? 88 :
                                            balanceS.BsMoneda1 == "INR" ? 16 : balanceS.BsMoneda1 == "PKR" ? 119 :
                                            balanceS.BsMoneda1 == "BSD" ? 42 : balanceS.BsMoneda1 == "SRD" ? 146 :
                                            balanceS.BsMoneda1 == "TRY" ? 19 : balanceS.BsMoneda1 == "SAR" ? 37 :
                                            balanceS.BsMoneda1 == "CNY" ? 8 : balanceS.BsMoneda1 == "XCD" ? 36 :
                                            balanceS.BsMoneda1 == "HUF" ? 26 : balanceS.BsMoneda1 == "AWG" ? 40 :
                                            balanceS.BsMoneda1 == "CHF" ? 7 : balanceS.BsMoneda1 == "TWD" ? 21 :
                                            balanceS.BsMoneda1 == "DKK" ? 22 : balanceS.BsMoneda1 == "ANG" ? 69 :
                                            balanceS.BsMoneda1 == "PLN" ? 23 : balanceS.BsMoneda1 == "MNX" ? 15 : null,
                                                ExchangeRate = (decimal)balanceS.BsTipcam1,
                                                Sales = (decimal)balanceS.BsVentas1,
                                                Utilities = (decimal)balanceS.BsUtiper1,
                                                TotalAssets = (decimal)balanceS.BsTotact1,
                                                TotalCurrentAssets = (decimal)balanceS.BsTotcor1,
                                                ACashBoxBank = (decimal)balanceS.BsCajban1,
                                                AToCollect = (decimal)balanceS.BsPorcob1,
                                                AInventory = (decimal)balanceS.BsInvent1,
                                                AOtherCurrentAssets = (decimal)balanceS.BsActcorotr1,
                                                TotalNonCurrentAssets = (decimal)balanceS.BsFijo1 + (decimal)balanceS.BsActotr1,
                                                AFixed = (decimal)balanceS.BsFijo1,
                                                AOtherNonCurrentAssets = (decimal)balanceS.BsActotr1,
                                                TotalLliabilities = (decimal)balanceS.BsTotcrr1 + (decimal)balanceS.BsLarpla1 + (decimal)balanceS.BsCorotr1,
                                                TotalCurrentLiabilities = (decimal)balanceS.BsTotcrr1,
                                                LCashBoxBank = (decimal)balanceS.BsBanpro1,
                                                LOtherCurrentLiabilities = (decimal)balanceS.BsPasotr1,
                                                TotalNonCurrentLiabilities = (decimal)balanceS.BsLarpla1 + (decimal)balanceS.BsCorotr1,
                                                LLongTerm = (decimal)balanceS.BsLarpla1,
                                                LOtherNonCurrentLiabilities = (decimal)balanceS.BsCorotr1,
                                                TotalPatrimony = (decimal)balanceS.BsTotpat1,
                                                PCapital = (decimal)balanceS.BsCapita1,
                                                PStockPile = (decimal)balanceS.BsReser1,
                                                PUtilities = (decimal)balanceS.BsUtili1,
                                                POther = (decimal)balanceS.BsPatotr1,
                                                TotalLiabilitiesPatrimony = (decimal)balanceS.BsTotpas1,
                                                LiquidityRatio = (decimal)balanceS.BsTotcor1 / (decimal)balanceS.BsTotcrr1,
                                                DebtRatio = ((decimal)balanceS.BsTotpat1 / (decimal)balanceS.BsTotcrr1) * 100,
                                                ProfitabilityRatio = ((decimal)balanceS.BsUtiper1 / (decimal)balanceS.BsVentas1) * 100,
                                                WorkingCapital = (decimal)balanceS.BsTotcor1 - (decimal)balanceS.BsTotcrr1
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }
                                    if (balanceS.BsFecbal2 != null && balanceS.BsFecbal2 != "")
                                    {
                                        try
                                        {
                                            var insertBalance = await _financialBalanceDomain.AddAsync(new FinancialBalance
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                Date = StaticFunctions.VerifyDate(balanceS.BsFecbal2),
                                                BalanceType = "SITUACIONAL",
                                                BalanceTypeEng = balanceS.BsTipBal2Ing,
                                                Duration = balanceS.BsTimbal2,
                                                DurationEng = balanceS.BsTimBal2Ing,
                                                IdCurrency = balanceS.BsMoneda2 == "USD" ? 1 : balanceS.BsMoneda2 == "PEN" ? 31 :
                                           balanceS.BsMoneda2 == "UYU" ? 154 : balanceS.BsMoneda2 == "MXN" ? 15 :
                                           balanceS.BsMoneda2 == "PAB" ? 120 : balanceS.BsMoneda2 == "DOP" ? 126 :
                                           balanceS.BsMoneda2 == "GTQ" ? 80 : balanceS.BsMoneda2 == "COP" ? 63 :
                                           balanceS.BsMoneda2 == "BOB" ? 51 : balanceS.BsMoneda2 == "ARS" ? 38 :
                                           balanceS.BsMoneda2 == "CRC" ? 66 : balanceS.BsMoneda2 == "PYG" ? 122 :
                                           balanceS.BsMoneda2 == "CLP" ? 29 : balanceS.BsMoneda2 == "BRL" ? 20 :
                                           balanceS.BsMoneda2 == "HNL" ? 84 : balanceS.BsMoneda2 == "NIO" ? 115 :
                                           balanceS.BsMoneda2 == "JMD" ? 93 : balanceS.BsMoneda2 == "MYR" ? 105 :
                                           balanceS.BsMoneda2 == "EUR" ? 2 : balanceS.BsMoneda2 == "COL" ? 63 :
                                           balanceS.BsMoneda2 == "VND" ? 156 : balanceS.BsMoneda2 == "GYD" ? 82 :
                                           balanceS.BsMoneda2 == "UDS" ? 1 : balanceS.BsMoneda2 == "BBD" ? 44 :
                                           balanceS.BsMoneda2 == "BZD" ? 46 : balanceS.BsMoneda2 == "GBP" ? 4 :
                                           balanceS.BsMoneda2 == "TTD" ? 148 : balanceS.BsMoneda2 == "KYD" ? 88 :
                                           balanceS.BsMoneda2 == "INR" ? 16 : balanceS.BsMoneda2 == "PKR" ? 119 :
                                           balanceS.BsMoneda2 == "BSD" ? 42 : balanceS.BsMoneda2 == "SRD" ? 146 :
                                           balanceS.BsMoneda2 == "TRY" ? 19 : balanceS.BsMoneda2 == "SAR" ? 37 :
                                           balanceS.BsMoneda2 == "CNY" ? 8 : balanceS.BsMoneda2 == "XCD" ? 36 :
                                           balanceS.BsMoneda2 == "HUF" ? 26 : balanceS.BsMoneda2 == "AWG" ? 40 :
                                           balanceS.BsMoneda2 == "CHF" ? 7 : balanceS.BsMoneda2 == "TWD" ? 21 :
                                           balanceS.BsMoneda2 == "DKK" ? 22 : balanceS.BsMoneda2 == "ANG" ? 69 :
                                           balanceS.BsMoneda2 == "PLN" ? 23 : balanceS.BsMoneda2 == "MNX" ? 15 : null,
                                                ExchangeRate = (decimal)balanceS.BsTipcam2,
                                                Sales = (decimal)balanceS.BsVentas2,
                                                Utilities = (decimal)balanceS.BsUtiper2,
                                                TotalAssets = (decimal)balanceS.BsTotact2,
                                                TotalCurrentAssets = (decimal)balanceS.BsTotcor2,
                                                ACashBoxBank = (decimal)balanceS.BsCajban2,
                                                AToCollect = (decimal)balanceS.BsPorcob2,
                                                AInventory = (decimal)balanceS.BsInvent2,
                                                AOtherCurrentAssets = (decimal)balanceS.BsActCorOtr2,
                                                TotalNonCurrentAssets = (decimal)balanceS.BsFijo2 + (decimal)balanceS.BsActotr2,
                                                AFixed = (decimal)balanceS.BsFijo2,
                                                AOtherNonCurrentAssets = (decimal)balanceS.BsActotr2,
                                                TotalLliabilities = (decimal)balanceS.BsTotcrr2 + (decimal)balanceS.BsLarpla2 + (decimal)balanceS.BsCorotr2,
                                                TotalCurrentLiabilities = (decimal)balanceS.BsTotcrr2,
                                                LCashBoxBank = (decimal)balanceS.BsBanpro2,
                                                LOtherCurrentLiabilities = (decimal)balanceS.BsPasotr2,
                                                TotalNonCurrentLiabilities = (decimal)balanceS.BsLarpla2 + (decimal)balanceS.BsCorotr2,
                                                LLongTerm = (decimal)balanceS.BsLarpla2,
                                                LOtherNonCurrentLiabilities = (decimal)balanceS.BsCorotr2,
                                                TotalPatrimony = (decimal)balanceS.BsTotpat2,
                                                PCapital = (decimal)balanceS.BsCapita2,
                                                PStockPile = (decimal)balanceS.BsReser2,
                                                PUtilities = (decimal)balanceS.BsUtili2,
                                                POther = (decimal)balanceS.BsPatOtr2,
                                                TotalLiabilitiesPatrimony = (decimal)balanceS.BsTotpas2,
                                                LiquidityRatio = (decimal)balanceS.BsTotcor2 / (decimal)balanceS.BsTotcrr2,
                                                DebtRatio = ((decimal)balanceS.BsTotpat2 / (decimal)balanceS.BsTotcrr2) * 100,
                                                ProfitabilityRatio = ((decimal)balanceS.BsUtiper2 / (decimal)balanceS.BsVentas2) * 100,
                                                WorkingCapital = (decimal)balanceS.BsTotcor2 - (decimal)balanceS.BsTotcrr2
                                            });
                                        }
                                        catch(Exception EX)
                                        {
                                            continue;
                                        }
                                    }
                                    if (balanceS.BsFecbal3 != null && balanceS.BsFecbal3 != "")
                                    {
                                        try
                                        {
                                            var insertBalance = await _financialBalanceDomain.AddAsync(new FinancialBalance
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                Date = StaticFunctions.VerifyDate(balanceS.BsFecbal3),
                                                BalanceType = "SITUACIONAL",
                                                BalanceTypeEng = balanceS.BsTipBal3Ing,
                                                Duration = balanceS.BsTimbal3,
                                                DurationEng = balanceS.BsTimBal3Ing,
                                                IdCurrency = balanceS.BsMoneda3 == "USD" ? 1 : balanceS.BsMoneda3 == "PEN" ? 31 :
                                          balanceS.BsMoneda3 == "UYU" ? 154 : balanceS.BsMoneda3 == "MXN" ? 15 :
                                          balanceS.BsMoneda3 == "PAB" ? 120 : balanceS.BsMoneda3 == "DOP" ? 126 :
                                          balanceS.BsMoneda3 == "GTQ" ? 80 : balanceS.BsMoneda3 == "COP" ? 63 :
                                          balanceS.BsMoneda3 == "BOB" ? 51 : balanceS.BsMoneda3 == "ARS" ? 38 :
                                          balanceS.BsMoneda3 == "CRC" ? 66 : balanceS.BsMoneda3 == "PYG" ? 122 :
                                          balanceS.BsMoneda3 == "CLP" ? 29 : balanceS.BsMoneda3 == "BRL" ? 20 :
                                          balanceS.BsMoneda3 == "HNL" ? 84 : balanceS.BsMoneda3 == "NIO" ? 115 :
                                          balanceS.BsMoneda3 == "JMD" ? 93 : balanceS.BsMoneda3 == "MYR" ? 105 :
                                          balanceS.BsMoneda3 == "EUR" ? 2 : balanceS.BsMoneda3 == "COL" ? 63 :
                                          balanceS.BsMoneda3 == "VND" ? 156 : balanceS.BsMoneda3 == "GYD" ? 82 :
                                          balanceS.BsMoneda3 == "UDS" ? 1 : balanceS.BsMoneda3 == "BBD" ? 44 :
                                          balanceS.BsMoneda3 == "BZD" ? 46 : balanceS.BsMoneda3 == "GBP" ? 4 :
                                          balanceS.BsMoneda3 == "TTD" ? 148 : balanceS.BsMoneda3 == "KYD" ? 88 :
                                          balanceS.BsMoneda3 == "INR" ? 16 : balanceS.BsMoneda3 == "PKR" ? 119 :
                                          balanceS.BsMoneda3 == "BSD" ? 42 : balanceS.BsMoneda3 == "SRD" ? 146 :
                                          balanceS.BsMoneda3 == "TRY" ? 19 : balanceS.BsMoneda3 == "SAR" ? 37 :
                                          balanceS.BsMoneda3 == "CNY" ? 8 : balanceS.BsMoneda3 == "XCD" ? 36 :
                                          balanceS.BsMoneda3 == "HUF" ? 26 : balanceS.BsMoneda3 == "AWG" ? 40 :
                                          balanceS.BsMoneda3 == "CHF" ? 7 : balanceS.BsMoneda3 == "TWD" ? 21 :
                                          balanceS.BsMoneda3 == "DKK" ? 22 : balanceS.BsMoneda3 == "ANG" ? 69 :
                                          balanceS.BsMoneda3 == "PLN" ? 23 : balanceS.BsMoneda3 == "MNX" ? 15 : null,
                                                ExchangeRate = (decimal)balanceS.BsTipcam3,
                                                Sales = (decimal)balanceS.BsVentas3,
                                                Utilities = (decimal)balanceS.BsUtiper3,
                                                TotalAssets = (decimal)balanceS.BsTotact3,
                                                TotalCurrentAssets = (decimal)balanceS.BsTotcor3,
                                                ACashBoxBank = (decimal)balanceS.BsCajban3,
                                                AToCollect = (decimal)balanceS.BsPorcob3,
                                                AInventory = (decimal)balanceS.BsInvent3,
                                                AOtherCurrentAssets = (decimal)balanceS.BsActCorOtr3,
                                                TotalNonCurrentAssets = (decimal)balanceS.BsFijo3 + (decimal)balanceS.BsActotr3,
                                                AFixed = (decimal)balanceS.BsFijo3,
                                                AOtherNonCurrentAssets = (decimal)balanceS.BsActotr3,
                                                TotalLliabilities = (decimal)balanceS.BsTotcrr3 + (decimal)balanceS.BsLarpla3 + (decimal)balanceS.BsCorotr3,
                                                TotalCurrentLiabilities = (decimal)balanceS.BsTotcrr3,
                                                LCashBoxBank = (decimal)balanceS.BsBanpro3,
                                                LOtherCurrentLiabilities = (decimal)balanceS.BsPasotr3,
                                                TotalNonCurrentLiabilities = (decimal)balanceS.BsLarpla3 + (decimal)balanceS.BsCorotr3,
                                                LLongTerm = (decimal)balanceS.BsLarpla3,
                                                LOtherNonCurrentLiabilities = (decimal)balanceS.BsCorotr3,
                                                TotalPatrimony = (decimal)balanceS.BsTotpat3,
                                                PCapital = (decimal)balanceS.BsCapita3,
                                                PStockPile = (decimal)balanceS.BsReser3,
                                                PUtilities = (decimal)balanceS.BsUtili3,
                                                POther = (decimal)balanceS.BsPatOtr3,
                                                TotalLiabilitiesPatrimony = (decimal)balanceS.BsTotpas3,
                                                LiquidityRatio = (decimal)balanceS.BsTotcor3 / (decimal)balanceS.BsTotcrr3,
                                                DebtRatio = ((decimal)balanceS.BsTotpat3 / (decimal)balanceS.BsTotcrr3) * 100,
                                                ProfitabilityRatio = ((decimal)balanceS.BsUtiper3 / (decimal)balanceS.BsVentas3) * 100,
                                                WorkingCapital = (decimal)balanceS.BsTotcor3 - (decimal)balanceS.BsTotcrr3
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }

                                }

                                //historico de ventas
                                var historico = await _mempresaDomain.GetmEmpresaHistVentByCodigoAsync(empresa.EmCodigo);
                                if (historico.Count > 0)
                                {
                                    foreach (var item in historico)
                                    {
                                        if (item != null)
                                        {
                                            try
                                            {
                                                var insertHistorico = await _financialSalesHistoryDomain.AddAsync(new SalesHistory
                                                {
                                                    Id = 0,
                                                    IdCompany = inserted,
                                                    Date = StaticFunctions.VerifyDate(item.VeFecha),
                                                    IdCurrency = item.PaiMone == "USD020" ? 1 : item.PaiMone == "PEN015" ? 31 : item.PaiMone == "USD007" ? 1 :
                                                item.PaiMone == "USD008" ? 1 : item.PaiMone == "PEN015" ? 31 :
                                                item.PaiMone == "USD207" ? 1 : item.PaiMone == "USD016" ? 1 :
                                                item.PaiMone == "USD213" ? 1 : item.PaiMone == "USD207" ? 1 :
                                                item.PaiMone == "UYU019" ? 154 : item.PaiMone == "MXN039" ? 15 :
                                                item.PaiMone == "PAB013" ? 120 : item.PaiMone == "DOP017" ? 126 :
                                                item.PaiMone == "GTQ009" ? 80 : item.PaiMone == "COP004" ? 63 :
                                                item.PaiMone == "BOB002" ? 51 : item.PaiMone == "ARS001" ? 38 :
                                                item.PaiMone == "CRC005" ? 66 : item.PaiMone == "PYG014" ? 122 :
                                                item.PaiMone == "CLP006" ? 29 : item.PaiMone == "BRL003" ? 20 :
                                                item.PaiMone == "HNL010" ? 84 : item.PaiMone == "NIO012" ? 115 :
                                                item.PaiMone == "JMD034" ? 93 : item.PaiMone == "MYR" ? 105 :
                                                item.PaiMone == "EUR197" ? 2 : item.PaiMone == "COP004" ? 63 :
                                                item.PaiMone == "EUR024" ? 2 : item.PaiMone == "COL" ? 63 :
                                                item.PaiMone == "EUR041" ? 2 : item.PaiMone == "EUR048" ? 2 :
                                                item.PaiMone == "EUR023" ? 2 : item.PaiMone == "EUR052" ? 2 :
                                                item.PaiMone == "EUR025" ? 2 : item.PaiMone == "EUR219" ? 2 :
                                                item.PaiMone == "EUR036" ? 2 : item.PaiMone == "EUR045" ? 2 :
                                                item.PaiMone == "EUR068" ? 2 : item.PaiMone == "EUR068" ? 2 :
                                                item.PaiMone == "EUR068" ? 2 : item.PaiMone == "EUR068" ? 2 :
                                                item.PaiMone == "GYD025" ? 82 : item.PaiMone == "BBD038" ? 44 :
                                                item.PaiMone == "TTD018" ? 148 : item.PaiMone == "KYD026" ? 88 :
                                                item.PaiMone == "INR086" ? 16 : item.PaiMone == "BSD033" ? 42 :
                                                item.PaiMone == "SRD058" ? 146 : item.PaiMone == "TRY093" ? 19 :
                                                item.PaiMone == "CNY061" ? 8 : item.PaiMone == "XCD056" ? 36 :
                                                item.PaiMone == "XCD049" ? 36 : item.PaiMone == "XCD047" ? 36 :
                                                item.PaiMone == "XCD218" ? 36 : item.PaiMone == "XCD063" ? 36 :
                                                item.PaiMone == "HUF" ? 26 : item.PaiMone == "AWG043" ? 40 :
                                                item.PaiMone == "CHF083" ? 7 : item.PaiMone == "ANG027" ? 69 :
                                                item.PaiMone == "ANG057" ? 69 : item.PaiMone == "ANG081" ? 69 : null,
                                                    Amount = (decimal)item.VeVentas,
                                                    ExchangeRate = (decimal)item.VeTipcam,
                                                    EquivalentToDollars = (decimal)item.VeVentas / (decimal)item.VeTipcam,

                                                });
                                            }
                                            catch(Exception ex)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                //importaciones 

                                var imports = await _mempresaDomain.GetmEmpresaImpByCodigoAsync(empresa.EmCodigo);
                                if (imports.Count > 0)
                                {
                                    foreach (var imp in imports)
                                    {
                                        try
                                        {
                                            var insertImpo = await _importsAndExportsDomain.AddAsync(new ImportsAndExport
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                Type = "I",
                                                Year = imp.EiAno,
                                                Amount = imp.EiMonto
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                //exportaciones 

                                var exports = await _mempresaDomain.GetmEmpresaExpByCodigoAsync(empresa.EmCodigo);
                                if (exports.Count > 0)
                                {
                                    foreach (var exp in exports)
                                    {
                                        try
                                        {
                                            var insertExpo = await _importsAndExportsDomain.AddAsync(new ImportsAndExport
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                Type = "E",
                                                Year = exp.ExAno,
                                                Amount = exp.ExMonto
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                //proveedores

                                var providers = await _mempresaDomain.GetmEmpresaProvByCodigoAsync(empresa.EmCodigo);
                                if (providers.Count > 0)
                                {
                                    foreach (var item in providers)
                                    {
                                        try
                                        {
                                            var insertProv = await _providerDomain.AddAsync(new Provider
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                IdCountry = item.PaiCodigo == "001" ? 11 : item.PaiCodigo == "002" ? 29 : item.PaiCodigo == "003" ? 34 :
                                               item.PaiCodigo == "004" ? 54 : item.PaiCodigo == "005" ? 57 : item.PaiCodigo == "006" ? 49 :
                                               item.PaiCodigo == "007" ? 70 : item.PaiCodigo == "008" ? 72 : item.PaiCodigo == "009" ? 100 :
                                               item.PaiCodigo == "010" ? 108 : item.PaiCodigo == "012" ? 168 : item.PaiCodigo == "013" ? 179 :
                                               item.PaiCodigo == "014" ? 181 : item.PaiCodigo == "015" ? 182 : item.PaiCodigo == "016" ? 187 :
                                               item.PaiCodigo == "017" ? 69 : item.PaiCodigo == "018" ? 237 : item.PaiCodigo == "019" ? 250 :
                                               item.PaiCodigo == "020" ? 249 : item.PaiCodigo == "021" ? 253 : item.PaiCodigo == "022" ? 105 :
                                               item.PaiCodigo == "023" ? 147 : item.PaiCodigo == "024" ? 98 : item.PaiCodigo == "025" ? 104 :
                                               item.PaiCodigo == "026" ? 46 : item.PaiCodigo == "027" ? 60 : item.PaiCodigo == "029" ? 256 :
                                               item.PaiCodigo == "030" ? 255 : item.PaiCodigo == "031" ? 43 : item.PaiCodigo == "032" ? 25 :
                                               item.PaiCodigo == "033" ? 18 : item.PaiCodigo == "034" ? 120 : item.PaiCodigo == "035" ? 183 :
                                               item.PaiCodigo == "036" ? 92 : item.PaiCodigo == "037" ? 15 : item.PaiCodigo == "038" ? 21 :
                                               item.PaiCodigo == "039" ? 151 : item.PaiCodigo == "040" ? 59 : item.PaiCodigo == "041" ? 220 :
                                               item.PaiCodigo == "042" ? 186 : item.PaiCodigo == "043" ? 13 : item.PaiCodigo == "044" ? 16 :
                                               item.PaiCodigo == "045" ? 24 : item.PaiCodigo == "046" ? 27 : item.PaiCodigo == "047" ? 68 :
                                               item.PaiCodigo == "048" ? 84 : item.PaiCodigo == "049" ? 97 : item.PaiCodigo == "064" ? 123 :
                                               item.PaiCodigo == "051" ? 109 : item.PaiCodigo == "052" ? 119 : item.PaiCodigo == "053" ? 121 :
                                               item.PaiCodigo == "054" ? 218 : item.PaiCodigo == "055" ? 196 : item.PaiCodigo == "056" ? 197 :
                                               item.PaiCodigo == "057" ? 198 : item.PaiCodigo == "058" ? 224 : item.PaiCodigo == "059" ? 8 :
                                               item.PaiCodigo == "060" ? 149 : item.PaiCodigo == "061" ? 50 : item.PaiCodigo == "062" ? 229 :
                                               item.PaiCodigo == "063" ? 10 : item.PaiCodigo == "065" ? 65 : item.PaiCodigo == "066" ? 239 :
                                               item.PaiCodigo == "067" ? 205 : item.PaiCodigo == "068" ? 83 : item.PaiCodigo == "069" ? 175 :
                                               item.PaiCodigo == "070" ? 62 : item.PaiCodigo == "071" ? 191 : item.PaiCodigo == "072" ? 245 :
                                               item.PaiCodigo == "073" ? 247 : item.PaiCodigo == "074" ? 200 : item.PaiCodigo == "076" ? 156 :
                                               item.PaiCodigo == "078" ? 194 : item.PaiCodigo == "080" ? 241 : item.PaiCodigo == "081" ? 265 :
                                               item.PaiCodigo == "079" ? 264 : item.PaiCodigo == "083" ? 227 : item.PaiCodigo == "084" ? 226 :
                                               item.PaiCodigo == "085" ? 131 : item.PaiCodigo == "086" ? 112 : item.PaiCodigo == "087" ? 118 :
                                               item.PaiCodigo == "088" ? 185 : item.PaiCodigo == "089" ? 137 : item.PaiCodigo == "090" ? 165 :
                                               item.PaiCodigo == "091" ? 94 : item.PaiCodigo == "092" ? 142 : item.PaiCodigo == "093" ? 243 :
                                               item.PaiCodigo == "095" ? 246 : item.PaiCodigo == "096" ? 124 : item.PaiCodigo == "097" ? 4 :
                                               item.PaiCodigo == "099" ? 91 : item.PaiCodigo == "100" ? 95 : item.PaiCodigo == "101" ? 266 :
                                               item.PaiCodigo == "102" ? 210 : item.PaiCodigo == "103" ? 136 : item.PaiCodigo == "104" ? 177 :
                                               item.PaiCodigo == "105" ? 7 : item.PaiCodigo == "106" ? 26 : item.PaiCodigo == "107" ? 32 :
                                               item.PaiCodigo == "108" ? 38 : item.PaiCodigo == "109" ? 39 : item.PaiCodigo == "110" ? 42 :
                                               item.PaiCodigo == "111" ? 47 : item.PaiCodigo == "113" ? 48 : item.PaiCodigo == "114" ? 55 :
                                               item.PaiCodigo == "115" ? 267 : item.PaiCodigo == "116" ? 71 : item.PaiCodigo == "117" ? 102 :
                                               item.PaiCodigo == "118" ? 75 : item.PaiCodigo == "119" ? 78 : item.PaiCodigo == "120" ? 88 :
                                               item.PaiCodigo == "121" ? 90 : item.PaiCodigo == "122" ? 93 : item.PaiCodigo == "123" ? 103 :
                                               item.PaiCodigo == "124" ? 125 : item.PaiCodigo == "125" ? 134 : item.PaiCodigo == "126" ? 135 :
                                               item.PaiCodigo == "127" ? 140 : item.PaiCodigo == "128" ? 141 : item.PaiCodigo == "129" ? 144 :
                                               item.PaiCodigo == "130" ? 148 : item.PaiCodigo == "132" ? 157 : item.PaiCodigo == "133" ? 158 :
                                               item.PaiCodigo == "134" ? 160 : item.PaiCodigo == "135" ? 168 : item.PaiCodigo == "136" ? 192 :
                                               item.PaiCodigo == "137" ? 259 : item.PaiCodigo == "139" ? 206 : item.PaiCodigo == "140" ? 209 :
                                               item.PaiCodigo == "141" ? 215 : item.PaiCodigo == "142" ? 223 : item.PaiCodigo == "143" ? 77 :
                                               item.PaiCodigo == "145" ? 234 : item.PaiCodigo == "147" ? 244 : item.PaiCodigo == "148" ? 268 :
                                               item.PaiCodigo == "149" ? 261 : item.PaiCodigo == "150" ? 262 : item.PaiCodigo == "152" ? 1 :
                                               item.PaiCodigo == "153" ? 12 : item.PaiCodigo == "154" ? 17 : item.PaiCodigo == "155" ? 19 :
                                               item.PaiCodigo == "156" ? 20 : item.PaiCodigo == "157" ? 28 : item.PaiCodigo == "158" ? 36 :
                                               item.PaiCodigo == "159" ? 281 : item.PaiCodigo == "160" ? 41 : item.PaiCodigo == "161" ? 61 :
                                               item.PaiCodigo == "162" ? 113 : item.PaiCodigo == "163" ? 114 : item.PaiCodigo == "164" ? 115 :
                                               item.PaiCodigo == "166" ? 129 : item.PaiCodigo == "167" ? 128 : item.PaiCodigo == "168" ? 130 :
                                               item.PaiCodigo == "169" ? 154 : item.PaiCodigo == "170" ? 162 : item.PaiCodigo == "171" ? 176 :
                                               item.PaiCodigo == "172" ? 222 : item.PaiCodigo == "173" ? 188 : item.PaiCodigo == "174" ? 204 :
                                               item.PaiCodigo == "175" ? 221 : item.PaiCodigo == "176" ? 228 : item.PaiCodigo == "177" ? 230 :
                                               item.PaiCodigo == "178" ? 232 : item.PaiCodigo == "179" ? 240 : item.PaiCodigo == "181" ? 251 :
                                               item.PaiCodigo == "182" ? 254 : item.PaiCodigo == "183" ? 260 : item.PaiCodigo == "185" ? 3 :
                                               item.PaiCodigo == "186" ? 6 : item.PaiCodigo == "187" ? 31 : item.PaiCodigo == "188" ? 37 :
                                               item.PaiCodigo == "189" ? 23 : item.PaiCodigo == "190" ? 58 : item.PaiCodigo == "191" ? 76 :
                                               item.PaiCodigo == "192" ? 80 : item.PaiCodigo == "193" ? 110 : item.PaiCodigo == "194" ? 111 :
                                               item.PaiCodigo == "195" ? 116 : item.PaiCodigo == "197" ? 138 : item.PaiCodigo == "198" ? 172 :
                                               item.PaiCodigo == "199" ? 145 : item.PaiCodigo == "200" ? 152 : item.PaiCodigo == "201" ? 153 :
                                               item.PaiCodigo == "202" ? 190 : item.PaiCodigo == "203" ? 202 : item.PaiCodigo == "204" ? 155 :
                                               item.PaiCodigo == "205" ? 212 : item.PaiCodigo == "206" ? 213 : item.PaiCodigo == "208" ? 214 :
                                               item.PaiCodigo == "209" ? 252 : item.PaiCodigo == "210" ? 82 : item.PaiCodigo == "211" ? 161 :
                                               item.PaiCodigo == "212" ? 146 : item.PaiCodigo == "213" ? 99 : item.PaiCodigo == "214" ? 201 :
                                               item.PaiCodigo == "215" ? 178 : item.PaiCodigo == "216" ? 236 : item.PaiCodigo == "217" ? 86 :
                                               item.PaiCodigo == "221" ? 171 : item.PaiCodigo == "222" ? 282 : item.PaiCodigo == "219" ? 85 :
                                               item.PaiCodigo == "224" ? 117 : item.PaiCodigo == "220" ? 143 : item.PaiCodigo == "225" ? 139 :
                                               item.PaiCodigo == "011" ? 169 : item.PaiCodigo == "028" ? 164 : item.PaiCodigo == "207" ? 283 :
                                               item.PaiCodigo == "218" ? 284 : item.PaiCodigo == "223" ? 285 : item.PaiCodigo == "226" ? 63 :
                                               item.PaiCodigo == "227" ? 180 : item.PaiCodigo == "228" ? 286 : item.PaiCodigo == "229" ? 143 :
                                               item.PaiCodigo == "230" ? 208 : item.PaiCodigo == "231" ? 64 : item.PaiCodigo == "232" ? 263 :
                                               item.PaiCodigo == "233" ? 60 : item.PaiCodigo == "234" ? 30 : item.PaiCodigo == "235" ? 217 :
                                               item.PaiCodigo == "236" ? 231 : item.PaiCodigo == "237" ? 30 : item.PaiCodigo == "238" ? 30 :
                                               item.PaiCodigo == "239" ? 18 : item.PaiCodigo == "240" ? 207 : item.PaiCodigo == "241" ? 155 : null,
                                                Name = item.ProvNombre,
                                                Qualification = item.CumCodigo == "02" ? "Puntual" : item.CumCodigo == "03" ? "Lento Eventual" :
                                             item.CumCodigo == "04" ? "Lento Siempre" : item.CumCodigo == "05" ? "Moroso" :
                                              item.CumCodigo == "06" ? "Sin Experiencia" : item.CumCodigo == "01" ? "" : null,
                                                QualificationEng = item.CumCodigo == "02" ? "Prompt" : item.CumCodigo == "03" ? "Sometimes delayed" :
                                             item.CumCodigo == "04" ? "Always delayed" : item.CumCodigo == "05" ? "Delinquent" :
                                              item.CumCodigo == "06" ? "No experience" : item.CumCodigo == "01" ? "" : null,
                                                Date = item.ProvFecha,
                                                Telephone = item.ProvTelefo,
                                                AttendedBy = item.ProvAtendio,
                                                IdCurrency = item.ProvMnLiCr == "US$" ? 1 : item.ProvMnLiCr == "MN" ? 31 : item.ProvMnLiCr == "EUR" ? 2 : null,
                                                MaximumAmount = item.ProvLinCre,
                                                MaximumAmountEng = item.ProvLinCreIng,
                                                TimeLimit = item.ProvPlazos,
                                                TimeLimitEng = item.ProvPlazosIng,
                                                Compliance = item.ProvCumple,
                                                ClientSince = item.ProvTiempo,
                                                ClientSinceEng = item.ProvTiempoIng,
                                                ProductsTheySell = item.ProvVenden,
                                                ProductsTheySellEng = item.ProvVendenIng,
                                                AdditionalCommentary = item.ProvComen,
                                                AdditionalCommentaryEng = item.ProvComenIng,
                                                ReferentCommentary = item.ProvTexto,

                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }
                                }
                                //morosidad comercial
                                var morComercial = await _mempresaDomain.GetmEmpresaMorComByCodigoAsync(empresa.EmCodigo);
                                if (morComercial.Count > 0)
                                {
                                    foreach (var item in morComercial)
                                    {
                                        try
                                        {
                                            var insertMorCom = await _comercialLatePaymentDomain.AddAsync(new ComercialLatePayment
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                CreditorOrSupplier = item.PaGirador,
                                                DocumentType = item.PaTitulo,
                                                DocumentTypeEng = item.PaTituloIng,
                                                Date = item.PaFecpro,
                                                AmountNc = (decimal)item.PaMonmn,
                                                AmountFc = (decimal)item.PaMonme,
                                                PendingPaymentDate = item.PaFecreg,
                                                DaysLate = item.PaDiaatr,
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }
                                }
                                //endeudamiento bancario
                                var endBancario = await _mempresaDomain.GetmEmpresaEndBancByCodigoAsync(empresa.EmCodigo);
                                if (endBancario.Count > 0)
                                {
                                    foreach (var item in endBancario)
                                    {
                                        try
                                        {
                                            var insertMorCom = await _bankDebtDomain.AddAsync(new BankDebt
                                            {
                                                Id = 0,
                                                IdCompany = inserted,
                                                BankName = item.SbdNombre,
                                                Qualification = item.SbdCalifi,
                                                QualificationEng = item.SbdCalifiIng,
                                                DebtNc = (decimal)item.SbdMonto,
                                                DebtFc = (decimal)item.SbdMonMe,
                                                Memo = item.SbdMemo,
                                                MemoEng = item.SbdMemoIng,
                                            });
                                        }
                                        catch(Exception ex)
                                        {
                                            continue;
                                        }
                                    }
                                }


                            }
                        }
                        else
                        {
                            _logger.LogError("Error empresa :" + empresa.EmCodigo);
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error empresa :" + empresa.EmCodigo+ " : "+ex.Message);
                        continue;
                    }
                }
            }
            
            return true;
        }

        private int? ObtenerCodigoPais(string? PaiCodigo)
        {
            return PaiCodigo == "001" ? 11 : PaiCodigo == "002" ? 29 : PaiCodigo == "003" ? 34 :
            PaiCodigo == "004" ? 54 : PaiCodigo == "005" ? 57 : PaiCodigo == "006" ? 49 :
            PaiCodigo == "007" ? 70 : PaiCodigo == "008" ? 72 : PaiCodigo == "009" ? 100 :
            PaiCodigo == "010" ? 108 : PaiCodigo == "012" ? 168 : PaiCodigo == "013" ? 179 :
            PaiCodigo == "014" ? 181 : PaiCodigo == "015" ? 182 : PaiCodigo == "016" ? 187 :
            PaiCodigo == "017" ? 69 : PaiCodigo == "018" ? 237 : PaiCodigo == "019" ? 250 :
            PaiCodigo == "020" ? 249 : PaiCodigo == "021" ? 253 : PaiCodigo == "022" ? 105 :
            PaiCodigo == "023" ? 147 : PaiCodigo == "024" ? 98 : PaiCodigo == "025" ? 104 :
            PaiCodigo == "026" ? 46 : PaiCodigo == "027" ? 60 : PaiCodigo == "029" ? 256 :
            PaiCodigo == "030" ? 255 : PaiCodigo == "031" ? 43 : PaiCodigo == "032" ? 25 :
            PaiCodigo == "033" ? 18 : PaiCodigo == "034" ? 120 : PaiCodigo == "035" ? 183 :
            PaiCodigo == "036" ? 92 : PaiCodigo == "037" ? 15 : PaiCodigo == "038" ? 21 :
            PaiCodigo == "039" ? 151 : PaiCodigo == "040" ? 59 : PaiCodigo == "041" ? 220 :
            PaiCodigo == "042" ? 186 : PaiCodigo == "043" ? 13 : PaiCodigo == "044" ? 16 :
            PaiCodigo == "045" ? 24 : PaiCodigo == "046" ? 27 : PaiCodigo == "047" ? 68 :
            PaiCodigo == "048" ? 84 : PaiCodigo == "049" ? 97 : PaiCodigo == "064" ? 123 :
            PaiCodigo == "051" ? 109 : PaiCodigo == "052" ? 119 : PaiCodigo == "053" ? 121 :
            PaiCodigo == "054" ? 218 : PaiCodigo == "055" ? 196 : PaiCodigo == "056" ? 197 :
            PaiCodigo == "057" ? 198 : PaiCodigo == "058" ? 224 : PaiCodigo == "059" ? 8 :
            PaiCodigo == "060" ? 149 : PaiCodigo == "061" ? 50 : PaiCodigo == "062" ? 229 :
            PaiCodigo == "063" ? 10 : PaiCodigo == "065" ? 65 : PaiCodigo == "066" ? 239 :
            PaiCodigo == "067" ? 205 : PaiCodigo == "068" ? 83 : PaiCodigo == "069" ? 175 :
            PaiCodigo == "070" ? 62 : PaiCodigo == "071" ? 191 : PaiCodigo == "072" ? 245 :
            PaiCodigo == "073" ? 247 : PaiCodigo == "074" ? 200 : PaiCodigo == "076" ? 156 :
            PaiCodigo == "078" ? 194 : PaiCodigo == "080" ? 241 : PaiCodigo == "081" ? 265 :
            PaiCodigo == "079" ? 264 : PaiCodigo == "083" ? 227 : PaiCodigo == "084" ? 226 :
            PaiCodigo == "085" ? 131 : PaiCodigo == "086" ? 112 : PaiCodigo == "087" ? 118 :
            PaiCodigo == "088" ? 185 : PaiCodigo == "089" ? 137 : PaiCodigo == "090" ? 165 :
            PaiCodigo == "091" ? 94 : PaiCodigo == "092" ? 142 : PaiCodigo == "093" ? 243 :
            PaiCodigo == "095" ? 246 : PaiCodigo == "096" ? 124 : PaiCodigo == "097" ? 4 :
            PaiCodigo == "099" ? 91 : PaiCodigo == "100" ? 95 : PaiCodigo == "101" ? 266 :
            PaiCodigo == "102" ? 210 : PaiCodigo == "103" ? 136 : PaiCodigo == "104" ? 177 :
            PaiCodigo == "105" ? 7 : PaiCodigo == "106" ? 26 : PaiCodigo == "107" ? 32 :
            PaiCodigo == "108" ? 38 : PaiCodigo == "109" ? 39 : PaiCodigo == "110" ? 42 :
            PaiCodigo == "111" ? 47 : PaiCodigo == "113" ? 48 : PaiCodigo == "114" ? 55 :
            PaiCodigo == "115" ? 267 : PaiCodigo == "116" ? 71 : PaiCodigo == "117" ? 102 :
            PaiCodigo == "118" ? 75 : PaiCodigo == "119" ? 78 : PaiCodigo == "120" ? 88 :
            PaiCodigo == "121" ? 90 : PaiCodigo == "122" ? 93 : PaiCodigo == "123" ? 103 :
            PaiCodigo == "124" ? 125 : PaiCodigo == "125" ? 134 : PaiCodigo == "126" ? 135 :
            PaiCodigo == "127" ? 140 : PaiCodigo == "128" ? 141 : PaiCodigo == "129" ? 144 :
            PaiCodigo == "130" ? 148 : PaiCodigo == "132" ? 157 : PaiCodigo == "133" ? 158 :
            PaiCodigo == "134" ? 160 : PaiCodigo == "135" ? 168 : PaiCodigo == "136" ? 192 :
            PaiCodigo == "137" ? 259 : PaiCodigo == "139" ? 206 : PaiCodigo == "140" ? 209 :
            PaiCodigo == "141" ? 215 : PaiCodigo == "142" ? 223 : PaiCodigo == "143" ? 77 :
            PaiCodigo == "145" ? 234 : PaiCodigo == "147" ? 244 : PaiCodigo == "148" ? 268 :
            PaiCodigo == "149" ? 261 : PaiCodigo == "150" ? 262 : PaiCodigo == "152" ? 1 :
            PaiCodigo == "153" ? 12 : PaiCodigo == "154" ? 17 : PaiCodigo == "155" ? 19 :
            PaiCodigo == "156" ? 20 : PaiCodigo == "157" ? 28 : PaiCodigo == "158" ? 36 :
            PaiCodigo == "159" ? 281 : PaiCodigo == "160" ? 41 : PaiCodigo == "161" ? 61 :
            PaiCodigo == "162" ? 113 : PaiCodigo == "163" ? 114 : PaiCodigo == "164" ? 115 :
            PaiCodigo == "166" ? 129 : PaiCodigo == "167" ? 128 : PaiCodigo == "168" ? 130 :
            PaiCodigo == "169" ? 154 : PaiCodigo == "170" ? 162 : PaiCodigo == "171" ? 176 :
            PaiCodigo == "172" ? 222 : PaiCodigo == "173" ? 188 : PaiCodigo == "174" ? 204 :
            PaiCodigo == "175" ? 221 : PaiCodigo == "176" ? 228 : PaiCodigo == "177" ? 230 :
            PaiCodigo == "178" ? 232 : PaiCodigo == "179" ? 240 : PaiCodigo == "181" ? 251 :
            PaiCodigo == "182" ? 254 : PaiCodigo == "183" ? 260 : PaiCodigo == "185" ? 3 :
            PaiCodigo == "186" ? 6 : PaiCodigo == "187" ? 31 : PaiCodigo == "188" ? 37 :
            PaiCodigo == "189" ? 23 : PaiCodigo == "190" ? 58 : PaiCodigo == "191" ? 76 :
            PaiCodigo == "192" ? 80 : PaiCodigo == "193" ? 110 : PaiCodigo == "194" ? 111 :
            PaiCodigo == "195" ? 116 : PaiCodigo == "197" ? 138 : PaiCodigo == "198" ? 172 :
            PaiCodigo == "199" ? 145 : PaiCodigo == "200" ? 152 : PaiCodigo == "201" ? 153 :
            PaiCodigo == "202" ? 190 : PaiCodigo == "203" ? 202 : PaiCodigo == "204" ? 155 :
            PaiCodigo == "205" ? 212 : PaiCodigo == "206" ? 213 : PaiCodigo == "208" ? 214 :
            PaiCodigo == "209" ? 252 : PaiCodigo == "210" ? 82 : PaiCodigo == "211" ? 161 :
            PaiCodigo == "212" ? 146 : PaiCodigo == "213" ? 99 : PaiCodigo == "214" ? 201 :
            PaiCodigo == "215" ? 178 : PaiCodigo == "216" ? 236 : PaiCodigo == "217" ? 86 :
            PaiCodigo == "221" ? 171 : PaiCodigo == "222" ? 282 : PaiCodigo == "219" ? 85 :
            PaiCodigo == "224" ? 117 : PaiCodigo == "220" ? 143 : PaiCodigo == "225" ? 139 :
            PaiCodigo == "011" ? 169 : PaiCodigo == "028" ? 164 : PaiCodigo == "207" ? 283 :
            PaiCodigo == "218" ? 284 : PaiCodigo == "223" ? 285 : PaiCodigo == "226" ? 63 :
            PaiCodigo == "227" ? 180 : PaiCodigo == "228" ? 286 : PaiCodigo == "229" ? 143 :
            PaiCodigo == "230" ? 208 : PaiCodigo == "231" ? 64 : PaiCodigo == "232" ? 263 :
            PaiCodigo == "233" ? 60 : PaiCodigo == "234" ? 30 : PaiCodigo == "235" ? 217 :
            PaiCodigo == "236" ? 231 : PaiCodigo == "237" ? 30 : PaiCodigo == "238" ? 30 :
            PaiCodigo == "239" ? 18 : PaiCodigo == "240" ? 207 : PaiCodigo == "241" ? 155 : null;
        }

        private int? ObtenerPersoneriaLegal(string? JuCodigo)
        {
            return JuCodigo == "000" ? 376 : JuCodigo == "001" ? 280 :
            JuCodigo == "002" ? 288 : JuCodigo == "003" ? 289 : JuCodigo == "005" ? 290 :
            JuCodigo == "006" ? 291 : JuCodigo == "007" ? 292 : JuCodigo == "009" ? 294 :
            JuCodigo == "010" ? 297 : JuCodigo == "011" ? 298 : JuCodigo == "012" ? 299 :
            JuCodigo == "013" ? 302 : JuCodigo == "014" ? 301 : JuCodigo == "016" ? 305 :
            JuCodigo == "017" ? 306 : JuCodigo == "018" ? 307 : JuCodigo == "020" ? 314 :
            JuCodigo == "022" ? 310 : JuCodigo == "023" ? 322 : JuCodigo == "024" ? 329 :
            JuCodigo == "025" ? 325 : JuCodigo == "026" ? 326 : JuCodigo == "027" ? 327 :
            JuCodigo == "028" ? 328 : JuCodigo == "030" ? 342 : JuCodigo == "031" ? 343 :
            JuCodigo == "032" ? 346 : JuCodigo == "033" ? 347 : JuCodigo == "034" ? 349 :
            JuCodigo == "035" ? 350 : JuCodigo == "037" ? 352 : JuCodigo == "039" ? 353 :
            JuCodigo == "040" ? 354 : JuCodigo == "041" ? 355 : JuCodigo == "043" ? 358 :
            JuCodigo == "044" ? 360 : JuCodigo == "045" ? 362 : JuCodigo == "046" ? 364 :
            JuCodigo == "047" ? 371 : JuCodigo == "048" ? 373 : JuCodigo == "049" ? 374 :
            JuCodigo == "050" ? 377 : JuCodigo == "051" ? 381 : JuCodigo == "052" ? 382 :
            JuCodigo == "053" ? 383 : JuCodigo == "055" ? 384 : JuCodigo == "056" ? 385 :
            JuCodigo == "058" ? 391 : JuCodigo == "059" ? 396 : JuCodigo == "060" ? 397 :
            JuCodigo == "061" ? 406 : JuCodigo == "062" ? 408 : JuCodigo == "063" ? 409 :
            JuCodigo == "064" ? 411 : JuCodigo == "065" ? 414 : JuCodigo == "067" ? 286 :
            JuCodigo == "068" ? 413 : JuCodigo == "069" ? 293 : JuCodigo == "070" ? 361 :
            JuCodigo == "072" ? 339 : JuCodigo == "074" ? 304 : JuCodigo == "075" ? 300 :
            JuCodigo == "076" ? 379 : JuCodigo == "077" ? 283 : JuCodigo == "078" ? 394 :
            JuCodigo == "079" ? 285 : JuCodigo == "080" ? 407 : JuCodigo == "081" ? 336 :
            JuCodigo == "082" ? 296 : JuCodigo == "083" ? 303 : JuCodigo == "084" ? 281 :
            JuCodigo == "085" ? 395 : JuCodigo == "086" ? 405 : JuCodigo == "087" ? 393 :
            JuCodigo == "088" ? 334 : JuCodigo == "089" ? 333 : JuCodigo == "090" ? 375 :
            JuCodigo == "091" ? 388 : JuCodigo == "092" ? 311 : JuCodigo == "093" ? 351 :
            JuCodigo == "095" ? 324 : JuCodigo == "096" ? 357 : JuCodigo == "097" ? 338 :
            JuCodigo == "098" ? 282 : JuCodigo == "099" ? 378 : JuCodigo == "100" ? 348 :
            JuCodigo == "101" ? 404 : JuCodigo == "102" ? 320 : JuCodigo == "103" ? 368 :
            JuCodigo == "104" ? 380 : JuCodigo == "105" ? 369 : JuCodigo == "106" ? 359 :
            JuCodigo == "107" ? 389 : JuCodigo == "108" ? 315 : JuCodigo == "109" ? 370 :
            JuCodigo == "110" ? 392 : JuCodigo == "111" ? 284 : JuCodigo == "112" ? 366 :
            JuCodigo == "113" ? 335 : JuCodigo == "114" ? 402 : JuCodigo == "115" ? 321 :
            JuCodigo == "116" ? 416 : JuCodigo == "117" ? 417 : JuCodigo == "118" ? 337 :
            JuCodigo == "119" ? 400 : JuCodigo == "120" ? 390 : JuCodigo == "121" ? 365 :
            JuCodigo == "122" ? 367 : JuCodigo == "123" ? 403 : JuCodigo == "124" ? 345 :
            JuCodigo == "125" ? 331 : JuCodigo == "126" ? 410 : JuCodigo == "127" ? 312 :
            JuCodigo == "128" ? 363 : JuCodigo == "129" ? 412 : JuCodigo == "130" ? 317 :
            JuCodigo == "131" ? 341 : JuCodigo == "132" ? 330 : JuCodigo == "133" ? 287 :
            JuCodigo == "134" ? 344 : JuCodigo == "135" ? 279 : JuCodigo == "136" ? 313 :
            JuCodigo == "137" ? 318 : JuCodigo == "138" ? 356 : JuCodigo == "139" ? 316 :
            JuCodigo == "140" ? 323 : JuCodigo == "141" ? 401 : JuCodigo == "142" ? 399 :
            JuCodigo == "143" ? 332 : JuCodigo == "144" ? 340 : JuCodigo == "145" ? 372 :
            JuCodigo == "146" ? 387 : JuCodigo == "147" ? 309 : JuCodigo == "148" ? 415 :
            JuCodigo == "149" ? 319 : JuCodigo == "150" ? 295 : JuCodigo == "151" ? 398 :
            JuCodigo == "152" ? 308 : JuCodigo == "153" ? 386 : null;
        }

        private string ObtenerCalidad(string? CalCodigo)
        {
            return CalCodigo == "1" ? "A" : CalCodigo == "2" ? "A" : CalCodigo == "3" ? "B" :
            CalCodigo == "4" ? "B" : CalCodigo == "5" ? "C" : CalCodigo == "6" ? "C" :
            CalCodigo == "7" ? "D" : "";
        }

        private int ObtenerReputacion(string RcCodigo)
        {
            return  RcCodigo == "EAD" ? 1 :  RcCodigo == "ECC" ? 25 : RcCodigo == "EDJ" ? 33 :
                    RcCodigo == "EJA" ? 34 : RcCodigo == "ELQ" ? 39 : RcCodigo == "EMO" ? 42 :
                    RcCodigo == "ENC" ? 4 :  RcCodigo == "ERC" ? 49 : RcCodigo == "EXX" ? 2 :
                    RcCodigo == "PA1" ? 20 : RcCodigo == "PCC" ? 10 : RcCodigo == "PDJ" ? 11 :
                    RcCodigo == "PEF" ? 12 : RcCodigo == "PRE" ? 13 : RcCodigo == "PEX" ? 14 :
                    RcCodigo == "PQL" ? 24 : RcCodigo == "PIT" ? 26 : RcCodigo == "PNC" ? 17 :
                    RcCodigo == "PNN" ? 18 : RcCodigo == "PTC" ? 19 : RcCodigo == "PVC" ? 22 :
                    RcCodigo == "PBC" ? 21 : RcCodigo == "PNX" ? 29 : RcCodigo == "EBC" ? 6 :
                    RcCodigo == "ERN" ? 58 : RcCodigo == "PXX" ? 15 : RcCodigo == "ENR" ? 9 :
                    RcCodigo == "ELD" ? 37 : RcCodigo == "PLB" ? 16 : RcCodigo == "EDI" ? 3 :
                    RcCodigo == "PRP" ? 31 : RcCodigo == "EAE" ? 32 : RcCodigo == "EMR" ? 87 :
                    RcCodigo == "ETF" ? 23 : RcCodigo == "PMR" ? 46 : RcCodigo == "PTF" ? 47 :
                    RcCodigo == "ELC" ? 8 :  RcCodigo == "PLC" ? 38 : RcCodigo == "ETR" ? 88 :
                    RcCodigo == "PTR" ? 55 : RcCodigo == "PVP" ? 63 : RcCodigo == "PAS" ? 64 :
                    RcCodigo == "PBS" ? 65 : RcCodigo == "PCS" ? 66 : RcCodigo == "PDS" ? 70 :
                    RcCodigo == "PSS" ? 71 : RcCodigo == "PRD" ? 48 : RcCodigo == "EAA" ? 7 :
                    RcCodigo == "PSC" ? 50 : RcCodigo == "PBR" ? 51 : RcCodigo == "PIA" ? 52 :
                    RcCodigo == "PDC" ? 53 : RcCodigo == "PDM" ? 54 : RcCodigo == "PRM" ? 35 :
                    RcCodigo == "PHE" ? 56 : RcCodigo == "PCP" ? 57 : RcCodigo == "ERD" ? 28 :
                    RcCodigo == "ENT" ? 27 : RcCodigo == "ERF" ? 30 : RcCodigo == "EBM" ? 5 :
                    RcCodigo == "EMN" ? 59 : RcCodigo == "PCT" ? 36 : RcCodigo == "PMC" ? 40 :
                    RcCodigo == "PCV" ? 41 : RcCodigo == "PGA" ? 43 : RcCodigo == "EMP" ? 60 :
                    RcCodigo == "EMB" ? 61 : RcCodigo == "ENX" ? 62 : RcCodigo == "PIR" ? 44 :
                    RcCodigo == "PDF" ? 45 : RcCodigo == "PDZ" ? 72 : RcCodigo == "PPJ" ? 73 :
                    RcCodigo == "PMZ" ? 74 : RcCodigo == "PRS" ? 75 : RcCodigo == "ESC" ? 67 :
                    RcCodigo == "ESO" ? 68 : RcCodigo == "ECP" ? 69 : RcCodigo == "ECN" ? 76 :
                    RcCodigo == "EQC" ? 77 : RcCodigo == "EQD" ? 78 : RcCodigo == "EQO" ? 79 :
                    RcCodigo == "EAR" ? 80 : RcCodigo == "EFS" ? 81 : RcCodigo == "ETP" ? 82 :
                    RcCodigo == "ETC" ? 83 : RcCodigo == "EAQ" ? 84 : RcCodigo == "ETO" ? 85 :
                    RcCodigo == "EDF" ? 86 : 0; 
        }

        public async Task<bool> MigratePerson()
        {
            var personas = await _impersonaDomain.GetNotMigratedPersona();
            foreach (var persona in personas)
            {
                var reputacion = await _impersonaDomain.GetmPersonaReputacionByCodigoAsync(persona.PeCodigo);
                int idReputacion = 0;
                if (reputacion != null)
                {
                    idReputacion = reputacion.RcCodigo == "EAD" ? 1 : reputacion.RcCodigo == "ECC" ? 25 : reputacion.RcCodigo == "EDJ" ? 33 :
                        reputacion.RcCodigo == "EJA" ? 34 : reputacion.RcCodigo == "ELQ" ? 39 : reputacion.RcCodigo == "EMO" ? 42 :
                        reputacion.RcCodigo == "ENC" ? 4 : reputacion.RcCodigo == "ERC" ? 49 : reputacion.RcCodigo == "EXX" ? 2 :
                        reputacion.RcCodigo == "PA1" ? 20 : reputacion.RcCodigo == "PCC" ? 10 : reputacion.RcCodigo == "PDJ" ? 11 :
                        reputacion.RcCodigo == "PEF" ? 12 : reputacion.RcCodigo == "PRE" ? 13 : reputacion.RcCodigo == "PEX" ? 14 :
                        reputacion.RcCodigo == "PQL" ? 24 : reputacion.RcCodigo == "PIT" ? 26 : reputacion.RcCodigo == "PNC" ? 17 :
                        reputacion.RcCodigo == "PNN" ? 18 : reputacion.RcCodigo == "PTC" ? 19 : reputacion.RcCodigo == "PVC" ? 22 :
                        reputacion.RcCodigo == "PBC" ? 21 : reputacion.RcCodigo == "PNX" ? 29 : reputacion.RcCodigo == "EBC" ? 6 :
                        reputacion.RcCodigo == "ERN" ? 58 : reputacion.RcCodigo == "PXX" ? 15 : reputacion.RcCodigo == "ENR" ? 9 :
                        reputacion.RcCodigo == "ELD" ? 37 : reputacion.RcCodigo == "PLB" ? 16 : reputacion.RcCodigo == "EDI" ? 3 :
                        reputacion.RcCodigo == "PRP" ? 31 : reputacion.RcCodigo == "EAE" ? 32 : reputacion.RcCodigo == "EMR" ? 87 :
                        reputacion.RcCodigo == "ETF" ? 23 : reputacion.RcCodigo == "PMR" ? 46 : reputacion.RcCodigo == "PTF" ? 47 :
                        reputacion.RcCodigo == "ELC" ? 8 : reputacion.RcCodigo == "PLC" ? 38 : reputacion.RcCodigo == "ETR" ? 88 :
                        reputacion.RcCodigo == "PTR" ? 55 : reputacion.RcCodigo == "PVP" ? 63 : reputacion.RcCodigo == "PAS" ? 64 :
                        reputacion.RcCodigo == "PBS" ? 65 : reputacion.RcCodigo == "PCS" ? 66 : reputacion.RcCodigo == "PDS" ? 70 :
                        reputacion.RcCodigo == "PSS" ? 71 : reputacion.RcCodigo == "PRD" ? 48 : reputacion.RcCodigo == "EAA" ? 7 :
                        reputacion.RcCodigo == "PSC" ? 50 : reputacion.RcCodigo == "PBR" ? 51 : reputacion.RcCodigo == "PIA" ? 52 :
                        reputacion.RcCodigo == "PDC" ? 53 : reputacion.RcCodigo == "PDM" ? 54 : reputacion.RcCodigo == "PRM" ? 35 :
                        reputacion.RcCodigo == "PHE" ? 56 : reputacion.RcCodigo == "PCP" ? 57 : reputacion.RcCodigo == "ERD" ? 28 :
                        reputacion.RcCodigo == "ENT" ? 27 : reputacion.RcCodigo == "ERF" ? 30 : reputacion.RcCodigo == "EBM" ? 5 :
                        reputacion.RcCodigo == "EMN" ? 59 : reputacion.RcCodigo == "PCT" ? 36 : reputacion.RcCodigo == "PMC" ? 40 :
                        reputacion.RcCodigo == "PCV" ? 41 : reputacion.RcCodigo == "PGA" ? 43 : reputacion.RcCodigo == "EMP" ? 60 :
                        reputacion.RcCodigo == "EMB" ? 61 : reputacion.RcCodigo == "ENX" ? 62 : reputacion.RcCodigo == "PIR" ? 44 :
                        reputacion.RcCodigo == "PDF" ? 45 : reputacion.RcCodigo == "PDZ" ? 72 : reputacion.RcCodigo == "PPJ" ? 73 :
                        reputacion.RcCodigo == "PMZ" ? 74 : reputacion.RcCodigo == "PRS" ? 75 : reputacion.RcCodigo == "ESC" ? 67 :
                        reputacion.RcCodigo == "ESO" ? 68 : reputacion.RcCodigo == "ECP" ? 69 : reputacion.RcCodigo == "ECN" ? 76 :
                        reputacion.RcCodigo == "EQC" ? 77 : reputacion.RcCodigo == "EQD" ? 78 : reputacion.RcCodigo == "EQO" ? 79 :
                        reputacion.RcCodigo == "EAR" ? 80 : reputacion.RcCodigo == "EFS" ? 81 : reputacion.RcCodigo == "ETP" ? 82 :
                        reputacion.RcCodigo == "ETC" ? 83 : reputacion.RcCodigo == "EAQ" ? 84 : reputacion.RcCodigo == "ETO" ? 85 :
                        reputacion.RcCodigo == "EDF" ? 86 : 0;
                }
                try
                {
                    var inserted = await _personDomain.AddPersonAsync(new Person
                    {
                        Id = 0,
                        OldCode = persona.PeCodigo,
                        Fullname = persona.PeNombre,
                        LastSearched = persona.PeFecinf,
                        Language = Dictionary.LanguageMigra[persona.IdiCodigo.Value],
                        Nationality = persona.PeNacion,
                        BirthDate = persona.PeFecnac,
                        BirthPlace = persona.PeLugnac,
                        IdDocumentType = persona.TiCodigo == "CExt" ? 2 : persona.TiCodigo == "C.I." ? 6 :
                         persona.TiCodigo == "C.C." ? 7 : persona.TiCodigo == "CPF/MF" ? 9 :
                         persona.TiCodigo == "CURP" ? 10 : persona.TiCodigo == "D.I." ? 11 :
                         persona.TiCodigo == "DNI" ? 1 : persona.TiCodigo == "DPI" ? 12 :
                         persona.TiCodigo == "DUI" ? 13 : persona.TiCodigo == "IDEH" ? 14 :
                         persona.TiCodigo == "LE" ? 15 : persona.TiCodigo == "LC" ? 16 :
                         persona.TiCodigo == "Pas." ? 4 : persona.TiCodigo == "RUT" ? 20 :
                         persona.TiCodigo == "S.S." ? 17 : persona.TiCodigo == "RUN" ? 18 :
                         persona.TiCodigo == "T.I." ? 19 : persona.TiCodigo == "DIM" ? 21 : null,
                        CodeDocumentType = persona.PeDocide,
                        TaxTypeCode = persona.PeRegtri,
                        IdPersonSituation = persona.EsCodigo == "01" ? null : persona.EsCodigo == "02" ? 1 :
                        persona.EsCodigo == "05" ? 2 : persona.EsCodigo == "03" ? 3 :
                        persona.EsCodigo == "04" ? 4 : persona.EsCodigo == "06" ? 5 :
                        persona.EsCodigo == "07" ? 6 : null,
                        IdLegalRegisterSituation = persona.ErCodigo == "" ? null : persona.ErCodigo == "AC" ? 1 :
                        persona.ErCodigo == "BP" ? 4 : persona.ErCodigo == "BD" ? 3 :
                        persona.ErCodigo == "ST" ? 16 : persona.ErCodigo == "NL" ? 13 :
                        persona.ErCodigo == "BO" ? 2 : persona.ErCodigo == "IN" ? 10 : null,
                        Address = persona.PeDirecc,
                        Cp = persona.PeCodpos,
                        City = persona.PeCiudad,
                        OtherDirecctions = persona.PeDireccCome,
                        TradeName = persona.PeNombreCome,
                        IdCountry = persona.PaiCodigo == "001" ? 11 : persona.PaiCodigo == "002" ? 29 : persona.PaiCodigo == "003" ? 34 :
                        persona.PaiCodigo == "004" ? 54  : persona.PaiCodigo == "005" ? 57  : persona.PaiCodigo == "006" ? 49 :
                        persona.PaiCodigo == "007" ? 70  : persona.PaiCodigo == "008" ? 72  : persona.PaiCodigo == "009" ? 100 :
                        persona.PaiCodigo == "010" ? 108 : persona.PaiCodigo == "012" ? 168 : persona.PaiCodigo == "013" ? 179 :
                        persona.PaiCodigo == "014" ? 181 : persona.PaiCodigo == "015" ? 182 : persona.PaiCodigo == "016" ? 187 :
                        persona.PaiCodigo == "017" ? 69  : persona.PaiCodigo == "018" ? 237 : persona.PaiCodigo == "019" ? 250 :
                        persona.PaiCodigo == "020" ? 249 : persona.PaiCodigo == "021" ? 253 : persona.PaiCodigo == "022" ? 105 :
                        persona.PaiCodigo == "023" ? 147 : persona.PaiCodigo == "024" ? 98  : persona.PaiCodigo == "025" ? 104 :
                        persona.PaiCodigo == "026" ? 46  : persona.PaiCodigo == "027" ? 60  : persona.PaiCodigo == "029" ? 256 :
                        persona.PaiCodigo == "030" ? 255 : persona.PaiCodigo == "031" ? 43  : persona.PaiCodigo == "032" ? 25 :
                        persona.PaiCodigo == "033" ? 18  : persona.PaiCodigo == "034" ? 120 : persona.PaiCodigo == "035" ? 183 :
                        persona.PaiCodigo == "036" ? 92  : persona.PaiCodigo == "037" ? 15  : persona.PaiCodigo == "038" ? 21 :
                        persona.PaiCodigo == "039" ? 151 : persona.PaiCodigo == "040" ? 59  : persona.PaiCodigo == "041" ? 220 :
                        persona.PaiCodigo == "042" ? 186 : persona.PaiCodigo == "043" ? 13  : persona.PaiCodigo == "044" ? 16 :
                        persona.PaiCodigo == "045" ? 24  : persona.PaiCodigo == "046" ? 27  : persona.PaiCodigo == "047" ? 68 :
                        persona.PaiCodigo == "048" ? 84  : persona.PaiCodigo == "049" ? 97  : persona.PaiCodigo == "064" ? 123 :
                        persona.PaiCodigo == "051" ? 109 : persona.PaiCodigo == "052" ? 119 : persona.PaiCodigo == "053" ? 121 :
                        persona.PaiCodigo == "054" ? 218 : persona.PaiCodigo == "055" ? 196 : persona.PaiCodigo == "056" ? 197 :
                        persona.PaiCodigo == "057" ? 198 : persona.PaiCodigo == "058" ? 224 : persona.PaiCodigo == "059" ? 8 :
                        persona.PaiCodigo == "060" ? 149 : persona.PaiCodigo == "061" ? 50  : persona.PaiCodigo == "062" ? 229 :
                        persona.PaiCodigo == "063" ? 10  : persona.PaiCodigo == "065" ? 65  : persona.PaiCodigo == "066" ? 239 :
                        persona.PaiCodigo == "067" ? 205 : persona.PaiCodigo == "068" ? 83  : persona.PaiCodigo == "069" ? 175 :
                        persona.PaiCodigo == "070" ? 62  : persona.PaiCodigo == "071" ? 191 : persona.PaiCodigo == "072" ? 245 :
                        persona.PaiCodigo == "073" ? 247 : persona.PaiCodigo == "074" ? 200 : persona.PaiCodigo == "076" ? 156 :
                        persona.PaiCodigo == "078" ? 194 : persona.PaiCodigo == "080" ? 241 : persona.PaiCodigo == "081" ? 265 :
                        persona.PaiCodigo == "079" ? 264 : persona.PaiCodigo == "083" ? 227 : persona.PaiCodigo == "084" ? 226 :
                        persona.PaiCodigo == "085" ? 131 : persona.PaiCodigo == "086" ? 112 : persona.PaiCodigo == "087" ? 118 :
                        persona.PaiCodigo == "088" ? 185 : persona.PaiCodigo == "089" ? 137 : persona.PaiCodigo == "090" ? 165 :
                        persona.PaiCodigo == "091" ? 94  : persona.PaiCodigo == "092" ? 142 : persona.PaiCodigo == "093" ? 243 :
                        persona.PaiCodigo == "095" ? 246 : persona.PaiCodigo == "096" ? 124 : persona.PaiCodigo == "097" ? 4 :
                        persona.PaiCodigo == "099" ? 91  : persona.PaiCodigo == "100" ? 95  : persona.PaiCodigo == "101" ? 266 :
                        persona.PaiCodigo == "102" ? 210 : persona.PaiCodigo == "103" ? 136 : persona.PaiCodigo == "104" ? 177 :
                        persona.PaiCodigo == "105" ? 7   : persona.PaiCodigo == "106" ? 26  : persona.PaiCodigo == "107" ? 32 :
                        persona.PaiCodigo == "108" ? 38  : persona.PaiCodigo == "109" ? 39  : persona.PaiCodigo == "110" ? 42 :
                        persona.PaiCodigo == "111" ? 47  : persona.PaiCodigo == "113" ? 48  : persona.PaiCodigo == "114" ? 55 :
                        persona.PaiCodigo == "115" ? 267 : persona.PaiCodigo == "116" ? 71  : persona.PaiCodigo == "117" ? 102 :
                        persona.PaiCodigo == "118" ? 75  : persona.PaiCodigo == "119" ? 78  : persona.PaiCodigo == "120" ? 88 :
                        persona.PaiCodigo == "121" ? 90  : persona.PaiCodigo == "122" ? 93  : persona.PaiCodigo == "123" ? 103 :
                        persona.PaiCodigo == "124" ? 125 : persona.PaiCodigo == "125" ? 134 : persona.PaiCodigo == "126" ? 135 :
                        persona.PaiCodigo == "127" ? 140 : persona.PaiCodigo == "128" ? 141 : persona.PaiCodigo == "129" ? 144 :
                        persona.PaiCodigo == "130" ? 148 : persona.PaiCodigo == "132" ? 157 : persona.PaiCodigo == "133" ? 158 :
                        persona.PaiCodigo == "134" ? 160 : persona.PaiCodigo == "135" ? 168 : persona.PaiCodigo == "136" ? 192 :
                        persona.PaiCodigo == "137" ? 259 : persona.PaiCodigo == "139" ? 206 : persona.PaiCodigo == "140" ? 209 :
                        persona.PaiCodigo == "141" ? 215 : persona.PaiCodigo == "142" ? 223 : persona.PaiCodigo == "143" ? 77 :
                        persona.PaiCodigo == "145" ? 234 : persona.PaiCodigo == "147" ? 244 : persona.PaiCodigo == "148" ? 268 :
                        persona.PaiCodigo == "149" ? 261 : persona.PaiCodigo == "150" ? 262 : persona.PaiCodigo == "152" ? 1 :
                        persona.PaiCodigo == "153" ? 12  : persona.PaiCodigo == "154" ? 17  : persona.PaiCodigo == "155" ? 19 :
                        persona.PaiCodigo == "156" ? 20  : persona.PaiCodigo == "157" ? 28  : persona.PaiCodigo == "158" ? 36 :
                        persona.PaiCodigo == "159" ? 281 : persona.PaiCodigo == "160" ? 41  : persona.PaiCodigo == "161" ? 61 :
                        persona.PaiCodigo == "162" ? 113 : persona.PaiCodigo == "163" ? 114 : persona.PaiCodigo == "164" ? 115 :
                        persona.PaiCodigo == "166" ? 129 : persona.PaiCodigo == "167" ? 128 : persona.PaiCodigo == "168" ? 130 :
                        persona.PaiCodigo == "169" ? 154 : persona.PaiCodigo == "170" ? 162 : persona.PaiCodigo == "171" ? 176 :
                        persona.PaiCodigo == "172" ? 222 : persona.PaiCodigo == "173" ? 188 : persona.PaiCodigo == "174" ? 204 :
                        persona.PaiCodigo == "175" ? 221 : persona.PaiCodigo == "176" ? 228 : persona.PaiCodigo == "177" ? 230 :
                        persona.PaiCodigo == "178" ? 232 : persona.PaiCodigo == "179" ? 240 : persona.PaiCodigo == "181" ? 251 :
                        persona.PaiCodigo == "182" ? 254 : persona.PaiCodigo == "183" ? 260 : persona.PaiCodigo == "185" ? 3 :
                        persona.PaiCodigo == "186" ? 6   : persona.PaiCodigo == "187" ? 31  : persona.PaiCodigo == "188" ? 37 :
                        persona.PaiCodigo == "189" ? 23  : persona.PaiCodigo == "190" ? 58  : persona.PaiCodigo == "191" ? 76 :
                        persona.PaiCodigo == "192" ? 80  : persona.PaiCodigo == "193" ? 110 : persona.PaiCodigo == "194" ? 111 :
                        persona.PaiCodigo == "195" ? 116 : persona.PaiCodigo == "197" ? 138 : persona.PaiCodigo == "198" ? 172 :
                        persona.PaiCodigo == "199" ? 145 : persona.PaiCodigo == "200" ? 152 : persona.PaiCodigo == "201" ? 153 :
                        persona.PaiCodigo == "202" ? 190 : persona.PaiCodigo == "203" ? 202 : persona.PaiCodigo == "204" ? 155 :
                        persona.PaiCodigo == "205" ? 212 : persona.PaiCodigo == "206" ? 213 : persona.PaiCodigo == "208" ? 214 :
                        persona.PaiCodigo == "209" ? 252 : persona.PaiCodigo == "210" ? 82  : persona.PaiCodigo == "211" ? 161 :
                        persona.PaiCodigo == "212" ? 146 : persona.PaiCodigo == "213" ? 99  : persona.PaiCodigo == "214" ? 201 :
                        persona.PaiCodigo == "215" ? 178 : persona.PaiCodigo == "216" ? 236 : persona.PaiCodigo == "217" ? 86 :
                        persona.PaiCodigo == "221" ? 171 : persona.PaiCodigo == "222" ? 282 : persona.PaiCodigo == "219" ? 85 :
                        persona.PaiCodigo == "224" ? 117 : persona.PaiCodigo == "220" ? 143 : persona.PaiCodigo == "225" ? 139 :
                        persona.PaiCodigo == "011" ? 169 : persona.PaiCodigo == "028" ? 164 : persona.PaiCodigo == "207" ? 283 :
                        persona.PaiCodigo == "218" ? 284 : persona.PaiCodigo == "223" ? 285 : persona.PaiCodigo == "226" ? 63 :
                        persona.PaiCodigo == "227" ? 180 : persona.PaiCodigo == "228" ? 286 : persona.PaiCodigo == "229" ? 143 :
                        persona.PaiCodigo == "230" ? 208 : persona.PaiCodigo == "231" ? 64  : persona.PaiCodigo == "232" ? 263 :
                        persona.PaiCodigo == "233" ? 60  : persona.PaiCodigo == "234" ? 30  : persona.PaiCodigo == "235" ? 217 :
                        persona.PaiCodigo == "236" ? 231 : persona.PaiCodigo == "237" ? 30  : persona.PaiCodigo == "238" ? 30 :
                        persona.PaiCodigo == "239" ? 18  : persona.PaiCodigo == "240" ? 207 : persona.PaiCodigo == "241" ? 155 : null,
                        CodePhone = persona.PePrftlf,
                        NumberPhone = persona.PeTelefo,
                        IdCivilStatus = persona.EcCodigo == "01" ? 5 : persona.EcCodigo == "02" ? 2 :
                        persona.EcCodigo == "03" ? 1 : persona.EcCodigo == "04" ? 4 :
                        persona.EcCodigo == "05" ? 6 : persona.EcCodigo == "06" ? 3 : null,
                        RelationshipWith = persona.PeRelciv,
                        RelationshipDocumentType = string.IsNullOrEmpty(persona.PeRelcivDni) ? 1 : null,
                        RelationshipCodeDocument = persona.PeRelcivDni,
                        FatherName = persona.PePadre,
                        MotherName = persona.PeMadre,
                        Email = persona.PeEmail,
                        Cellphone = persona.PeCelula,
                        ClubMember = persona.PeClub,
                        Insurance = persona.PeAsegur,
                        NewsCommentary = persona.PePrensasel,
                        PrintNewsCommentary = persona.PeLogpre == 1 ? true : false,
                        ReputationCommentary = persona.PeComRep,
                        IdCreditRisk = persona.CrCodigo == "0005" ? 1 : persona.CrCodigo == "0000" ? 2 : persona.CrCodigo == "0001" ? 3 :
                       persona.CrCodigo == "0002" ? 4 : persona.CrCodigo == "0003" ? 5 : persona.CrCodigo == "0011" ? 6 : persona.CrCodigo == "0004" ? 7 : null,
                        IdPaymentPolicy = persona.PaCodigo == "01" ? 8 : persona.PaCodigo == "02" ? 9 : persona.PaCodigo == "03" ? 10 : persona.PaCodigo == "04" ? 11 :
                       persona.PaCodigo == "05" ? 12 : persona.PaCodigo == "06" ? 13 : persona.PaCodigo == "07" ? 14 : null,
                        IdReputation = idReputacion != 0 ? idReputacion : null,
                        Profession = persona.PfNombre,
                    });
                    if(inserted != 0)
                    {
                        using var context = new SqlCoreContext();
                        var listTraductionPerson = new List<Traduction>();
                        listTraductionPerson.AddRange(await context.Traductions.Where(x => x.IdPerson == inserted && x.Identifier.Contains("_P_")).ToListAsync());
                        foreach (var item in listTraductionPerson)
                        {
                            if (item.Identifier == "S_P_NACIONALITY")
                            {
                                if (persona.PeNacionIng != null)
                                {
                                    item.ShortValue= persona.PeNacionIng;
                                }
                            }
                            else if (item.Identifier == "S_P_BIRTHDATE")
                            {
                                if (persona.PeFecnacIng != null)
                                {
                                    item.ShortValue = persona.PeFecnacIng;
                                }
                            }
                            else if (item.Identifier == "S_P_MARRIEDTO")
                            {
                                if (persona.PeRelcivIng != null)
                                {
                                    item.ShortValue = persona.PeRelcivIng;
                                }
                            }
                            else if (item.Identifier == "S_P_PROFESSION")
                            {
                                if (persona.PfNombreIng != null)
                                {
                                    item.ShortValue = persona.PfNombreIng;
                                }
                            }
                            else if (item.Identifier == "L_P_NEWSCOMM")
                            {
                                if (persona.PePrensaselIng != null)
                                {
                                    item.LargeValue = persona.PePrensaselIng;
                                }
                            }
                            else if (item.Identifier == "L_P_REPUTATION")
                            {
                                if (persona.PeComRepIng != null)
                                {
                                    item.LargeValue = persona.PeComRepIng;
                                }
                            }
                        }
                        await _impersonaDomain.MigratePersona(persona.PeCodigo);
                        var pers = await _personDomain.GetByIdAsync(inserted);
                        pers.Traductions = listTraductionPerson;
                        var ins = await _personDomain.UpdateAsync(pers);
                        if (ins == true)
                        {

                        }
                        await _mempresaDomain.MigrateEmpresa(persona.PeCodigo);

                        //domicilio
                        var listTraductionPersonDom = new List<Traduction>();
                        listTraductionPersonDom.AddRange(await context.Traductions.Where(x => x.IdPerson == inserted && x.Identifier.Contains("_D_")).ToListAsync());
                        foreach (var item in listTraductionPersonDom)
                        {
                            if (item.Identifier == "S_D_VALUE")
                            {
                                if (persona.PeValdomIng != null)
                                {
                                    item.ShortValue = persona.PeValdomIng;
                                }
                            }
                            else if (item.Identifier == "L_D_RESIDENCE")
                            {
                                if (persona.PeCondocIng != null)
                                {
                                    item.LargeValue = persona.PeCondocIng;
                                }
                            }
                        }
                        var insertedDomicilio = await _personHomeDomain.AddPersonHomeAsync(new PersonHome
                        {
                            Id = 0,
                            IdPerson = inserted,
                            OwnHome = persona.PeTipdom == "" ? null : persona.PeTipdom == "Si" ? true : false,
                            Value = persona.PeValdom,
                            HomeCommentary = persona.PeCondoc
                        }, listTraductionPersonDom);
                        if (insertedDomicilio != 0)
                        {
                        }
                        else
                        {
                            Console.WriteLine("Error en persona domicilio :" + inserted);
                        }
                        //centro de trabajo
                        var trabajo = await _impersonaDomain.GetmPersonaTrabajoByCodigoAsync(persona.PeCodigo);
                        if(trabajo != null)
                        {
                            int idCompany = 0;
                            var emp = await _companyDomain.GetByOldCode(trabajo.EmCodigo);
                            if(emp != null)
                            {
                                idCompany = emp.Id;
                            }
                            var listTraductionPersonTrab = new List<Traduction>();
                            listTraductionPersonTrab.AddRange(await context.Traductions.Where(x => x.IdPerson == inserted && x.Identifier.Contains("_C_")).ToListAsync());
                            foreach (var item in listTraductionPersonTrab)
                            {
                                if (item.Identifier == "S_C_CURJOB")
                                {
                                    if (trabajo.CaNombreIng != null)
                                    {
                                        item.ShortValue = trabajo.CaNombreIng;
                                    }
                                }
                                else if (item.Identifier == "S_C_STARTDT")
                                {
                                    if (trabajo.PtFecingIng != null)
                                    {
                                        item.ShortValue = trabajo.PtFecingIng;
                                    }
                                }
                                else if (item.Identifier == "S_C_ENDDT")
                                {
                                    if (trabajo.PtFeccesIng != null)
                                    {
                                        item.ShortValue = trabajo.PtFeccesIng;
                                    }
                                }
                                else if (item.Identifier == "S_C_INCOME")
                                {
                                    if (trabajo.PtInganuIng != null)
                                    {
                                        item.ShortValue = trabajo.PtInganuIng;
                                    }
                                }
                                else if (item.Identifier == "L_C_DETAILS")
                                {
                                    if (trabajo.PtDetallIng != null)
                                    {
                                        item.LargeValue = trabajo.PtDetallIng;
                                    }
                                }
                            }
                            var insertTrab = await _personJobDomain.AddPersonJobAsync(new PersonJob
                            {
                                Id = 0,
                                IdPerson = inserted,
                                IdCompany = idCompany == 0 ? null : idCompany,
                                CurrentJob = trabajo.CaNombre,
                                StartDate =trabajo.PtFecing,
                                EndDate = trabajo.PtFecces,
                                MonthlyIncome = trabajo.PtEstadi + "",
                                AnnualIncome = trabajo.PtInganu,
                                JobDetails = trabajo.PtDetall
                            }, listTraductionPersonTrab);
                            if (insertTrab != 0) 
                            {
                            }
                            else
                            {
                                Console.WriteLine("Error en persona trabajo :" + inserted); 
                            }
                        }
                        //otras actividades
                        var listTraductionPersonAct = new List<Traduction>();
                        listTraductionPersonAct.AddRange(await context.Traductions.Where(x => x.IdPerson == inserted && x.Identifier.Contains("_A_")).ToListAsync());
                        foreach (var item in listTraductionPersonAct)
                        {
                            if (item.Identifier == "L_A_OTHERACT")
                            {
                                if (persona.PeOtrRecIng != null)
                                {
                                    item.LargeValue = persona.PeOtrRecIng;
                                }
                            }
                        }
                        var insertActiv = await _personActivitiesDomain.AddPersonActivitiesAsync(new PersonActivity
                        {
                            Id = 0,
                            IdPerson = inserted,
                            ActivitiesCommentary = persona.PeOtrrec,
                        }, listTraductionPersonAct);
                        if (insertActiv != 0)
                        {
                        }
                        else
                        {
                            Console.WriteLine("Error en persona actividades :" + inserted);
                        }

                        //propiedades
                        var listTraductionPersonPro = new List<Traduction>();
                        listTraductionPersonPro.AddRange(await context.Traductions.Where(x => x.IdPerson == inserted && x.Identifier.Contains("_PR_")).ToListAsync());
                        foreach (var item in listTraductionPersonPro)
                        {
                            if (item.Identifier == "L_PR_DETAILS")
                            {
                                if (persona.PeCombieIng != null)
                                {
                                    item.LargeValue = persona.PeCombieIng;
                                }
                            }
                        }
                        var insertProp = await _personPropertyDomain.AddPersonPropertiesAsync(new PersonProperty
                        {
                            Id = 0,
                            IdPerson = inserted,
                            PropertiesCommentary = persona.PeCombie,
                        }, listTraductionPersonPro);
                        if (insertProp != 0)
                        {
                        }
                        else
                        {
                            Console.WriteLine("Error en persona propiedades :" + inserted);
                        }

                        //sbs
                        var listTraductionPersonSbs = new List<Traduction>();
                        listTraductionPersonSbs.AddRange(await context.Traductions.Where(x => x.IdPerson == inserted && x.Identifier.Contains("_PR_")).ToListAsync());
                        foreach (var item in listTraductionPersonSbs)
                        {
                            if (item.Identifier == "L_SBS_ANTEC")
                            {
                                if (persona.PeAntcredIng != null)
                                {
                                    item.LargeValue = persona.PeAntcredIng;
                                }
                            }
                            else if (item.Identifier == "L_SBS_RISKCNT")
                            {
                                if (persona.PeCenRieIng != null)
                                {
                                    item.LargeValue = persona.PeCenRieIng;
                                }
                            }
                            else if (item.Identifier == "L_SBS_COMMENTSBS")
                            {
                                if (persona.PeSupbanIng != null)
                                {
                                    item.LargeValue = persona.PeSupbanIng;
                                }
                            }
                            else if (item.Identifier == "L_SBS_COMMENTBANK")
                            {
                                if (persona.PeSubacuIng != null)
                                {
                                    item.LargeValue = persona.PeSubacuIng;
                                }
                            }
                            else if (item.Identifier == "L_SBS_LITIG")
                            {
                                if (persona.PeComlitIng != null)
                                {
                                    item.LargeValue = persona.PeComlitIng;
                                }
                            }
                        }
                        var insertSbs = await _personSBSDomain.AddPersonSBS(new PersonSb
                        {
                            Id = 0,
                            IdPerson = inserted,
                            AditionalCommentaryRiskCenter = persona.PeCenrie,
                            DebtRecordedDate =persona.PeFecreg,
                            ExchangeRate = (decimal)persona.PeTcsbs,
                            BankingCommentary = persona.PeSubacu,
                            ReferentOrAnalyst = persona.PerCodref,
                            Date = StaticFunctions.VerifyDate(persona.PeFecref),
                            LitigationsCommentary = persona.PeComlit,
                            CreditHistoryCommentary = persona.PeAntcred,
                            GuaranteesOfferedNc = (decimal)persona.PeGaomn,
                            GuaranteesOfferedFc = (decimal)persona.PeGaome,
                            SbsCommentary = persona.PeSupban

                        }, listTraductionPersonSbs);
                        if (insertSbs != 0)
                        {
                        }
                        else
                        {
                            Console.WriteLine("Error en persona Sbs :" + inserted);
                        }
                        //proveedores

                        var providers = await _impersonaDomain.GetmPersonaProvByCodigoAsync(persona.PeCodigo);
                        if (providers.Count > 0)
                        {
                            foreach (var item in providers)
                            {
                                var insertProv = await _providerDomain.AddAsync(new Provider
                                {
                                    Id = 0,
                                    IdPerson = inserted,
                                    IdCountry = item.PaiCodigo == "001" ? 11 : item.PaiCodigo == "002" ? 29 : item.PaiCodigo == "003" ? 34 :
                                       item.PaiCodigo == "004" ? 54 : item.PaiCodigo == "005" ? 57 : item.PaiCodigo == "006" ? 49 :
                                       item.PaiCodigo == "007" ? 70 : item.PaiCodigo == "008" ? 72 : item.PaiCodigo == "009" ? 100 :
                                       item.PaiCodigo == "010" ? 108 : item.PaiCodigo == "012" ? 168 : item.PaiCodigo == "013" ? 179 :
                                       item.PaiCodigo == "014" ? 181 : item.PaiCodigo == "015" ? 182 : item.PaiCodigo == "016" ? 187 :
                                       item.PaiCodigo == "017" ? 69 : item.PaiCodigo == "018" ? 237 : item.PaiCodigo == "019" ? 250 :
                                       item.PaiCodigo == "020" ? 249 : item.PaiCodigo == "021" ? 253 : item.PaiCodigo == "022" ? 105 :
                                       item.PaiCodigo == "023" ? 147 : item.PaiCodigo == "024" ? 98 : item.PaiCodigo == "025" ? 104 :
                                       item.PaiCodigo == "026" ? 46 : item.PaiCodigo == "027" ? 60 : item.PaiCodigo == "029" ? 256 :
                                       item.PaiCodigo == "030" ? 255 : item.PaiCodigo == "031" ? 43 : item.PaiCodigo == "032" ? 25 :
                                       item.PaiCodigo == "033" ? 18 : item.PaiCodigo == "034" ? 120 : item.PaiCodigo == "035" ? 183 :
                                       item.PaiCodigo == "036" ? 92 : item.PaiCodigo == "037" ? 15 : item.PaiCodigo == "038" ? 21 :
                                       item.PaiCodigo == "039" ? 151 : item.PaiCodigo == "040" ? 59 : item.PaiCodigo == "041" ? 220 :
                                       item.PaiCodigo == "042" ? 186 : item.PaiCodigo == "043" ? 13 : item.PaiCodigo == "044" ? 16 :
                                       item.PaiCodigo == "045" ? 24 : item.PaiCodigo == "046" ? 27 : item.PaiCodigo == "047" ? 68 :
                                       item.PaiCodigo == "048" ? 84 : item.PaiCodigo == "049" ? 97 : item.PaiCodigo == "064" ? 123 :
                                       item.PaiCodigo == "051" ? 109 : item.PaiCodigo == "052" ? 119 : item.PaiCodigo == "053" ? 121 :
                                       item.PaiCodigo == "054" ? 218 : item.PaiCodigo == "055" ? 196 : item.PaiCodigo == "056" ? 197 :
                                       item.PaiCodigo == "057" ? 198 : item.PaiCodigo == "058" ? 224 : item.PaiCodigo == "059" ? 8 :
                                       item.PaiCodigo == "060" ? 149 : item.PaiCodigo == "061" ? 50 : item.PaiCodigo == "062" ? 229 :
                                       item.PaiCodigo == "063" ? 10 : item.PaiCodigo == "065" ? 65 : item.PaiCodigo == "066" ? 239 :
                                       item.PaiCodigo == "067" ? 205 : item.PaiCodigo == "068" ? 83 : item.PaiCodigo == "069" ? 175 :
                                       item.PaiCodigo == "070" ? 62 : item.PaiCodigo == "071" ? 191 : item.PaiCodigo == "072" ? 245 :
                                       item.PaiCodigo == "073" ? 247 : item.PaiCodigo == "074" ? 200 : item.PaiCodigo == "076" ? 156 :
                                       item.PaiCodigo == "078" ? 194 : item.PaiCodigo == "080" ? 241 : item.PaiCodigo == "081" ? 265 :
                                       item.PaiCodigo == "079" ? 264 : item.PaiCodigo == "083" ? 227 : item.PaiCodigo == "084" ? 226 :
                                       item.PaiCodigo == "085" ? 131 : item.PaiCodigo == "086" ? 112 : item.PaiCodigo == "087" ? 118 :
                                       item.PaiCodigo == "088" ? 185 : item.PaiCodigo == "089" ? 137 : item.PaiCodigo == "090" ? 165 :
                                       item.PaiCodigo == "091" ? 94 : item.PaiCodigo == "092" ? 142 : item.PaiCodigo == "093" ? 243 :
                                       item.PaiCodigo == "095" ? 246 : item.PaiCodigo == "096" ? 124 : item.PaiCodigo == "097" ? 4 :
                                       item.PaiCodigo == "099" ? 91 : item.PaiCodigo == "100" ? 95 : item.PaiCodigo == "101" ? 266 :
                                       item.PaiCodigo == "102" ? 210 : item.PaiCodigo == "103" ? 136 : item.PaiCodigo == "104" ? 177 :
                                       item.PaiCodigo == "105" ? 7 : item.PaiCodigo == "106" ? 26 : item.PaiCodigo == "107" ? 32 :
                                       item.PaiCodigo == "108" ? 38 : item.PaiCodigo == "109" ? 39 : item.PaiCodigo == "110" ? 42 :
                                       item.PaiCodigo == "111" ? 47 : item.PaiCodigo == "113" ? 48 : item.PaiCodigo == "114" ? 55 :
                                       item.PaiCodigo == "115" ? 267 : item.PaiCodigo == "116" ? 71 : item.PaiCodigo == "117" ? 102 :
                                       item.PaiCodigo == "118" ? 75 : item.PaiCodigo == "119" ? 78 : item.PaiCodigo == "120" ? 88 :
                                       item.PaiCodigo == "121" ? 90 : item.PaiCodigo == "122" ? 93 : item.PaiCodigo == "123" ? 103 :
                                       item.PaiCodigo == "124" ? 125 : item.PaiCodigo == "125" ? 134 : item.PaiCodigo == "126" ? 135 :
                                       item.PaiCodigo == "127" ? 140 : item.PaiCodigo == "128" ? 141 : item.PaiCodigo == "129" ? 144 :
                                       item.PaiCodigo == "130" ? 148 : item.PaiCodigo == "132" ? 157 : item.PaiCodigo == "133" ? 158 :
                                       item.PaiCodigo == "134" ? 160 : item.PaiCodigo == "135" ? 168 : item.PaiCodigo == "136" ? 192 :
                                       item.PaiCodigo == "137" ? 259 : item.PaiCodigo == "139" ? 206 : item.PaiCodigo == "140" ? 209 :
                                       item.PaiCodigo == "141" ? 215 : item.PaiCodigo == "142" ? 223 : item.PaiCodigo == "143" ? 77 :
                                       item.PaiCodigo == "145" ? 234 : item.PaiCodigo == "147" ? 244 : item.PaiCodigo == "148" ? 268 :
                                       item.PaiCodigo == "149" ? 261 : item.PaiCodigo == "150" ? 262 : item.PaiCodigo == "152" ? 1 :
                                       item.PaiCodigo == "153" ? 12 : item.PaiCodigo == "154" ? 17 : item.PaiCodigo == "155" ? 19 :
                                       item.PaiCodigo == "156" ? 20 : item.PaiCodigo == "157" ? 28 : item.PaiCodigo == "158" ? 36 :
                                       item.PaiCodigo == "159" ? 281 : item.PaiCodigo == "160" ? 41 : item.PaiCodigo == "161" ? 61 :
                                       item.PaiCodigo == "162" ? 113 : item.PaiCodigo == "163" ? 114 : item.PaiCodigo == "164" ? 115 :
                                       item.PaiCodigo == "166" ? 129 : item.PaiCodigo == "167" ? 128 : item.PaiCodigo == "168" ? 130 :
                                       item.PaiCodigo == "169" ? 154 : item.PaiCodigo == "170" ? 162 : item.PaiCodigo == "171" ? 176 :
                                       item.PaiCodigo == "172" ? 222 : item.PaiCodigo == "173" ? 188 : item.PaiCodigo == "174" ? 204 :
                                       item.PaiCodigo == "175" ? 221 : item.PaiCodigo == "176" ? 228 : item.PaiCodigo == "177" ? 230 :
                                       item.PaiCodigo == "178" ? 232 : item.PaiCodigo == "179" ? 240 : item.PaiCodigo == "181" ? 251 :
                                       item.PaiCodigo == "182" ? 254 : item.PaiCodigo == "183" ? 260 : item.PaiCodigo == "185" ? 3 :
                                       item.PaiCodigo == "186" ? 6 : item.PaiCodigo == "187" ? 31 : item.PaiCodigo == "188" ? 37 :
                                       item.PaiCodigo == "189" ? 23 : item.PaiCodigo == "190" ? 58 : item.PaiCodigo == "191" ? 76 :
                                       item.PaiCodigo == "192" ? 80 : item.PaiCodigo == "193" ? 110 : item.PaiCodigo == "194" ? 111 :
                                       item.PaiCodigo == "195" ? 116 : item.PaiCodigo == "197" ? 138 : item.PaiCodigo == "198" ? 172 :
                                       item.PaiCodigo == "199" ? 145 : item.PaiCodigo == "200" ? 152 : item.PaiCodigo == "201" ? 153 :
                                       item.PaiCodigo == "202" ? 190 : item.PaiCodigo == "203" ? 202 : item.PaiCodigo == "204" ? 155 :
                                       item.PaiCodigo == "205" ? 212 : item.PaiCodigo == "206" ? 213 : item.PaiCodigo == "208" ? 214 :
                                       item.PaiCodigo == "209" ? 252 : item.PaiCodigo == "210" ? 82 : item.PaiCodigo == "211" ? 161 :
                                       item.PaiCodigo == "212" ? 146 : item.PaiCodigo == "213" ? 99 : item.PaiCodigo == "214" ? 201 :
                                       item.PaiCodigo == "215" ? 178 : item.PaiCodigo == "216" ? 236 : item.PaiCodigo == "217" ? 86 :
                                       item.PaiCodigo == "221" ? 171 : item.PaiCodigo == "222" ? 282 : item.PaiCodigo == "219" ? 85 :
                                       item.PaiCodigo == "224" ? 117 : item.PaiCodigo == "220" ? 143 : item.PaiCodigo == "225" ? 139 :
                                       item.PaiCodigo == "011" ? 169 : item.PaiCodigo == "028" ? 164 : item.PaiCodigo == "207" ? 283 :
                                       item.PaiCodigo == "218" ? 284 : item.PaiCodigo == "223" ? 285 : item.PaiCodigo == "226" ? 63 :
                                       item.PaiCodigo == "227" ? 180 : item.PaiCodigo == "228" ? 286 : item.PaiCodigo == "229" ? 143 :
                                       item.PaiCodigo == "230" ? 208 : item.PaiCodigo == "231" ? 64 : item.PaiCodigo == "232" ? 263 :
                                       item.PaiCodigo == "233" ? 60 : item.PaiCodigo == "234" ? 30 : item.PaiCodigo == "235" ? 217 :
                                       item.PaiCodigo == "236" ? 231 : item.PaiCodigo == "237" ? 30 : item.PaiCodigo == "238" ? 30 :
                                       item.PaiCodigo == "239" ? 18 : item.PaiCodigo == "240" ? 207 : item.PaiCodigo == "241" ? 155 : null,
                                    Name = item.ProvNombre,
                                    Qualification = item.CumCodigo == "02" ? "Puntual" : item.CumCodigo == "03" ? "Lento Eventual" :
                                     item.CumCodigo == "04" ? "Lento Siempre" : item.CumCodigo == "05" ? "Moroso" :
                                      item.CumCodigo == "06" ? "Sin Experiencia" : item.CumCodigo == "01" ? "" : null,
                                    QualificationEng = item.CumCodigo == "02" ? "Prompt" : item.CumCodigo == "03" ? "Sometimes delayed" :
                                     item.CumCodigo == "04" ? "Always delayed" : item.CumCodigo == "05" ? "Delinquent" :
                                      item.CumCodigo == "06" ? "No experience" : item.CumCodigo == "01" ? "" : null,
                                    Date = item.ProvFecha,
                                    Telephone = item.ProvTelefo,
                                    AttendedBy = item.ProvAtendio,
                                    IdCurrency = item.ProvMnLiCr == "US$" ? 1 : item.ProvMnLiCr == "MN" ? 31 : item.ProvMnLiCr == "EUR" ? 2 : null,
                                    MaximumAmount = item.ProvLinCre,
                                    MaximumAmountEng = item.ProvLinCreIng,
                                    TimeLimit = item.ProvPlazos,
                                    TimeLimitEng = item.ProvPlazosIng,
                                    Compliance = item.ProvCumple,
                                    ClientSince = item.ProvTiempo,
                                    ClientSinceEng = item.ProvTiempoIng,
                                    ProductsTheySell = item.ProvVenden,
                                    ProductsTheySellEng = item.ProvVendenIng,
                                    AdditionalCommentary = item.ProvComen,
                                    AdditionalCommentaryEng = item.ProvComenIng,
                                    ReferentCommentary = item.ProvTexto,
                                });
                            }
                        }
                        //morosidad comercial
                        var morComercial = await _impersonaDomain.GetmPersonaMorComByCodigoAsync(persona.PeCodigo);
                        if (morComercial.Count > 0)
                        {
                            foreach (var item in morComercial)
                            {
                                var insertMorCom = await _comercialLatePaymentDomain.AddAsync(new ComercialLatePayment
                                {
                                    Id = 0,
                                    IdPerson = inserted,
                                    CreditorOrSupplier = item.PaGirador,
                                    DocumentType = item.PaTitulo,
                                    DocumentTypeEng = item.PaTituloIng,
                                    Date = item.PaFecpro,
                                    AmountNc = (decimal)item.PaMonmn,
                                    AmountFc = (decimal)item.PaMonme,
                                    PendingPaymentDate = item.PaFecreg,
                                    DaysLate = 0,
                                });
                            }
                        }

                        //endeudamiento bancario
                        var endBancario = await _impersonaDomain.GetmPersonaEndBancByCodigoAsync(persona.PeCodigo);
                        if (endBancario.Count > 0)
                        {
                            foreach (var item in endBancario)
                            {
                                var insertMorCom = await _bankDebtDomain.AddAsync(new BankDebt
                                {
                                    Id = 0,
                                    IdPerson = inserted,
                                    BankName = item.SbdNombre,
                                    Qualification = item.SbdCalifi,
                                    QualificationEng = item.SbdCalifiIng,
                                    DebtNc = (decimal)item.SbdMonmn,
                                    DebtFc = (decimal)item.SbdMonme,
                                });
                            }
                        }

                        //historial de quien es quien
                        var listTraductionPersonHis = new List<Traduction>();
                        listTraductionPersonHis.AddRange(await context.Traductions.Where(x => x.IdPerson == inserted && x.Identifier.Contains("_H_")).ToListAsync());
                        foreach (var item in listTraductionPersonHis)
                        {
                            if (item.Identifier == "L_H_DETAILS")
                            {
                                if (persona.PeAnteceIng != null)
                                {
                                    item.LargeValue = persona.PeAnteceIng;
                                }
                            }
                        }
                        var insertHist = await _personHistoryDomain.AddPersonHistoryAsync(new PersonHistory
                        {
                            Id = 0,
                            IdPerson = inserted,
                            HistoryCommentary = persona.PeAntece
                        }, listTraductionPersonHis);
                        if (insertHist != 0)
                        {
                        }
                        else
                        {
                            Console.WriteLine("Error en persona Historia :" + inserted);
                        }

                        //informacion general
                        var listTraductionPersonInf = new List<Traduction>();
                        listTraductionPersonInf.AddRange(await context.Traductions.Where(x => x.IdPerson == inserted && x.Identifier.Contains("_IG_")).ToListAsync());
                        foreach (var item in listTraductionPersonInf)
                        {
                            if (item.Identifier == "L_IG_DETAILS")
                            {
                                if (persona.PeObservIng != null)
                                {
                                    item.LargeValue = persona.PeObservIng;
                                }
                            }
                        }
                        var insertInf = await _personGeneralInfoDomain.AddPersonGeneralInfoAsync(new PersonGeneralInformation
                        {
                            Id = 0,
                            IdPerson = inserted,
                            GeneralInformation = persona.PeObserv
                        }, listTraductionPersonInf);
                        if (insertInf != 0)
                        {
                        }
                        else
                        {
                            Console.WriteLine("Error en persona Info General :" + inserted);
                        }
                    }
                    else
                    {
                        _logger.LogError("Error persona :" + persona.PeCodigo);
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error persona :" + persona.PeCodigo);
                    continue;
                }
            }

            return true;
        }

        public async Task<bool> MigrateSubscriber()
        {
            throw new NotImplementedException();
        }
        public async Task<bool> MigrateSubscriberType()
        {
            throw new NotImplementedException();
        }

        public Task MigrateUser()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MigrateOldTicket()
        {
            int[] year = { 2024,2023, 2022, 2021, 2020, 2019, 2018, 2017, 2016, 2015, 2014, 2013, 2012, 2011,2010,2009,2007,2006 };
           
                using (var mysqlContext = new MySqlContext())
                using (var context = new SqlCoreContext())
                {
                for (int i = 0; i < year.Length; i++)
                {
                    var cupon = await mysqlContext.TCupons.Where(x => x.CupEstado == "J" &&
                    x.EpCodigo != "" && x.Migra == 0 && x.CupFecped.Value.Year == year[i]).ToListAsync();

                    foreach (var item in cupon)
                    {
                        try
                        {
                            context.OldTickets.Add(new OldTicket
                            {
                                Cupcodigo = item.CupCodigo.ToString(),
                                FechaPedido = item.CupFecped,
                                FechaDespacho = item.CupFecdes,
                                Abonado = item.AboCodigo,
                                Idioma = item.IdiCodigo == "001" ? "I" : item.IdiCodigo == "002" ? "E" : "A",
                                EmpresaPersona = item.EmpPer != null ? item.EmpPer : item.EpCodigo == null ? null : item.EpCodigo.StartsWith('P') ? "P" : "E",
                                FechaVencimiento = item.CupFecvcto,
                                TipoInforme = item.CupTipinf.Substring(0,2),
                                Tramite = GetProcedureType(item.TramCodigo),
                                NombreDespachado = item.CupNomdes,
                                NombreSolicitado = item.CupNomsol,
                                Empresa = item.EpCodigo

                            });

                            await context.SaveChangesAsync();
                            item.Migra = 1;
                            mysqlContext.TCupons.Update(item);
                            await mysqlContext.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            continue;

                        }

                    }
                }

                
            }
            return true;
        }
        private static string GetProcedureType(string? tram)
        {
            if (!string.IsNullOrEmpty(tram))
            {
                var value = int.Parse(tram);
                if (value <= 8)
                {
                    return "T" + value;
                }
                else
                {
                    switch (value)
                    {
                        case 9:
                            return "O1";
                        case 10:
                            return "O2";
                        case 11:
                            return "O3";
                    }
                }
            }
            return string.Empty;
        }
    }
}
