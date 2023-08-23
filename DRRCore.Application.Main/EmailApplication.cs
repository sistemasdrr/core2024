using AutoMapper;
using DRRCore.Application.DTO.Email;
using DRRCore.Application.Interfaces;
using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.AspNetCore.Http.Features;
using System.Globalization;

namespace DRRCore.Application.Main
{
    public class EmailApplication : IEmailApplication
    {
        private readonly ILogger _logger;
        private readonly IMailSender _mailSender;
        private readonly IFileManager _fileManager;
        private readonly IAttachmentsNotSendDomain _attachmentsNotSendDomain;
        private readonly IEmailConfigurationDomain _emailConfigurationDomain;
        private IMapper _mapper { get; }
        private IEmailHistoryDomain _emailHistoryDomain;

        public EmailApplication(ILogger logger, IMailSender mailSender, IMapper mapper,
            IEmailHistoryDomain emailHistoryDomain, IFileManager fileManager, 
            IAttachmentsNotSendDomain attachmentsNotSendDomain, IEmailConfigurationDomain emailConfigurationDomain)
        {
            _logger = logger;
            _mailSender = mailSender;
            _mapper = mapper;
            _emailHistoryDomain = emailHistoryDomain;
            _fileManager = fileManager;
            _attachmentsNotSendDomain = attachmentsNotSendDomain;
            _emailConfigurationDomain = emailConfigurationDomain;
           
        }

        public async Task<Response<bool>> SendMailAsync(EmailDataDTO emailDataDto)
        {
            string body = string.Empty;
            var response = new Response<bool>();
            try
            {
                if (emailDataDto.IsBodyHTML)
                {
                    body =await GetBodyHtml(emailDataDto);
                }
                emailDataDto.BodyHTML = body;
                var result = await _mailSender.SendMailAsync(_mapper.Map<EmailValues>(emailDataDto));

                if (!result)
                {
                    foreach (var item in emailDataDto.Attachments)
                    {
                        item.Path = await UploadFile(item);
                    }
                }
                var emailHistory = _mapper.Map<EmailHistory>(emailDataDto);   
                emailHistory.Success = result;
                response.Data = await _emailHistoryDomain.AddAsync(emailHistory);
               
                _logger.LogInformation(Messages.MailSuccessSend);
                response.IsSuccess = true;
                response.Data = result;


            }
            catch (Exception ex)
            {             
                response.IsSuccess = false;
                response.Message = string.Format(Messages.ExceptionMessage, ex.Message);
                _logger.LogError(response.Message);

            }
            return response;
        }

        private async Task<string> GetBodyHtml(EmailDataDTO emailDataDto)
        {
            var emailConfiguration = await _emailConfigurationDomain.GetByNameAsync(emailDataDto.EmailKey);
            return string.Empty;
        }

        /*Reenvio de mail*/
        public async Task<Response<bool>> ReSendMailAsync()
        {
            var response = new Response<bool>();
            List<Attachment> listAttachments;

            var mailsNotSend = await _emailHistoryDomain.GetEmailNotSendAsync();//obtiene la lista de los mails no enviados - success 0

            foreach (var mail in mailsNotSend)
            {
                try
                {
                    var attachmentsNotSend = await _attachmentsNotSendDomain.GetByEmailHistoryIdAsync(mail.IdEmailHistory);//obtiene el adjunto no enviado por medio del id
                    listAttachments = new List<Attachment>();
                    foreach (var attachment in attachmentsNotSend) //Recorre todos los adjuntos
                    {
                        var newAttachment= new Attachment { 
                         FileName = attachment.FileName
                        };
                        newAttachment.StreamBase64= await DownloadFile(attachment.AttachmentsUrl);
                        listAttachments.Add(newAttachment);
                    }
                    var emailRequest = _mapper.Map<EmailValues>(mail);
                    emailRequest.Attachments = listAttachments;
                    var result = await _mailSender.SendMailAsync(emailRequest);
                    if (result)
                    {
                        mail.Success = true;
                        await _emailHistoryDomain.UpdateAsync(mail);
                        foreach (var item in attachmentsNotSend)
                        {
                            await DeleteFile(item.FileName);
                        }
                    }                   
                }
                catch(Exception ex) 
                {
                    response.IsSuccess = false;
                    response.Message = string.Format(Messages.ExceptionMessage, ex.Message);
                    _logger.LogError(response.Message);
                    continue;
                }
            }            
            return response;
        }


        private async Task<string> UploadFile(AttachmentDto attachmentDto)
        {
            return await _fileManager.UploadFile(new MemoryStream(Convert.FromBase64String(attachmentDto.Content)), attachmentDto.FileName);
        }
        private async Task<string> DownloadFile(string remoteFilePath)
        {
            return await _fileManager.DownloadFile(remoteFilePath);
        }
        private async Task<bool> DeleteFile(string path)
        {
            return await _fileManager.DeleteFile(path);
        }
    }
}
