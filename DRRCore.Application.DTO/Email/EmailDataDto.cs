using DRRCore.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Application.DTO.Email
{
    internal class EmailDataDTO
    {

        public string User { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public List<string>? To { get; set; }
        public string CC { get; set; } = string.Empty;
        public string CCO { get; set; } = string.Empty;
        public string BodyHTML { get; set; } = string.Empty;
        public FileManager? Attachment { get; set; }    

    }
}
