using AutoMapper;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using Microsoft.IdentityModel.Tokens;
using DRRCore.Infraestructure.Interfaces.CoreRepository;

namespace DRRCore.Application.Main.CoreApplication
{
    public class XmlApplication : IXmlApplication
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ITicketDomain _ticketDomain;
        private readonly ICompanyDomain _companyDomain;
        private readonly ICompanyBackgroundDomain _companyBackgroundDomain;
        private readonly ICompanyBranchDomain _companyBranchDomain;
        private readonly ICompanyFinancialInformationDomain _companyFinancialInformationDomain;
        private readonly ICompanyGeneralInformationDomain _companyGeneralInformationDomain;
        private readonly ICompanySBSDomain _companySBSDomain;
        public XmlApplication(ILogger logger,IMapper mapper, ICompanyDomain companyDomain,
            ICompanyBackgroundDomain companyBackgroundDomain, ICompanyBranchDomain companyBranchDomain, ICompanyFinancialInformationDomain companyFinancialInformationDomain,
            ITicketDomain ticketDomain, ICompanyGeneralInformationDomain companyGeneralInformationDomain
            )
        {
            _ticketDomain = ticketDomain;
            _companyDomain = companyDomain;
            _companyBackgroundDomain = companyBackgroundDomain;
            _companyBranchDomain = companyBranchDomain;
            _companyFinancialInformationDomain = companyFinancialInformationDomain;
            _companyGeneralInformationDomain = companyGeneralInformationDomain;
            _logger = logger;
            _mapper = mapper;
        }
        public string? GetCreditRiskCode(int? idCreditRisk)
        {
            return idCreditRisk == 1 ? "0005" : idCreditRisk == 2 ? "0000" : idCreditRisk == 3 ? "0001" :
                           idCreditRisk == 4 ? "0002" : idCreditRisk == 5 ? "0003" : idCreditRisk == 6 ? "0011" : idCreditRisk == 7 ? "0004" : "";
        }
        public string? GetBusinessBranchCode(int? idBusinessBranch)
        {
            return idBusinessBranch == 1 ? "0" : idBusinessBranch == 3 ? "1" : idBusinessBranch == 4 ? "2" : idBusinessBranch == 5 ? "3" :
            idBusinessBranch == 6 ? "4" : idBusinessBranch == 7 ? "5" : idBusinessBranch == 8 ? "6" : idBusinessBranch == 9 ? "7" : idBusinessBranch == 10 ? "8" :
            idBusinessBranch == 11 ? "9" : idBusinessBranch == 12 ? "10" : idBusinessBranch == 13 ? "11" : idBusinessBranch == 14 ? "12" : idBusinessBranch == 15 ? "13" :
            idBusinessBranch == 16 ? "14" : idBusinessBranch == 17 ? "15" : idBusinessBranch == 18 ? "16" : idBusinessBranch == 19 ? "17" : idBusinessBranch == 20 ? "18" :
            idBusinessBranch == 21 ? "19" : idBusinessBranch == 22 ? "20" : idBusinessBranch == 23 ? "21" : idBusinessBranch == 24 ? "22" : idBusinessBranch == 25 ? "23" :
            idBusinessBranch == 26 ? "24" : idBusinessBranch == 27 ? "25" : idBusinessBranch == 28 ? "26" : idBusinessBranch == 29 ? "27" : idBusinessBranch == 30 ? "28" :
            idBusinessBranch == 31 ? "29" : idBusinessBranch == 32 ? "30" : "";
        }
        public string? GetCountryCode(int? idCountry)
        {
            return idCountry == null ? "" : idCountry == 11 ? "001" : idCountry == 29 ? "002" : idCountry == 34 ? "003" :
            idCountry == 54 ? "004" : idCountry == 57 ? "005" : idCountry == 49 ? "006" :
            idCountry == 70 ? "007" : idCountry == 72 ? "008" : idCountry == 100 ? "009" :
            idCountry == 108 ? "010" : idCountry == 168 ? "012" : idCountry == 179 ? "013" :
            idCountry == 181 ? "014" :idCountry == 182 ? "015" : idCountry == 187 ? "016" :
            idCountry == 69 ? "017" : idCountry == 237 ? "018" : idCountry == 250 ? "019" :
            idCountry == 249 ? "020" :idCountry == 253 ? "021" : idCountry  == 105 ? "022" :
            idCountry == 147 ? "023" :idCountry == 98 ? "024" :idCountry == 104 ? "025" :
            idCountry == 46 ? "026" : idCountry == 60 ? "027" : idCountry == 256 ? "029" :
            idCountry == 255 ? "030" :idCountry == 43 ? "031" :idCountry == 25 ? "032" :
            idCountry == 18 ? "033" : idCountry == 120 ? "034" :idCountry == 183 ? "035" :
            idCountry == 92 ? "036" : idCountry == 15 ? "037" : idCountry == 21 ? "038" :
            idCountry == 151 ? "039" :idCountry == 59 ? "040" :idCountry == 220 ? "041" :
            idCountry == 186 ? "042" :idCountry == 13 ? "043" :idCountry == 16 ? "044" :
            idCountry == 24 ? "045" : idCountry == 27 ? "046" : idCountry  == 68 ? "047" :
            idCountry == 84 ? "048" : idCountry == 97 ? "049" : idCountry == 123 ? "064" :
            idCountry == 109 ? "051" :idCountry == 119 ? "052" : idCountry == 121 ? "053" :
            idCountry == 218 ? "054" :idCountry == 196 ? "055" : idCountry == 197 ? "056" :
            idCountry == 198 ? "057" :idCountry == 224 ? "058" : idCountry == 8 ? "059" :
            idCountry == 149 ? "060" :idCountry == 50 ? "061" :idCountry == 229 ? "062" :
            idCountry == 10 ? "063" : idCountry == 65 ? "065" : idCountry == 239 ? "066" :
            idCountry == 205 ? "067" :idCountry == 83 ? "068" :idCountry == 175 ? "069" :
            idCountry == 62 ? "070" : idCountry == 191 ? "071" : idCountry  == 245 ? "072" :
            idCountry == 247 ? "073" :idCountry == 200 ? "074" : idCountry == 156 ? "076" :
            idCountry == 194 ? "078" :idCountry == 241 ? "080" : idCountry == 265 ? "081" :
            idCountry == 264 ? "079" :idCountry == 227 ? "083" : idCountry == 226 ? "084" :
            idCountry == 131 ? "085" :idCountry == 112 ? "086" : idCountry == 118 ? "087" :
            idCountry == 185 ? "088" :idCountry == 137 ? "089" : idCountry == 165 ? "090" :
            idCountry == 94 ? "091" : idCountry == 142 ? "092" : idCountry == 243 ? "093" :
            idCountry == 246 ? "095" :idCountry == 124 ? "096" : idCountry  == 4 ? "097" :
            idCountry == 91 ? "099" : idCountry == 95 ? "100" : idCountry == 266 ? "101" :
            idCountry == 210 ? "102" :idCountry == 136 ? "103" : idCountry == 177 ? "104" :
            idCountry == 7 ? "105" : idCountry == 26 ? "106" : idCountry == 32 ? "107" :
            idCountry == 38 ? "108" : idCountry == 39 ? "109" :idCountry == 42 ? "110" :
            idCountry == 47 ? "111" : idCountry == 48 ? "113" : idCountry  == 55 ? "114" :
            idCountry == 267 ? "115" :idCountry == 71 ? "116" :idCountry== 102 ? "117" :
            idCountry == 75 ? "118" : idCountry == 78 ? "119" : idCountry== 88 ? "120" :
            idCountry == 90 ? "121" : idCountry == 93 ? "122" : idCountry == 103 ? "123" :
            idCountry == 125 ? "124" :idCountry == 134 ? "125" : idCountry == 135 ? "126" :
            idCountry == 140 ? "127" :idCountry == 141 ? "128" : idCountry == 144 ? "129" :
            idCountry == 148 ? "130" :idCountry == 157 ? "132" : idCountry == 158 ? "133" :
            idCountry == 160 ? "134" :idCountry == 168 ? "135" : idCountry == 192 ? "136" :
            idCountry == 259 ? "137" :idCountry == 206 ? "139" : idCountry == 209 ? "140" :
            idCountry == 215 ? "141" :idCountry == 223 ? "142" : idCountry == 77 ? "143" :
            idCountry == 234 ? "145" :idCountry == 244 ? "147" : idCountry == 268 ? "148" :
            idCountry == 261 ? "149" :idCountry == 262 ? "150" : idCountry == 1 ? "152" :
            idCountry == 12 ? "153" : idCountry == 17 ? "154" : idCountry == 19 ? "155" :
            idCountry == 20 ? "156" : idCountry == 28 ? "157" : idCountry == 36 ? "158" :
            idCountry == 281 ? "159" : idCountry  == 41 ? "160" : idCountry == 61 ? "161" :
            idCountry == 113 ? "162" : idCountry == 114 ? "163" : idCountry == 115 ? "164" :
            idCountry == 129 ? "166" : idCountry == 128 ? "167" : idCountry == 130 ? "168" :
            idCountry == 154 ? "169" : idCountry == 162 ? "170" : idCountry == 176 ? "171" :
            idCountry == 222 ? "172" : idCountry == 188 ? "173" : idCountry == 204 ? "174" :
            idCountry == 221 ? "175" : idCountry == 228 ? "176" : idCountry == 230 ? "177" :
            idCountry == 232 ? "178" : idCountry == 240 ? "179" : idCountry == 251 ? "181" :
            idCountry == 254 ? "182" : idCountry == 260 ? "183" : idCountry == 3 ? "185" :
            idCountry == 6 ? "186" : idCountry == 31 ? "187" : idCountry == 37 ? "188" :
            idCountry == 23 ? "189" :idCountry == 58 ? "190" : idCountry == 76 ? "191" :
            idCountry == 80 ? "192" : idCountry == 110 ? "193" : idCountry == 111 ? "194" :
            idCountry == 116 ? "195" : idCountry == 138 ? "197" :idCountry == 172 ? "198" :
            idCountry == 145 ? "199" : idCountry == 152 ? "200" :idCountry == 153 ? "201" :
            idCountry == 190 ? "202" : idCountry == 202 ? "203" :idCountry == 155 ? "204" :
            idCountry == 212 ? "205" : idCountry == 213 ? "206" :idCountry == 214 ? "208" :
            idCountry == 252 ? "209" : idCountry == 82 ? "210" : idCountry == 161 ? "211" :
            idCountry == 146 ? "212" : idCountry == 99 ? "213" : idCountry == 201 ? "214" :
            idCountry == 178 ? "215" : idCountry == 236 ? "216" :idCountry == 86 ? "217" :
            idCountry == 171 ? "221" : idCountry == 282 ? "222" :idCountry == 85 ? "219" :
            idCountry == 117 ? "224" : idCountry == 143 ? "220" :idCountry == 139 ? "225" :
            idCountry == 169 ? "011" : idCountry == 164 ? "028" :idCountry == 283 ? "207" :
            idCountry == 284 ? "218" : idCountry == 285 ? "223" :idCountry == 63 ? "226" :
            idCountry == 180 ? "227" : idCountry == 286 ? "228" :idCountry == 143 ? "229" :
            idCountry == 208 ? "230" : idCountry == 64 ? "231" : idCountry == 263 ? "232" :
            idCountry == 60 ? "233" : idCountry == 30 ? "234" : idCountry == 217 ? "235" :
            idCountry == 231 ? "236" : idCountry == 30 ? "237" :idCountry == 30 ? "238" :
           idCountry == 18 ? "239" : idCountry == 207 ? "240" : idCountry == 155 ? "241" : "";
        }
        public Boolean isNumeric(String numero)
        {
            try
            {
                double.Parse(numero);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<Response<GetFileResponseDto>> GetXmlCredendoAsync(int idTicket)
        {
            var response = new Response<GetFileResponseDto>();
            try
            {
                using var context = new SqlCoreContext();
                var idParameter = new SqlParameter("@idTicket", idTicket);
                var resultCompany = context.Set<CompanyXmlData>()
                                     .FromSqlRaw("EXECUTE DataCompanyCredendo @idTicket", idParameter)
                                     .AsEnumerable()
                                     .FirstOrDefault();
                var resultBalance = context.Set<CompanyBalanceData>()
                                    .FromSqlRaw("EXECUTE BalanceCompanyCredendo @idTicket", idParameter)
                                    .AsEnumerable()
                                    .ToList();
                var resultFunction =  context.Set<CompanyFunctionData>()
                                    .FromSqlRaw("EXECUTE FunctionCompanyCredendo @idTicket", idParameter)
                                    .AsEnumerable()
                                    .ToList();
                var resultLegalEvents =  context.Set<CompanyLegalEventsData>()
                                    .FromSqlRaw("EXECUTE LegalEventsCompanyCredendo @idTicket", idParameter)
                                    .AsEnumerable()
                                    .ToList();
                var resultRelated=  context.Set<CompanyRelatedData>()
                                    .FromSqlRaw("EXECUTE RelatedCompanyCredendo @idTicket", idParameter)
                                    .AsEnumerable()
                                    .ToList();
                if (resultCompany != null)
                {
                    string[] telefono = null;
                    string cadena = "";
                    List<XElement> TelefonoArray = new List<XElement>();
                    if (resultCompany.ContactPhoneNumber != "" && resultCompany.ContactPhoneNumber != null)
                    {
                        telefono = resultCompany.ContactPhoneNumber.Split(new char[] { ',', ';', '-' });
                        telefono = (from e in telefono
                                    select e.Trim()).ToArray();

                        for (int n = 0; n <= telefono.Length - 1; n++)
                        {
                            cadena = "<contact><contactPhoneNumber>" + resultCompany.ContactPrefixPhoneNumber + " " + telefono[n] + "</contactPhoneNumber></contact>";
                            TelefonoArray.Add(new XElement(XElement.Parse(cadena)));
                        }
                    }

                    string[] correo = null;
                    cadena = "";
                    List<XElement> CorreoArray = new List<XElement>();
                    if (resultCompany.ContactEmailAddress != "" && resultCompany.ContactPhoneNumber != null)
                    {
                        correo = resultCompany.ContactEmailAddress.Split(new char[] { ',', ';' });
                        correo = (from e in correo
                                  select e.Trim()).ToArray();

                        for (int n = 0; n <= correo.Length - 1; n++)
                        {
                            cadena = "<contact><contactEmailAddress>" + correo[n] + "</contactEmailAddress></contact>";
                            CorreoArray.Add(new XElement(XElement.Parse(cadena)));
                        }
                    }
                    Boolean bMaximumSingleCreditAdviceAmount = true;
                    if (String.IsNullOrEmpty(resultCompany.SourceEvaluationMaximumSingleCreditAdviceAmount) == true)
                    {
                        bMaximumSingleCreditAdviceAmount = false;
                    }
                    else
                    {
                        if (isNumeric(resultCompany.SourceEvaluationMaximumSingleCreditAdviceAmount) == true)
                        {
                            double valor = Convert.ToDouble(resultCompany.SourceEvaluationMaximumSingleCreditAdviceAmount);
                            if (valor == 0)
                            {
                                bMaximumSingleCreditAdviceAmount = false;
                            }
                        }
                    }

                    XNamespace xsiNs = "http://www.w3.org/2001/XMLSchema-instance";
                    XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-8", ""),
                        new XElement("credendoCanonicalReport",
                            new XAttribute(XNamespace.Xmlns + "xsi", xsiNs),
                            //new XAttribute("noNamespaceSchemaLocation", filePahthXsd ),
                            new XElement("version",
                                new XElement("major"),
                                new XElement("minor"),
                                new XElement("mappingVersion", (resultCompany == null ? "" : resultCompany.DebtorName))
                            ),
                            new XElement("inquiry",
                                new XElement("debtorName", (resultCompany == null ? "" : resultCompany.DebtorName)),
                                new XElement("debtorCountry", (resultCompany == null ? "" : GetCountryCode(resultCompany.Country))),
                                //new XElement("providerName"),
                                new XElement("providerReportReference", (resultCompany == null ? "" : resultCompany.ProviderReportReference)),
                                //new XElement("financialFiguresDate"),
                                new XElement("reportDefaultCurrency", (resultCompany == null ? "" : resultCompany.ReportDefaultCurrency)),
                                new XElement("reportDefaultMonetaryFactorCode", (resultCompany == null ? "" : resultCompany.ReportDefaultMonetaryFactorCode)),
                                new XElement("language", (resultCompany == null ? "" : resultCompany.Language))
                            ),
                            new XElement("key",
                                new XElement("taxIdentificationKey",
                                    new XElement("key", (resultCompany == null ? "" : resultCompany.TaxIdentificationKey))
                                //new XElement("category", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().taxIdentificationCategory))
                                ),

                                new XElement("sourceIdentificationKey",
                                    new XElement("key", (resultCompany == null ? "" : resultCompany.SourceIdentificationKey))
                                //new XElement("category", (bussines.FirstOrDefault() ?? "").sourceIdentificationCategory)
                                )
                            ),
                            new XElement("operatingStatus",
                                new XElement("operatingStatus", (resultCompany == null ? "" : resultCompany.OperatingStatus)),
                                new XElement("operatingStatusObservationDate", (resultCompany == null ? "" : resultCompany.OperatingStatusObservationDate))
                            ),
                            new XElement("masterDataBusinessPartner",
                                //new XElement("branchIndicator", "No branch"),
                                new XElement("name",
                                    new XElement("companyName", (resultCompany == null ? "" : resultCompany.CompanyName)),
                                    new XElement("companyNameType", (resultCompany == null ? "" : resultCompany.CompanyNameType)),
                                    new XElement("languageAndAlphabetType", (resultCompany == null ? "" : resultCompany.LanguageAndAlphabetType))
                                ),
                                resultCompany.CompanyTradName == "" ? null :
                                new XElement("name",
                                    new XElement("companyName", (resultCompany == null ? "" : resultCompany.CompanyTradName)),
                                    new XElement("companyNameType", (resultCompany == null ? "" : resultCompany.CompanyTradeNameType)),
                                    new XElement("languageAndAlphabetType", (resultCompany == null ? "" : resultCompany.LanguageAndAlphabetTradeType))
                                ),
                                new XElement("address",
                                    new XElement("addressType", (resultCompany == null ? "" : resultCompany.AddressType)),
                                    new XElement("languageAndAlphabetType", (resultCompany == null ? "" : resultCompany.LanguageAndAlphabetType)),
                                    new XElement("street", (resultCompany == null ? "" : resultCompany.Street)),
                                    //bussines.FirstOrDefault().postCode == "" ? null : new XElement("postCode", (bussines.FirstOrDefault().postCode)),
                                    new XElement("postCode", (resultCompany == null ? "" : resultCompany.PostCode)),
                                    new XElement("city", (resultCompany == null ? "" : resultCompany.City)),
                                    new XElement("stateRegion", (resultCompany == null ? "" : resultCompany.StateRegion)),
                                    new XElement("country", (resultCompany == null ? "" : GetCountryCode(resultCompany.Country)))
                                ),
                                resultCompany.FormerAddressStreet == "" ? null :
                                 new XElement("address",
                                    new XElement("addressType", (resultCompany == null ? "" : resultCompany.FormerAddressType)),
                                    new XElement("languageAndAlphabetType", (resultCompany == null ? "" : resultCompany.FormerAddresslanguageAndAlphabetType)),
                                    new XElement("street", (resultCompany == null ? "" : resultCompany.FormerAddressStreet)),
                                    new XElement("country", (resultCompany == null ? "" : GetCountryCode(resultCompany.FormerAddressCountry)))
                                ),
                                //bussines.Where(p => p.cR_MDBP_ADDRESS_street2 != "")
                                //.Select(p => 
                                //    new XElement("address",
                                //        new XElement("addressType", p.CR_MDBP_ADDRESS_street2NombreIng),
                                //        new XElement("languageAndAlphabetType", (bussines.FirstOrDefault() ?? "").CR_MDBP_ADDRESS_languageAndAlphabetType),
                                //        new XElement("street",p.CR_MDBP_ADDRESS_street2),
                                //        new XElement("postCode", (bussines.FirstOrDefault() ?? "").CR_MDBP_ADDRESS_postCode),
                                //        new XElement("city", (bussines.FirstOrDefault() ?? "").CR_MDBP_ADDRESS_city), 
                                //        new XElement("stateRegion", (bussines.FirstOrDefault() ?? "").CR_MDBP_ADDRESS_stateRegion), 
                                //        new XElement("country", (bussines.FirstOrDefault() ?? "").CR_MDBP_ADDRESS_country) 
                                //    )
                                //),


                                resultCompany.NumberOfEmployeesWithinTheCompanyRangeFrom == 0 ? null :
                                new XElement("numberOfEmployees",
                                    new XElement("numberOfEmployeesType", (resultCompany == null ? "" : resultCompany.NumberOfEmployeesType)),
                                    new XElement("numberOfEmployeesWithinTheCompanyRangeFrom", (resultCompany == null ? "" : resultCompany.NumberOfEmployeesWithinTheCompanyRangeFrom))
                                ),
                                new XElement("companyLegalForm", (resultCompany == null ? "" : resultCompany.CompanyLegalForm)),

                                new XElement("companyListedStockExchange", (resultCompany == null ? "" : resultCompany.CompanyListedStockExchange)),

                                CorreoArray.Count == 0 ? null : CorreoArray,

                                TelefonoArray.Count == 0 ? null : TelefonoArray,

                                new XElement("contact",
                                     new XElement("contactWebsiteAddress", (resultCompany == null ? "" : resultCompany.ContactWebsiteAddress))
                                ),
                                new XElement("incorporationDate", (resultCompany == null ? "" : resultCompany.IncorporationDate)),
                                new XElement("registrationDate", (resultCompany == null ? "" : resultCompany.RegistrationDate)),
                                new XElement("companyMainSector",
                                    new XElement("companyMainSectorCode", (resultCompany == null ? "" : GetBusinessBranchCode(resultCompany.CompanyMainSectorCode))),
                                    new XElement("companyMainSectorCodeType", (resultCompany == null ? "" : resultCompany.CompanyMainSectorCodeType)),
                                    new XElement("companyMainSectorDescription", (resultCompany == null ? "" : resultCompany.CompanyMainSectorDescription))
                                ),
                                new XElement("companyNature", (resultCompany == null ? "" : resultCompany.CompanyNature))
                            //new XElement("companyNatureFromProvider", (bussines.FirstOrDefault() ?? "").companyNatureFromProvider)
                            ),
                            resultBalance.Select(balance =>
                                new XElement("financialData",
                                    new XElement("keyFigures",
                                        new XElement("accountingPeriod", balance.CR_FD_keyFigures_accountingPeriod),
                                        new XElement("endDateOfAccountingPeriod", balance.CR_FD_keyFigures_endDateOfAccountingPeriod),
                                        new XElement("typeOfFigures", balance.CR_FD_keyFigures_typeOfFigures),
                                        new XElement("balancesheetDefaultCurrency", balance.CR_FD_keyFigures_balancesheetDefaultCurrency),
                                        new XElement("balancesheetDefaultMonetaryFactorCode", balance.CR_FD_keyFigures_balancesheetDefaultMonetaryFactorCode),
                                        //new XElement("keyFigure",
                                        //new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeTotalAssets), 
                                        //new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueTotalAssets), 
                                        //new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeTotalAssets)
                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueTotalAssets == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeTotalAssets),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueTotalAssets)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeTotalAssets)
                                        ),
                                        //new XElement("keyFigure",
                                        //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeFixedAssets), 
                                        //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueFixedAssets), 
                                        //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeFixedAssets)
                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueFixedAssets == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeFixedAssets),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueFixedAssets)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeFixedAssets)
                                        ),
                                        //new XElement("keyFigure",
                                        //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeCurrentAssets),
                                        //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCurrentAssets), 
                                        //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeCurrentAssets)
                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCurrentAssets == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeCurrentAssets),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCurrentAssets)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeCurrentAssets)
                                        ),

                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueNonCurrentAssets == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeNonCurrentAssets),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueNonCurrentAssets)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeNonCurrentAssets)
                                        ),

                                        //new XElement("keyFigure",
                                        //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeTotalEquityLiabilities), 
                                        //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCodeTotalEquityLiabilities), 
                                        //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeCodeTotalEquityLiabilities)
                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCodeTotalEquityLiabilities == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeTotalEquityLiabilities),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCodeTotalEquityLiabilities)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeCodeTotalEquityLiabilities)
                                        ),
                                        //new XElement("keyFigure",
                                        //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquity),
                                        //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquity),
                                        //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquity)
                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquity == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquity),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquity)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquity)
                                        ),

                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueLTLiabilities == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeLTLiabilities),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueLTLiabilities)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeLTLiabilities)
                                        ),

                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueSTLiabilities == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeSTLiabilities),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueSTLiabilities)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeSTLiabilities)
                                        ),

                                        //new XElement("keyFigure",
                                        //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityPaiduPCapital), 
                                        //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityPaiduPCapital), 
                                        //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityPaiduPCapital)
                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityPaiduPCapital == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityPaiduPCapital),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityPaiduPCapital)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityPaiduPCapital)
                                        ),
                                        //new XElement("keyFigure",
                                        //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityRevenueSales), 
                                        //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityRevenueSales), 
                                        //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityRevenueSales)
                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityRevenueSales == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityRevenueSales),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityRevenueSales)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityRevenueSales)
                                        ),
                                        //new XElement("keyFigure",
                                        //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityNetResult), 
                                        //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityNetResult), 
                                        //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityNetResult)
                                        balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityNetResult == 0 ? null :
                                            new XElement("keyFigure",
                                                new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityNetResult),
                                                new XElement("keyFigureItemValue", Math.Truncate((decimal)balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityNetResult)),
                                                new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityNetResult)
                                        )
                                    )
                                )
                            ),
                             // SERA UNA LISTA
                             resultFunction.Select(function =>
                                 new XElement("function",
                                    new XElement("functionType", function.FunctionType == null ? "" : function.FunctionType),
                                    new XElement("nameOfPerson", function.NameOfPerson == null ? "" : function.NameOfPerson)
                                )
                             ),

                             //legalEvents.Select(legalEvent =>
                             //    new XElement("legalEvent",
                             //         new XElement("event", legalEvent.legalEvent),
                             //         new XElement("sourceEvent", legalEvent.sourceEvent),
                             //         new XElement("endDate", legalEvent.endDate),
                             //         new XElement("startDate", legalEvent.startDate)
                             //     )
                             //),

                             resultLegalEvents.Select(legalEvent =>
                               legalEvent.LegalEvent == null ? null : new XElement("legalEvent",
                                                            new XElement("event", legalEvent.LegalEvent == null ? "" : legalEvent.LegalEvent),
                                                            new XElement("sourceEvent", legalEvent.SourceEvent == null ? "" : legalEvent.SourceEvent),
                                                            new XElement("endDate", legalEvent.EndDate == null ? "" : legalEvent.EndDate),
                                                            new XElement("startDate", legalEvent.StartDate == null ? "" : legalEvent.StartDate))
                             ),

                            new XElement("sourceEvaluation",
                                new XElement("sourceEvaluationOriginalPaymentExperience", (resultCompany == null ? "" : resultCompany.SourceEvaluationOriginalPaymentExperience)),
                                new XElement("sourceEvaluationSimplePaymentExperience", (resultCompany == null ? "" : GetCreditRiskCode(resultCompany.SourceEvaluationSimplePaymentExperience))),
                                new XElement("sourceEvaluationExtendedPaymentExperience", (resultCompany == null ? "" : resultCompany.SourceEvaluationExtendedPaymentExperience)),
                                new XElement("sourceEvaluationCreditAdviceAmount", (resultCompany == null ? "" : resultCompany.SourceEvaluationCreditAdviceAmount)),
                                new XElement("sourceEvaluationCreditAdviceExplanation", (resultCompany == null ? "" : resultCompany.SourceEvaluationCreditAdviceExplanation)),
                                new XElement("sourceEvaluationMaximumCreditAdviceAmount", (resultCompany == null ? "" : resultCompany.SourceEvaluationMaximumCreditAdviceAmount)),
                                //new XElement("sourceEvaluationMaximumSingleCreditAdviceAmount", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationMaximumSingleCreditAdviceAmount)),
                                //new XElement("sourceEvaluationCurrency", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationCurrency)),
                                bMaximumSingleCreditAdviceAmount == false ? null : new XElement("sourceEvaluationMaximumSingleCreditAdviceAmount", resultCompany.SourceEvaluationMaximumSingleCreditAdviceAmount == null ? "" : resultCompany.SourceEvaluationMaximumSingleCreditAdviceAmount),
                                bMaximumSingleCreditAdviceAmount == false ? null : new XElement("sourceEvaluationCurrency", resultCompany.SourceEvaluationCurrency == null ? "" : resultCompany.SourceEvaluationCurrency),
                                new XElement("sourceRating", (resultCompany == null ? "" : resultCompany.SourceRating)),
                                new XElement("sourceRatingRange", (resultCompany == null ? "" : resultCompany.SourceRatingRange)),
                                new XElement("sourceRatingComments", (resultCompany == null ? "" : resultCompany.SourceRatingComments)),
                                new XElement("sourceEvaluationComments", (resultCompany == null ? "" : resultCompany.SourceEvaluationComments)),

                               resultRelated.Select(related =>
                                  new XElement("relatedCompany",
                                       new XElement("relatedName", related.RelatedName == null ? "" : related.RelatedName),
                                       new XElement("relatedTaxReg", related.RelatedTaxReg == null ? "" : related.RelatedTaxReg),
                                       new XElement("relatedCountry", related.RelatedCountry == null ? "" : GetCountryCode(related.RelatedCountry)),
                                       new XElement("dateInc", related.DateInc == null ? "" : related.DateInc),
                                       new XElement("relationType", related.RelationType == null ? "" : related.RelationType)
                                   )
                               )
                            )
                        )
                    );
                    string xmlString = xDoc.ToString();

                    // Convertir la cadena XML en un MemoryStream
                    byte[] byteArray = Encoding.UTF8.GetBytes(xmlString);
                    response.Data = new GetFileResponseDto
                    {
                        File = byteArray,
                        ContentType = "application/xml",
                        Name = "N" + resultCompany.TicketNumber.ToString("D6") + "_" + resultCompany.DebtorName.Replace(" ","_") + ".xml"
                    };
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Response<GetFileResponseDto>> GetXmlAtradiusAsync(int idTicket)
        {
            var response = new Response<GetFileResponseDto>();
            try
            {
                using var context = new SqlCoreContext();
                var idParameter = new SqlParameter("@idTicket", idTicket);
                var ticket = await _ticketDomain.GetByIdAsync(idTicket);
                if(ticket != null)
                {
                    var company = await _companyDomain.GetByIdAsync((int)ticket.IdCompany);
                    var companyBackground = await _companyBackgroundDomain.GetByIdAsync(((int)ticket.IdCompany));
                    var companyBranch = await _companyBranchDomain.GetCompanyBranchByIdCompany((int)ticket.IdCompany);
                    var companyInfoGeneral = await _companyGeneralInformationDomain.GetByIdCompany((int)ticket.IdCompany);
                    //1
                    XmlDocument xmlDoc = new XmlDocument();

                    //Client_Data
                    XmlElement clientDataElement = xmlDoc.CreateElement("Client_Data");
                    xmlDoc.AppendChild(clientDataElement);
                    AddCDataElement(xmlDoc, clientDataElement, "Client", ticket.IdSubscriberNavigation.Code);
                    AddCDataElement(xmlDoc, clientDataElement, "FecInf", ticket.OrderDate.ToString("dddd, MMMM dd, yyyy"));
                    AddCDataElement(xmlDoc, clientDataElement, "Status", ticket.IdSubscriberNavigation.Enable == true ? "Active" : "Inactive");
                    AddCDataElement(xmlDoc, clientDataElement, "Status_Desde", ticket.IdSubscriberNavigation.Code);
                    AddCDataElement(xmlDoc, clientDataElement, "Ordered_On", ticket.OrderDate.ToString("dddd, MMMM dd, yyyy"));
                    if (!ticket.ReferenceNumber.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "ClientReference", ticket.ReferenceNumber);
                    }
                    if (!ticket.ReferenceNumber.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Order_Num", ticket.ReferenceNumber);
                    }
                    AddCDataElement(xmlDoc, clientDataElement, "Priority", ticket.ProcedureType);
                    AddCDataElement(xmlDoc, clientDataElement, "Your_Request", ticket.RequestedName);

                    //Identification
                    XmlElement identificationElement = xmlDoc.CreateElement("Identification");
                    xmlDoc.AppendChild(identificationElement);
                    AddCDataElement(xmlDoc, identificationElement, "Correct_Company_Name", ticket.IdCompanyNavigation.Name);
                    if (!company.SocialName.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Trade_Name", company.SocialName);
                    }
                    if (!company.TaxTypeCode.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Taxpayer_Registration", company.TaxTypeCode);
                    }
                    if (!company.Address.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Main_Address", company.Address);
                    }
                    if (!company.Place.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "City_Province", company.Place);
                    }
                    if (!company.PostalCode.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Postal_Code", company.PostalCode);
                    }
                    if (company.IdCountry != 0 && company.IdCountry != null)
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Country ", company.IdCountryNavigation.Name);
                    }
                    if (!company.Telephone.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Telephone ", company.Telephone);
                    }
                    if (!company.Cellphone.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Phone_Mobile ", company.Cellphone);
                    }
                    if (!company.WhatsappPhone.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "WhatsApp ", company.WhatsappPhone);
                    }
                    if (!company.Email.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Email ", company.Email);
                    }
                    if (!company.WebPage.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Web ", company.WebPage);
                    }
                    if (!company.Traductions.Where(x => x.Identifier == "L_E_COMIDE").FirstOrDefault().LargeValue.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Comment ", company.Traductions.Where(x => x.Identifier == "L_E_COMIDE").FirstOrDefault().LargeValue);
                    }

                    //Summary
                    XmlElement summaryElement = xmlDoc.CreateElement("Summary");
                    xmlDoc.AppendChild(summaryElement);
                    if (!companyInfoGeneral.IdCompanyNavigation.Traductions.Where(x => x.Identifier == "L_I_GENERAL").FirstOrDefault().LargeValue.IsNullOrEmpty())
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Ver_Resumen ", companyInfoGeneral.IdCompanyNavigation.Traductions.Where(x => x.Identifier == "L_I_GENERAL").FirstOrDefault().LargeValue);
                    }
                    if (companyBackground.ConstitutionDate != null)
                    {
                        AddCDataElement(xmlDoc, clientDataElement, "Incorporation ", companyBackground.ConstitutionDate.ToString("dd/MM/yyyy"));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception(ex.Message);
            }
            return response;
        }
        static void AddCDataElement(XmlDocument xmlDoc, XmlElement parentElement, string elementName, string value)
        {
            XmlElement element = xmlDoc.CreateElement(elementName);
            XmlCDataSection cdata = xmlDoc.CreateCDataSection(value);
            element.AppendChild(cdata);
            parentElement.AppendChild(element);
        }
    }
}
