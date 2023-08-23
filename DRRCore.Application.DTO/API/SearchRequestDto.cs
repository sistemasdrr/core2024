namespace DRRCore.Application.DTO.API
{
    public class SearchRequestDto
    {
        public string SearchText { get; set; } = string.Empty;
        public string IsoCountry { get; set; }= string.Empty;
    }
}
