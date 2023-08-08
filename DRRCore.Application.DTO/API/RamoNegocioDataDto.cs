using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Application.DTO.API
{
    public class RamoNegocioDataDto
    {
        public string? CodigoInforme { get; set; }
        public int? CodigoCupon { get; set; }
        public int? SectorCodigo { get; set; }
        public string? SectorCodigo_EE { get; set; }
        public int? RamoCodigo { get; set; }
        public string? RamoCodigo_EE { get; set; }
        public ImportacionDto? Importa { get; set; }
        public ExportacionDto? Exporta { get; set; }
        public VentaContadoDto? VentaContado { get; set; }
        public VentaCreditoDto? VentaCredito { get; set; }
        public string? TerritorioVentas { get; set; }
        public string? VentaExterior { get; set; }
        public string? CompraNacional { get; set; }
        public string? CompraExtranjero { get; set; }
        public int? CantidadTrabajadores { get; set; }
        public string? TitularidadLocal { get; set; }
        public string? ObservacionLocal { get; set; }
        public string? AreaLocal { get; set; }
        public string? PlantasLocal { get; set; }
        public string? AlmacenesLocal { get; set; }
        public string? DomicilioAnterior { get; set; }
        public string? Actividades { get; set; }
        public string? Comentarios { get; set; }
        public string? Tabulado { get; set; }
        public int? Flag { get; set; }
        public InformeDto? DatosInforme { get; set; }
        public UsuarioDto? Usuario { get; set; }
        public string? DescripcionGeneral { get; set; } 

    }
    public class ImportacionDto
    {
        public string? importan { get; set; }
        public string? paises { get; set; }
    }
    public class ExportacionDto
    {
        public string? exportan { get; set; }
        public string? paises { get; set; }
    }
    public class VentaContadoDto
    {
        public int? cantidad { get; set; }
        public string? descripcion { get; set; }
        public string? observacion { get;}
    }
    public class VentaCreditoDto
    {
        public int? cantidad { get; set; }
        public string? descripcion { get; set; }
        public string? observacion { get; }
    }
}
