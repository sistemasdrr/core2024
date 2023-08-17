namespace DRRCore.Application.DTO.API
{
    public class AntecedentesDataDto
    {
        public string? CodigoInforme { get; set; }
        public string? FechaConstitucion { get; set; }
        public string? InicioActividades { get; set; }
        public string? Duracion { get; set; }
        public string? RegistradaEn { get; set; }
        public string? Notaria { get; set; }
        public string? RegistrosPublicos { get; set; }
        public CapitalPagadoDto? CapitalPagadoActual { get; set; }
        public string? Origen { get; set; }
        public AumentoDto? FechaAumento { get; set; }
        public string? CotizaEnBolsa { get; set; }
        public string? AumentoPor { get; set; }
        public TipoCambioDto? ActualTipoCambio { get; set; }
        public RRPPDto? ConsultaRRPP { get; set; }
        public string? Antecedentes { get; set; }
        public string? Comentarios { get; set; }
        public string? Flag { get; set; } // 1-0
      
        public string? DescripcionGeneral { get; set; }


    }
    public class CapitalPagadoDto
    {
        public int Cantidad { get; set; }
        public string? Moneda { get; set; }
        public string? Descripcion { get; set; }
        public string? Comentario { get; set; }
    }
    public class AumentoDto
    {
        public string? Fecha { get; set; }
        public string? Descripcion { get; set; }
    }
    public class TipoCambioDto
    {
        public decimal? Monto { get; set; }
        public string? Moneda { get; set; }
        public string? Descripcion { get; set; }
        public string? Observacion { get; set; }
    }
    public class RRPPDto
    {
        public string? Fecha { get; set; }
        public string? Descripcion { get; set; }
    }
    
}

