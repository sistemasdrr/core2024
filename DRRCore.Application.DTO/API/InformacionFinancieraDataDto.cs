using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public InformeDto? DatosInforme { get; set; }
        public UsuarioDto? Usuario { get; set; }
        public string? DescripcionGeneral { get; set; }
    }
    public class EntrevistaDto
    {
        public string? nombre { get; set; }
        public string? cargo { get; set; }
        public int? grado { get; set; }
        public string? comentario { get; set; }
    }
    public class SituacionFinancieraDto
    {
        public int? codigo { get; set; }
        public string? comentario { get; set; }
    }

}
