using AutoMapper;
using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Infraestructure.Repository.DataProfiles
{
    public class WebDataProfile:Profile
    {
        public WebDataProfile()
        {
            CreateMap<ViewConsultaWeb, WebQuery>()
              .ForMember(dest => dest.NombreEmpresa, opt => opt?.MapFrom(src => src.Empresa ?? string.Empty))
              .ForMember(dest => dest.CodigoEmpresaWeb, opt => opt?.MapFrom(src => src.CodigoEmpresa+DateTime.Now.ToString("HHmmss") ?? string.Empty));
          


        }
        private string GetQualityDescription(int calidadCodigo)
        {
            switch (calidadCodigo)
            {
                case 1:
                case 2:
                    return "A";
                case 3:
                case 4:
                    return "B";
                default:
                    return "C";
            }
        }
    }
}
