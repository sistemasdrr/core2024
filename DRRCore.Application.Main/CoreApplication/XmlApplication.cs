using AutoMapper;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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
        private readonly ICompanySBSDomain _companySBSDomain;
        public XmlApplication(ILogger logger,IMapper mapper, ICompanyDomain companyDomain,
            ICompanyBackgroundDomain companyBackgroundDomain, ICompanyBranchDomain companyBranchDomain, ICompanyFinancialInformationDomain companyFinancialInformationDomain,
            ITicketDomain ticketDomain
            )
        {
            _ticketDomain = ticketDomain;
            _companyDomain = companyDomain;
            _companyBackgroundDomain = companyBackgroundDomain;
            _companyBranchDomain = companyBranchDomain;
            _companyFinancialInformationDomain = companyFinancialInformationDomain;
            _logger = logger;
            _mapper = mapper;
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

        public async Task<Response<CompanyXmlData>> GetXmlCompanyAsync(int idTicket)
        {
            var response = new Response<CompanyXmlData>();
            try
            {
                using var context = new SqlCoreContext();
                var idParameter = new SqlParameter("@idTicket", idTicket);
                var company = context.CompanyXmls.FromSqlRaw("EXECUTE DataCompanyCredendo @idTicket", idParameter).FirstOrDefaultAsync();
                if(company != null)
                {
                    response.Data = _mapper.Map<CompanyXmlData>(company);
                }
                /*
                var ticket = await _ticketDomain.GetByIdAsync(idTicket);
                if(ticket != null)
                {
                    var company = await _companyDomain.GetByIdAsync((int)ticket.IdCompany);
                    if(company != null)
                    {
                        XNamespace xsiNs = "http://www.w3.org/2001/XMLSchema-instance";
                        XDocument xDoc = new XDocument(
                            new XDeclaration("1.0", "UTF-8", ""),
                            new XElement("credendoCanonicalReport",
                                new XAttribute(XNamespace.Xmlns + "xsi", xsiNs),
                                //new XAttribute("noNamespaceSchemaLocation", filePahthXsd ),
                                new XElement("version",
                                    new XElement("major" ),
                                    new XElement("minor" ),
                                    new XElement("mappingVersion", (company == null ? "" : company.Name))
                                ),
                                new XElement("inquiry",
                                    new XElement("debtorName", (company == null ? "" : company.Name)),
                                    new XElement("debtorCountry", (company == null ? "" : GetCountryCode(company.IdCountry)),
                                    //new XElement("providerName"),
                                    new XElement("providerReportReference", (ticket == null ? "" : "N" + ticket.Number.ToString("D6"))),
                                    //new XElement("financialFiguresDate"),
                                    new XElement("reportDefaultCurrency", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().reportDefaultCurrency)),
                                    new XElement("reportDefaultMonetaryFactorCode", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().reportDefaultMonetaryFactorCode)),
                                    new XElement("language", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().language))
                                ),
                                new XElement("key",
                                    new XElement("taxIdentificationKey",
                                        new XElement("key", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().taxIdentificationKey))
                                    //new XElement("category", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().taxIdentificationCategory))
                                    ),
                                   
                                    new XElement("sourceIdentificationKey",
                                        new XElement("key", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceIdentificationKey))
                                    //new XElement("category", (bussines.FirstOrDefault() ?? "").sourceIdentificationCategory)
                                    )
                                ),
                                new XElement("operatingStatus",
                                    new XElement("operatingStatus", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().operatingStatus)),
                                    new XElement("operatingStatusObservationDate", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().operatingStatusObservationDate))
                                ),
                                new XElement("masterDataBusinessPartner",
                                    //new XElement("branchIndicator", "No branch"),
                                    new XElement("name",
                                        new XElement("companyName", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyName)),
                                        new XElement("companyNameType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyNameType)),
                                        new XElement("languageAndAlphabetType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().languageAndAlphabetType))
                                    ),
                                    bussines.FirstOrDefault().companyTradName == "" ? null :
                                    new XElement("name",
                                        new XElement("companyName", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyTradName)),
                                        new XElement("companyNameType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyTradeNameType)),
                                        new XElement("languageAndAlphabetType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().languageAndAlphabetTradeType))
                                    ),
                                    new XElement("address",
                                        new XElement("addressType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().addressType)),
                                        new XElement("languageAndAlphabetType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().languageAndAlphabetType)),
                                        new XElement("street", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().street)),
                                        //bussines.FirstOrDefault().postCode == "" ? null : new XElement("postCode", (bussines.FirstOrDefault().postCode)),
                                        new XElement("postCode", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().postCode)),
                                        new XElement("city", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().city)),
                                        new XElement("stateRegion", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().stateRegion)),
                                        new XElement("country", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().country))
                                    ),
                                    bussines.FirstOrDefault().formerAddressStreet == "" ? null :
                                     new XElement("address",
                                        new XElement("addressType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().formerAddressType)),
                                        new XElement("languageAndAlphabetType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().formerAddresslanguageAndAlphabetType)),
                                        new XElement("street", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().formerAddressStreet)),
                                        new XElement("country", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().formerAddressCountry))
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


                                    bussines.FirstOrDefault().numberOfEmployeesWithinTheCompanyRangeFrom == "" ? null :
                                    new XElement("numberOfEmployees",
                                        new XElement("numberOfEmployeesType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().numberOfEmployeesType)),
                                        new XElement("numberOfEmployeesWithinTheCompanyRangeFrom", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().numberOfEmployeesWithinTheCompanyRangeFrom))
                                    ),
                                    new XElement("companyLegalForm", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyLegalForm)),

                                    new XElement("companyListedStockExchange", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyListedStockExchange)),

                                    CorreoArray.Count == 0 ? null : CorreoArray,

                                    TelefonoArray.Count == 0 ? null : TelefonoArray,

                                    new XElement("contact",
                                         new XElement("contactWebsiteAddress", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().contactWebsiteAddress))
                                    ),
                                    new XElement("incorporationDate", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().incorporationDate)),
                                    new XElement("registrationDate", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().registrationDate)),
                                    new XElement("companyMainSector",
                                        new XElement("companyMainSectorCode", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyMainSectorCode)),
                                        new XElement("companyMainSectorCodeType", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyMainSectorCodeType)),
                                        new XElement("companyMainSectorDescription", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyMainSectorDescription))
                                    ),
                                    new XElement("companyNature", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().companyNature))
                                //new XElement("companyNatureFromProvider", (bussines.FirstOrDefault() ?? "").companyNatureFromProvider)
                                ),
                                balances.Select(balance =>
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
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueTotalAssets),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeTotalAssets)
                                            ),
                                            //new XElement("keyFigure",
                                            //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeFixedAssets), 
                                            //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueFixedAssets), 
                                            //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeFixedAssets)
                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueFixedAssets == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeFixedAssets),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueFixedAssets),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeFixedAssets)
                                            ),
                                            //new XElement("keyFigure",
                                            //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeCurrentAssets),
                                            //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCurrentAssets), 
                                            //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeCurrentAssets)
                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCurrentAssets == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeCurrentAssets),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCurrentAssets),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeCurrentAssets)
                                            ),

                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueNonCurrentAssets == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeNonCurrentAssets),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueNonCurrentAssets),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeNonCurrentAssets)
                                            ),

                                            //new XElement("keyFigure",
                                            //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeTotalEquityLiabilities), 
                                            //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCodeTotalEquityLiabilities), 
                                            //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeCodeTotalEquityLiabilities)
                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCodeTotalEquityLiabilities == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeTotalEquityLiabilities),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueCodeTotalEquityLiabilities),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeCodeTotalEquityLiabilities)
                                            ),
                                            //new XElement("keyFigure",
                                            //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquity),
                                            //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquity),
                                            //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquity)
                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquity == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquity),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquity),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquity)
                                            ),

                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueLTLiabilities == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeLTLiabilities),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueLTLiabilities),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeLTLiabilities)
                                            ),

                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueSTLiabilities == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeSTLiabilities),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueSTLiabilities),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeSTLiabilities)
                                            ),

                                            //new XElement("keyFigure",
                                            //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityPaiduPCapital), 
                                            //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityPaiduPCapital), 
                                            //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityPaiduPCapital)
                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityPaiduPCapital == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityPaiduPCapital),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityPaiduPCapital),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityPaiduPCapital)
                                            ),
                                            //new XElement("keyFigure",
                                            //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityRevenueSales), 
                                            //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityRevenueSales), 
                                            //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityRevenueSales)
                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityRevenueSales == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityRevenueSales),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityRevenueSales),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityRevenueSales)
                                            ),
                                            //new XElement("keyFigure",
                                            //    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityNetResult), 
                                            //    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityNetResult), 
                                            //    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityNetResult)
                                            balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityNetResult == 0 ? null :
                                                new XElement("keyFigure",
                                                    new XElement("keyFigureItemCode", balance.CR_FD_keyFigures_keyFigure_keyFigureItemCodeEquityNetResult),
                                                    new XElement("keyFigureItemValue", balance.CR_FD_keyFigures_keyFigure_keyFigureItemValueEquityNetResult),
                                                    new XElement("keyFigureUnitPrefixCode", balance.CR_FD_keyFigures_keyFigure_keyFigureUnitPrefixCodeEquityNetResult)
                                            )

                                        )

                               
                                    )
                                ),
                        // SERA UNA LISTA
                                 functions.Select(function =>
                                     new XElement("function",
                                        new XElement("functionType", function.functionType),
                                        new XElement("nameOfPerson", function.nameOfPerson)
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

                                 legalEvents.Select(legalEvent =>
                                   legalEvent.legalEvent == null ? null : new XElement("legalEvent",
                                                                new XElement("event", legalEvent.legalEvent),
                                                                new XElement("sourceEvent", legalEvent.sourceEvent),
                                                                new XElement("endDate", legalEvent.endDate),
                                                                new XElement("startDate", legalEvent.startDate))
                                 ),

                                new XElement("sourceEvaluation",
                                    new XElement("sourceEvaluationOriginalPaymentExperience", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationOriginalPaymentExperience)),
                                    new XElement("sourceEvaluationSimplePaymentExperience", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationSimplePaymentExperience)),
                                    new XElement("sourceEvaluationExtendedPaymentExperience", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationExtendedPaymentExperience)),
                                    new XElement("sourceEvaluationCreditAdviceAmount", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationCreditAdviceAmount)),
                                    new XElement("sourceEvaluationCreditAdviceExplanation", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationCreditAdviceExplanation)),
                                    new XElement("sourceEvaluationMaximumCreditAdviceAmount", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationMaximumCreditAdviceAmount)),
                                    //new XElement("sourceEvaluationMaximumSingleCreditAdviceAmount", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationMaximumSingleCreditAdviceAmount)),
                                    //new XElement("sourceEvaluationCurrency", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationCurrency)),
                                    bMaximumSingleCreditAdviceAmount == false ? null : new XElement("sourceEvaluationMaximumSingleCreditAdviceAmount", bussines.FirstOrDefault().sourceEvaluationMaximumSingleCreditAdviceAmount),
                                    bMaximumSingleCreditAdviceAmount == false ? null : new XElement("sourceEvaluationCurrency", bussines.FirstOrDefault().sourceEvaluationCurrency),
                                    new XElement("sourceRating", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceRating)),
                                    new XElement("sourceRatingRange", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceRatingRange)),
                                    new XElement("sourceRatingComments", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceRatingComments)),
                                    new XElement("sourceEvaluationComments", (bussines.FirstOrDefault() == null ? "" : bussines.FirstOrDefault().sourceEvaluationComments))
                              
                               relateds.Select(related =>
                                  new XElement("relatedCompany",
                                       new XElement("relatedName", related.relatedName == null ? "" : related.relatedName),
                                       new XElement("relatedTaxReg", related.relatedTaxReg == null ? "" : related.relatedTaxReg),
                                       new XElement("relatedCountry", related.relatedCountry == null ? "" : related.relatedCountry),
                                       new XElement("dateInc", related.dateInc == null ? "" : related.dateInc),
                                       new XElement("relationType", related.relationType == null ? "" : related.relationType)
                                   )
                               )
                            )
                        );
                    }
                }
                
                */
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.IsSuccess = false;
            }
            return response;
        }

    }
}
