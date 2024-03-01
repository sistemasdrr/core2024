using AspNetCore.Reporting;

namespace DRRCore.Transversal.Common
{
    public static class StaticFunctions
    {
        public static DateTime? VerifyDate(string date)
        {
            DateTime result;
            if (string.IsNullOrEmpty(date)) return null;
            return DateTime.TryParse(date, out result) ? result : null;
        }

        public static string DateTimeToString(DateTime? date)
        {
            return date?.ToString("dd/MM/yyyy");
        }
        public static ReportRenderType GetReportRenderType(string typeFile)
        {
            switch (typeFile)
            {
                case "pdf":
                    return ReportRenderType.Pdf;
                case "excel":
                    return ReportRenderType.ExcelOpenXml;
                case "word":
                    return ReportRenderType.WordOpenXml;
                default:
                    return ReportRenderType.Null;
            }
        }
        public static string GetContentType(ReportRenderType reportRenderType)
        {
            switch (reportRenderType)
            {
                case ReportRenderType.Pdf:
                    return "application/pdf";
                case ReportRenderType.ExcelOpenXml:
                    return "application/vnd.ms-excel";
                case ReportRenderType.WordOpenXml:
                    return "application/msword";
                default:
                    return string.Empty;
            }
        }
        public static string FileExtension(ReportRenderType reportRenderType)
        {
            switch (reportRenderType)
            {
                case ReportRenderType.Pdf:
                    return ".pdf";
                case ReportRenderType.Excel:
                    return "xls";
                case ReportRenderType.Word:
                    return "application/msword";
                default:
                    return string.Empty;
            }
        }
    }
}
