using System.Text.Json.Serialization;

namespace DRRCore.Application.DTO.API
{
    public class ExecutiveShareholderDto
    {
        [JsonPropertyName("DRR_EXECUT_NAAMEE")]
        public string Name { get; set; }= string.Empty;
        [JsonPropertyName("DRR_EXECUT_TITTLE")]
        public string Title { get;set; }= string.Empty;
        [JsonPropertyName("DRR_EXECUT_SINDAT")]
        public string SinceDate { get; set; } = string.Empty;
    }
}
