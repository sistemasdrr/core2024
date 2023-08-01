using AutoMapper;
using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Infraestructure.Repository.DataProfiles
{
    public class EmailProfile:Profile
    {
        public EmailProfile() {
            CreateMap<TUsuario, EmailUser>()
                .ForMember(dest => dest.UserCode, opt => opt?.MapFrom(src => src.UsCodigo))
                .ForMember(dest => dest.Identifier, opt => opt?.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.UserName, opt => opt?.MapFrom(src => src.UsNombre ?? string.Empty))
                .ForMember(dest => dest.LoginName, opt => opt?.MapFrom(src => src.UsLogin ?? string.Empty));
                
        }
    }
}
