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
    }
}
