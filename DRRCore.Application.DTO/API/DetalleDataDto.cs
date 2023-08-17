namespace DRRCore.Application.DTO.API
{
    public class DetalleDataDto
    {
        public string? CodigoInforme { get; set; }
        public string? ComentarioIdentificacion { get; set; }
        public string? ComentarioReputacion { get; set; }
        public string? ComentarioPrensa { get; set; }
        public int? Flag { get; set; }
        public string? ImprimeComentarioPrensa { get; set; } //0-1
        public string? ListaClinton { get; set; } //si-no
        public SBSDto? SBSDatos { get; set; } 
        public string? Litigios { get; set; } 
        public string? AntecedentesCrediticios { get; set; } 
        public string? SBSAvalesRecibidos { get; set; } 
        public string? SBSAvalesEntregados { get; set; } 
        public string? ComentarioSupervisor { get; set; } 
     
        public string? DescripcionGeneral { get; set; }

    }
    public class SBSDto
    {
        public string? Fecha { get; set;}
        public decimal? TipoCambio { get; set; }
        public string? Comentario { get; set; }
        public string? Banquero { get; set; }
        public string? Creditos { get; set; }
    }
}
