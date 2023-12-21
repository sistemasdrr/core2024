namespace DRRCore.Application.DTO.Core.Response
{
    public class GetImagentDto
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public MemoryStream File { get; set; } = new MemoryStream();
    }
}
