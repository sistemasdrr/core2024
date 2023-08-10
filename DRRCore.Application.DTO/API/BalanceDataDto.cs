namespace DRRCore.Application.DTO.API
{
    public class BalanceDataDto
    {
        public string? CodigoInforme { get; set; }
        public int? CodigoCupon { get; set; }
        public string? CodigoBalanceClase { get; set; }
        public string? Fecha { get; set; }
        public int? Tipo { get; set; }
        public string? Periodo { get; set; }
        public string? Moneda { get; set; }
        public decimal? TasaCambio { get; set; }
        public decimal? Ventas { get; set; }
        public decimal? Utilidad { get; set; }
        public ActivoDto? Activos { get; set; }
        public PasivoDto? Pasivos { get; set; }
        public PatrimonioDto? Patrimonios { get; set; }
        public int? Flag { get; set; }
        public decimal? Cierre {get; set;}
        public decimal? ActivoTotalANC {get; set;}
        public decimal? PasivoTotalANC {get; set;}
        public InformeDto? Informe { get; set; }
        public UsuarioDto? Usuario { get; set; }
        public string? DescripcionGeneral { get; set; }
    }
    public class ActivoDto
    {
        public decimal CajaBanco { get; set; }
        public decimal PorCobrar { get; set; }
        public decimal Inventario{ get; set; }
        public decimal OtrosActivosCorrientes { get; set; }
        public decimal ActivoCorriente { get; set; }
        public decimal ActivoCorrienteFijo { get; set; }
        public decimal OtrosActivosNCorrientes { get; set; }
        public decimal TotalActivo { get; set; }
    }
    public class PasivoDto 
    {
        public decimal Bancos { get; set; }
        public decimal OtrosPasivosCorrientes { get; set; }
        public decimal PasivoCorriente { get; set; }
        public decimal LargoPlazo { get; set; }
        public decimal OtrosPasivosNoCorrientes { get; set; }
        public decimal TotalPasivo { get; set; }
    }
    public class PatrimonioDto
    {
        public decimal? Capital { get; set;}
        public decimal? Reservas { get; set;}
        public decimal? Utilidades { get; set;}
        public decimal? Otros { get; set;}
        public decimal? TotalPatrimonio { get; set;}
        public decimal? TotalPasivoPatrimonio { get; set;}
    }
}
