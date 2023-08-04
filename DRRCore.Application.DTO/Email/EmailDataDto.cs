namespace DRRCore.Application.DTO.Email
{
    public class EmailDataDTO
    {

        public string User { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public List<string> To { get; set; } = new List<string>();
        public List<string> CC { get; set; } = new List<string>();
        public List<string> CCO { get; set; } = new List<string>();
        public string BodyHTML { get; set; } = string.Empty;
        public List<byte[]> Attachments { get; set; } = new List<byte[]>();    

    }
}
