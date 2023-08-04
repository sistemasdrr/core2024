using AutoMapper;
using DRRCore.Application.DTO.Email;
using DRRCore.Application.DTO.Web;
using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Transversal.Common;

namespace DRRCore.Transversal.Mapper.Profiles.Email
{
    public class EmailProfile:Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailDataDTO, EmailValues>()
                .ForMember(dest => dest.FromEmail, opt => opt?.MapFrom(src => src.From))
                .ForMember(dest => dest.ToEmail, opt => opt?.MapFrom(src => string.Join(';',src.To)))
                .ForMember(dest => dest.CcEmail, opt => opt?.MapFrom(src => string.Join(';', src.CC)))
                .ForMember(dest => dest.BccEmail, opt => opt?.MapFrom(src => string.Join(';', src.CCO)))
                .ForMember(dest => dest.Subject, opt => opt?.MapFrom(src => src.Subject))
                .ForMember(dest => dest.IsHtml, opt => opt?.MapFrom(src => src.IsBodyHTML))
                .ForMember(dest => dest.Body, opt => opt?.MapFrom(src => src.BodyHTML))
                .ForMember(dest => dest.Attachments, opt => opt?.MapFrom(src => src.Attachments))
               .ReverseMap();

            CreateMap<AttachmentDto, Attachment>()
                .ForMember(dest => dest.FileName, opt => opt?.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Stream, opt => opt?.MapFrom(src => new MemoryStream(src.Content)))               
               .ReverseMap();

            CreateMap<EmailDataDTO, EmailHistory>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.FromMails, opt => opt.MapFrom(src => src.From))
                .ForMember(dest => dest.ToMails, opt => opt.MapFrom(src => string.Join(';', src.To)))
                .ForMember(dest => dest.CcMails, opt => opt.MapFrom(src => string.Join(';', src.CC)))
                .ForMember(dest => dest.CcoMails, opt => opt.MapFrom(src => string.Join(';', src.CCO)))
                .ForMember(dest => dest.Htmlbody, opt => opt.MapFrom(src => src.BodyHTML))
                .ForMember(dest => dest.InsertUser, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.AttachmentsNotSends, opt => opt.MapFrom(src => src.Attachments))
                .ReverseMap();
            CreateMap<AttachmentDto, AttachmentsNotSend>()
              .ForMember(dest => dest.AttachmentsUrl, opt => opt?.MapFrom(src => src.Path))            
             .ReverseMap();

        }
    }
}
