using AutoMapper;
using DRRCore.Application.DTO.Email;
using DRRCore.Application.Interfaces;
using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Application.Main
{
    public class EmailApplication : IEmailApplication
    {
        private readonly ILogger _logger;
        private readonly IMailSender _mailSender;
        private IMapper _mapper { get; }
        private IEmailHistoryDomain _emailHistoryDomain;

        public EmailApplication(ILogger logger,IMailSender mailSender ,IMapper mapper, IEmailHistoryDomain emailHistoryDomain)
        {
            _logger = logger;
            _mailSender = mailSender;
            _mapper = mapper;
            _emailHistoryDomain = emailHistoryDomain;
        }

        public async Task<Response<bool>> SendMailAsync(EmailDataDTO emailDataDto)
        {
            var response=new Response<bool>();

            try
            {
                var result = await _mailSender.SendMailAsync(_mapper.Map<EmailValues>(emailDataDto));
                if (!result)
                {
                    response.Data = await _emailHistoryDomain.AddAsync(_mapper.Map<EmailHistory>(emailDataDto));
                }               
                response.IsSuccess = true;


            }
            catch (Exception ex)
            {
                await _emailHistoryDomain.AddAsync(_mapper.Map<EmailHistory>(emailDataDto));
                response.IsSuccess = false;
                response.Message= string.Format(Messages.ExceptionMessage,ex.Message);

            }
            return response;
        }
    }
}
