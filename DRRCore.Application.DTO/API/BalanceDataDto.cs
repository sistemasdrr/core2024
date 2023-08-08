using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public decimal cajaBanco { get; set; }
        public decimal porCobrar { get; set; }
        public decimal inventario{ get; set; }
        public decimal otrosActivosCorrientes { get; set; }
        public decimal ActivoCorriente { get; set; }
        public decimal ActivoCorrienteFijo { get; set; }
        public decimal otrosActivosNCorrientes { get; set; }
        public decimal TotalActivo { get; set; }
    }
    public class PasivoDto 
    {
        public decimal bancos { get; set; }
        public decimal otrosPasivosCorrientes { get; set; }
        public decimal PasivoCorriente { get; set; }
        public decimal LargoPlazo { get; set; }
        public decimal otrosPasivosNoCorrientes { get; set; }
        public decimal totalPasivo { get; set; }
    }
    public class PatrimonioDto
    {
        public decimal? capital { get; set;}
        public decimal? reservas { get; set;}
        public decimal? utilidades { get; set;}
        public decimal? otros { get; set;}
        public decimal? totalPatrimonio { get; set;}
        public decimal? totalPasivoPatrimonio { get; set;}
    }
}
