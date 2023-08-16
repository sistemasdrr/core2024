using Microsoft.VisualBasic;
using System;

namespace DRRCore.Application.DTO.API
{
    public class SummaryDto
    {
        public string InscriptionYear { get; set; } = string.Empty;
        public CurrencyAmountDto CapitalStock { get; set; } = new CurrencyAmountDto();
        public CurrencyAmountWithDateDto ShareholdersEquity { get; set; } = new CurrencyAmountWithDateDto();
        public CurrencyAmountWithDateDto AnnRevenues { get; set; } = new CurrencyAmountWithDateDto();
        public CurrencyAmountWithDateDto Profits { get; set; } = new CurrencyAmountWithDateDto();
        public int Employees { get; set; } = 0;
        public string ChiefExecutive { get; set; } = string.Empty;
        public SummaryValueDetailDto FinalSituation { get; set; } = new SummaryValueDetailDto();
        public SummaryValueDetailDto Disposition { get; set; } = new SummaryValueDetailDto();
        public SummaryValueDetailDto PaymentsPolicy { get; set; } = new SummaryValueDetailDto();
        public SummaryValueDetailDto Credit { get; set; } = new SummaryValueDetailDto();
        public string Description { get; set; } = string.Empty; 
    }
    
   
    public class SummaryValueDetailDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}



 