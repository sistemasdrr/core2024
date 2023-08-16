using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Application.DTO.API
{
    public class CurrencyAmountDto
    {
        public string IsoCurrency { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
    public class CurrencyAmountWithDateDto : CurrencyAmountDto
    {
        public string LastInformationDate { get; set; } = string.Empty;
    }
}
