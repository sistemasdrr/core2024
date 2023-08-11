namespace DRRCore.Application.DTO.API
{
    public class InformationDto
    {
        public string CorrectCompanyName { get; set; }=string.Empty;
        public string TradeName { get; set; }=string.Empty;
        public string TaxpayerRegistration { get; set;}=string.Empty;
        public string Main_Address { get; set; }=string.Empty;
        public string CityProvincie { get; set; }=string.Empty;
        public string PostalCode { get; set; }=string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; }= string.Empty;
        public string Email { get; set;}=string.Empty;
        public string WebUrl { get; set; }=string.Empty;
        public string Comment { get; set;}=string.Empty;
    }

}
