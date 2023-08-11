using System;

namespace DRRCore.Application.DTO.API
{
    public class RequestClientDto
    {
        public string Subscriber { get; set; }=string.Empty;
        public string RequestDate { get;set; }=string.Empty;
        public string Token { get; set; }=string.Empty;
        public string LastReportDate { get; set; }=string.Empty;
        public string ClientReference { get; set;}=string.Empty;
        public string Priority { get; set; }=string.Empty;
        public string Request { get; set; }=string.Empty;   
    }
}
