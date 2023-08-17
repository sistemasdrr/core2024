namespace DRRCore.Application.DTO.API
{
    public class InformacionFinancieraDataDto
    {
        public string? CodigoInforme { get; set; }
        public int? CodigoCupon { get; set; }
        public EntrevistaDto? Entrevista { get; set; }
        public string? Tabulado { get; set; }
        public string? Auditoria { get; set; }
        public SituacionFinancieraDto? Situacion { get; set; }
        public string? Activos { get; set; }
        public string? UsuarioAnalista { get; set; }
        public string? ComentarioAnalista { get; set; }
        public int? Flag { get; set; }
      
        public string? DescripcionGeneral { get; set; }
    }
    public class EntrevistaDto
    {
        public string? Nombre { get; set; }
        public string? Cargo { get; set; }
        public int? Grado { get; set; }
        public string? Comentario { get; set; }
    }
    public class SituacionFinancieraDto
    {
        public int? Codigo { get; set; }
        public string? Comentario { get; set; }
    }

}
