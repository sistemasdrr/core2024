﻿using System.Text.Json.Serialization;

namespace DRRCore.Application.DTO.API
{
    public class InformationDto
    {
        [JsonPropertyName("DRR_INFORM_COMNAM")]
        public string CorrectCompanyName { get; set; }=string.Empty;
        [JsonPropertyName("DRR_INFORM_TRANAM")]
        public string TradeName { get; set; }=string.Empty;
        [JsonPropertyName("DRR_INFORM_NUMREG")]
        public string TaxpayerRegistration { get; set;}=string.Empty;
        [JsonPropertyName("DRR_INFORM_MAINAD")]
        public string Main_Address { get; set; }=string.Empty;
        [JsonPropertyName("DRR_INFORM_CITPRV")]
        public string CityProvincie { get; set; }=string.Empty;
        [JsonPropertyName("DRR_INFORM_POSCOD")]
        public string PostalCode { get; set; }=string.Empty;
        [JsonPropertyName("DRR_INFORM_COUNTR")]
        public string Country { get; set; } = string.Empty;
        [JsonPropertyName("DRR_INFORM_TPHONE")]
        public string Phone { get; set; }= string.Empty;
        [JsonPropertyName("DRR_INFORM_EEMAIL")]
        public string Email { get; set;}=string.Empty;
        [JsonPropertyName("DRR_INFORM_WEBURL")]
        public string WebUrl { get; set; }=string.Empty;
        [JsonPropertyName("DRR_INFORM_COMMEN")]
        public string Comment { get; set;}=string.Empty;
    }

}
