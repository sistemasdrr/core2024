namespace DRRCore.Application.DTO.API
{
    public class CabeceraDataDto
    {
        public string? CodigoInforme { get; set; }
        public string? NombreInforme { get; set; }
        public string? SiglasInforme { get; set; }
        public string? TipoInforme { get; set; }
        public string? FechaInforme { get; set; }
        public string? IdiomaInforme { get; set; }
        public int? CodigoPais { get; set; }
        public string? PaisNombre { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }
        public string? Ubigeo { get; set; }
        public string? FechaFundacion { get; set; }
        public string? RegistroTributario { get; set; }
        public int? CodigoPersoneriaJuridica { get; set; }
        public string? PersoneriaJuridica { get; set; }
        public string? NumeroCelularEmpresa { get; set; }
        public int? CodigoSituacionRUC { get; set; }
        public string? SituacionRUC { get; set; }
        public int? CodigoRiesgoCrediticio { get; set; }
        public string? RiesgoCrediticio { get; set; }
        public int? CodigoPoliticaPagos { get; set; }
        public string? PoliticaPagos { get; set; }
        public string? TipoPersonaria { get; set; }
        public string? AuditoriaAccion { get; set; }
        public string? AuditoriaFecha { get; set; }
        public string? Flag { get; set; }
        public string? Telefonos { get; set; }
        public string? CorreosElectronicos { get; set; }
        public string? FechaCese { get; set; }
        public int? ComboProveedores { get; set; }
        public string? ComentarioAdicionalProveedores { get; set; }
        public string? FechaCreacion { get; set; }
        public string? InformeEstadoEmpresa { get; set; }
        public InformeDto? Informe { get; set; }
        public UsuarioDto? Usuario { get; set; }
        public string? DescripcionGeneral { get; set; }
    }
    public class InformeDto
    {
        public int? Valor { get; set; }
        public string? Dato { get; set; }
        public int? Indicador { get; set; }
    }
    public class UsuarioDto
    {
        public string? Accion { get; set; }
        public string? Usuario { get; set; }
        public string? Fecha { get; set; }
    }
}
