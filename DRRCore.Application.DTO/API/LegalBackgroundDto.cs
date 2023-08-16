using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Application.DTO.API
{
    public class LegalBackgroundDto
    {
        public string LegalStatus { get; set; } = string.Empty;
        public string IncorporationDate { get; set; }=string.Empty;
        public string OperationStartDate { get; set; }=string.Empty;    
        public string RegisterPlace { get; set;}=string.Empty;
        public string NotaryOffice { get;set; }=string.Empty;
        public string DurationTime { get; set;}=string.Empty;
        public string RegistrationFolio { get; set; }=string.Empty;
        public CurrencyAmountDto CurrencyPaidInCapital { get; set; } = new CurrencyAmountDto();
        public string LastCapitalIncreaseDate { get; set; }=string.Empty;
        public CurrencyAmountWithDateDto ShareholdersEquity { get; set; } = new CurrencyAmountWithDateDto();
        public string ShareClass { get; set; }=string.Empty;
        public bool StockExchangeListed { get;set; }=false;
        public CurrencyAmountDto CurrentExchangeRate { get; set; } = new CurrencyAmountDto();
        public string Membership { get; set; }= string.Empty;
        public string Comments { get; set; } = string.Empty;

    }
}
