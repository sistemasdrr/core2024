using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DRRCore.Application.DTO.API
{
    public class SearchRequestDto
    {
        [JsonPropertyName("DRR_REQCLI")]
        [JsonRequired]
        [JsonPropertyOrder(0)]
        public string SearchText { get; set; } = string.Empty;
        [JsonPropertyName("DRR_ISOCOU")]
        [JsonPropertyOrder(1)]
        public string IsoCountry { get; set; }= string.Empty;
        [JsonPropertyName("DRR_PRIORI")]
        [JsonPropertyOrder(2)]
        public string Priority { get; set; } = string.Empty;
    }
}
