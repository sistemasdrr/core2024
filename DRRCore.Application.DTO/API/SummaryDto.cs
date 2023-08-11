using Microsoft.VisualBasic;
using System;

namespace DRRCore.Application.DTO.API
{
    public class SummaryDto
    {
        public string InscriptionYear { get; set; } = string.Empty;
        public CapitalStockDto CapitalStock { get; set; } = new CapitalStockDto();
        public SummaryAmountDto ShareholdersEquity { get; set; } = new SummaryAmountDto();
        public SummaryAmountDto AnnRevenues { get; set; } = new SummaryAmountDto();
        public SummaryAmountDto Profits { get; set; } = new SummaryAmountDto();
        public int Employees { get; set; } = 0;
        public string ChiefExecutive { get; set; } = string.Empty;
        public SummaryValueDetailDto FinalSituation { get; set; } = new SummaryValueDetailDto();
        public SummaryValueDetailDto Disposition { get; set; } = new SummaryValueDetailDto();
        public SummaryValueDetailDto PaymentsPolicy { get; set; } = new SummaryValueDetailDto();
        public SummaryValueDetailDto Credit { get; set; } = new SummaryValueDetailDto();
    }
    public class CapitalStockDto
    {
        public string IsoCurrency { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Comment { get; set; } = string.Empty;

    }
    public class SummaryAmountDto
    {
        public string IsoCurrency { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string LastInformationDate { get; set; } = string.Empty;
    }
    public class SummaryValueDetailDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}



 