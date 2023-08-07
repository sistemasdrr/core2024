using DRRCore.Transversal.Common.Interface;
using DRRCore.Transversal.Common.JsonReader;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Transversal.Common
{
    public class EmailValues{
        public string FromEmail { get; set; }= string.Empty;
        public string ToEmail { get; set; } = string.Empty;
        public string CcEmail { get; set; } = string.Empty;
        public string BccEmail { get; set; } = string.Empty;
        public string? Subject { get; set; } = string.Empty;
        public string? Body { get; set; } = string.Empty;
        public bool IsHtml { get; set; } = true;
        public List<Attachment> Attachments { get; set; }= new List<Attachment>();

     }
    public class Attachment
    {
        public string FileName { get; set; }=string.Empty;
        public MemoryStream Stream { get; set; }=new MemoryStream();
    }
public class MailSender : IMailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger _logger;
        public MailSender(IOptions<EmailSettings> options,ILogger logger)
        {
            _emailSettings = options.Value;
            _logger = logger;
        }
       
        public async Task<bool> SendMailAsync(EmailValues values)
        {
            bool response = false;
            if (!_emailSettings.IsDebugging) {

                if (_emailSettings.IsMultipleDomain)
                {
                    var client = smtpClient();
                    try
                    {
                        await client.SendMailAsync(mailMessage(values));
                        _logger.LogInformation(Messages.MailSuccessSend);
                        response = true;

                    }catch
                    {
                        for (var i = 0; i < _emailSettings?.OtherDomainsConfiguration.Count; i++)
                        {
                            client=smtpClient(i);
                            try
                            {
                                await client.SendMailAsync(mailMessage(values));
                                _logger.LogInformation(Messages.MailSuccessSend);
                                response = true;
                                break;
                            }
                            catch (Exception ex)
                            {
                                _logger.LogInformation(string.Format(Messages.ErrorMailSend,ex.Message));
                                continue;
                            }                            
                        }
                    }
                }
                else
                {
                    var client = smtpClient();
                    try
                    {
                        await client.SendMailAsync(mailMessage(values));
                        _logger.LogInformation(Messages.MailSuccessSend);
                        response = true;
                    }
                    catch (Exception ex)
                    {
                        //await client.SendMailAsync(mailMessage(values));
                        _logger.LogInformation(string.Format(Messages.ErrorMailSend, ex.Message));
                        return false;
                    }
                   
                }
               
            }
            return response;
        }
        private SmtpClient smtpClient()
        {
              return new SmtpClient()
                {
                    Host= _emailSettings.PrincipalDomain.Host,
                    //EnableSsl = _emailSettings.PrincipalDomain.EnableSsl,
                    Port= _emailSettings.PrincipalDomain.Port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                   // UseDefaultCredentials = _emailSettings.PrincipalDomain.UseDefaultCredentials,
                    Credentials = new NetworkCredential(_emailSettings.PrincipalDomain.Credential.Username, _emailSettings.PrincipalDomain.Credential.Password)
                };
          
        }
        private SmtpClient smtpClient(int index)
        {
            return new SmtpClient()
            {
                Host = _emailSettings.OtherDomainsConfiguration[index].Host,
                //EnableSsl = _emailSettings.OtherDomainsConfiguration[index].EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port= _emailSettings.OtherDomainsConfiguration[index].Port,
                UseDefaultCredentials = _emailSettings.OtherDomainsConfiguration[index].UseDefaultCredentials,
                Credentials = new NetworkCredential(_emailSettings.OtherDomainsConfiguration[index].Credential.Username,
                                                    _emailSettings.OtherDomainsConfiguration[index].Credential.Password)
            };

        }
        private MailMessage mailMessage(EmailValues values)
        {
            MailAddress from = new(values.FromEmail);
            MailAddress to = new(values.ToEmail);
           
            MailMessage message = new(from, to);           
            message.Subject = values.Subject;
            message.Body = values.Body;
            if (!string.IsNullOrEmpty(values.CcEmail))
            {
                message.Bcc.Add(values.CcEmail);
            }
            if (!string.IsNullOrEmpty(values.BccEmail))
            {
                message.Bcc.Add(values.BccEmail);
            }
           
            message.IsBodyHtml = values.IsHtml;
            if (values.Attachments!=null || values.Attachments?.Count>0)
            {
                foreach (var attachment in values.Attachments)
                {
                    message.Attachments.Add(new System.Net.Mail.Attachment(attachment.Stream, attachment.FileName));
                }
               
            }
           
            return message;
        }

        public Task<bool> ReSendMailAsync(EmailValues values)
        {
            throw new NotImplementedException();
        }
    }
}
