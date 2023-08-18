using System;
using System.Text.Json.Serialization;

namespace DRRCore.Application.DTO.API
{
    public class RequestClientDto
    {
        [JsonPropertyName("DRR_REQCLI_SUBSCR")]
        public string Subscriber { get; set; }=string.Empty;
        [JsonPropertyName("DRR_REQCLI_REQDAT")]
        public string RequestDate { get;set; }=string.Empty;
        [JsonPropertyName("DRR_REQCLI_TOKENN")]
        public string Token { get; set; }=string.Empty;
        [JsonPropertyName("DRR_REQCLI_LASTRD")]
        public string LastReportDate { get; set; }=string.Empty;
        [JsonPropertyName("DRR_REQCLI_CLIREF")]
        public string ClientReference { get; set;}=string.Empty;
        [JsonPropertyName("DRR_REQCLI_PRIORI")]
        public string Priority { get; set; }=string.Empty;
        [JsonPropertyName("DRR_REQCLI_REQUES")]
        public string Request { get; set; }=string.Empty;
        [JsonPropertyName("DRR_REQCLI_ENVIRO")]
        public string Environment { get; set; } = string.Empty;
    }
}
