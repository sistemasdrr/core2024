namespace DRRCore.Application.DTO.Email
{
    public class EmailDataDTO
    {

        public string User { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public List<string> To { get; set; } = new List<string>();
        public List<string> CC { get; set; } = new List<string>();
        public List<string> CCO { get; set; } = new List<string>();
        public string Subject { get; set; }=string.Empty;
        public string BodyHTML { get; set; } = string.Empty;
        public bool IsBodyHTML { get;set; }
        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();    

    }
    public class AttachmentDto
    {
        public string FileName { get; set;}=string.Empty;
        public byte[] Content { get; set;} = new byte[0];
        public string Path { get; set;}=string.Empty;   
    }
}
