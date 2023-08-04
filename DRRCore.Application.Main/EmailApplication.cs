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
        private readonly IFileManager _fileManager;
        private readonly IAttachmentsNotSendDomain _attachmentsNotSendDomain;
        private IMapper _mapper { get; }
        private IEmailHistoryDomain _emailHistoryDomain;

        public EmailApplication(ILogger logger,IMailSender mailSender ,IMapper mapper, IEmailHistoryDomain emailHistoryDomain,IFileManager fileManager, IAttachmentsNotSendDomain attachmentsNotSendDomain )
        {
            _logger = logger;
            _mailSender = mailSender;
            _mapper = mapper;
            _emailHistoryDomain = emailHistoryDomain;
            _fileManager = fileManager;
            _attachmentsNotSendDomain = attachmentsNotSendDomain;
        }

        public async Task<Response<bool>> SendMailAsync(EmailDataDTO emailDataDto)
        {
            var response=new Response<bool>();

            try
            {
                var result = await _mailSender.SendMailAsync(_mapper.Map<EmailValues>(emailDataDto));
                if (!result)
                {
                    foreach (var item in emailDataDto.Attachments)
                    {
                        item.Path = await UploadFile(item);
                    }
                    response.Data = await _emailHistoryDomain.AddAsync(_mapper.Map<EmailHistory>(emailDataDto));
                }
                _logger.LogInformation(Messages.MailSuccessSend);
                response.IsSuccess = true;


            }
            catch (Exception ex)
            {
                await _emailHistoryDomain.AddAsync(_mapper.Map<EmailHistory>(emailDataDto));
                response.IsSuccess = false;
                response.Message= string.Format(Messages.ExceptionMessage,ex.Message);
                _logger.LogError(response.Message);

            }
            return response;
        }
        /*Reenvio de mail*/
        public async Task<Response<bool>> ReSendMailAsync()
        {
            var response = new Response<bool>();

            try
            {
                var mailsNotSend = await _emailHistoryDomain.GetEmailNotSendAsync();//obtiene la lista de los mails no enviados - success 0

                foreach(var mail in mailsNotSend)
                {
                    var attachmentsNotSend = await _attachmentsNotSendDomain.GetByEmailHistoryIdAsync(mail.IdEmailHistory);//obtiene el adjunto no enviado por medio del id
                    foreach (var attachment in attachmentsNotSend)
                    {
                        var file = await DownloadFile(attachment.AttachmentsUrl);
                    }
                    var result = await _mailSender.SendMailAsync(_mapper.Map<EmailValues>(mail));
                    
                }
                
                response.IsSuccess = true;            
            }
            catch (Exception ex) 
            {

                response.IsSuccess = false;
                response.Message = string.Format(Messages.ExceptionMessage, ex.Message);
                _logger.LogError(response.Message);
            }
            return response;
        }


        private async Task<string> UploadFile(AttachmentDto attachmentDto)
        {
            return await _fileManager.UploadFile(new MemoryStream(attachmentDto.Content),attachmentDto.FileName);
        }
        private async Task<MemoryStream> DownloadFile(string path)
        {
            return await _fileManager.DownloadFile(path);
        }
    }
}
