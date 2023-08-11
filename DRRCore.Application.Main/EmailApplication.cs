using AutoMapper;
using DRRCore.Application.DTO.Email;
using DRRCore.Application.Interfaces;
using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public EmailApplication(ILogger logger, IMailSender mailSender, IMapper mapper, IEmailHistoryDomain emailHistoryDomain, IFileManager fileManager, IAttachmentsNotSendDomain attachmentsNotSendDomain)
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
            var response = new Response<bool>();

            try
            {
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
                        await DownloadFile(attachment.AttachmentsUrl, newAttachment.Stream);
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
        private async Task DownloadFile(string remoteFilePath, MemoryStream stream)
        {
            await _fileManager.DownloadFile(remoteFilePath, stream);
        }
        private async Task<bool> DeleteFile(string path)
        {
            return await _fileManager.DeleteFile(path);
        }
    }
}
