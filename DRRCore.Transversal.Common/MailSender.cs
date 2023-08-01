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
        public List<MemoryStream>? attachment { get; set; }


     }
public class MailSender : IMailSender
    {
        private readonly EmailSettings _emailSettings;       
        public MailSender(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
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
                        response = true;

                    }catch
                    {
                        for (var i = 0; i < _emailSettings?.OtherDomainsConfiguration.Count; i++)
                        {
                            client=smtpClient(i);
                            try
                            {
                                await client.SendMailAsync(mailMessage(values));
                                response = true;
                                break;
                            }
                            catch 
                            {
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
                        response = true;
                    }
                    catch
                    {
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
                    EnableSsl = _emailSettings.PrincipalDomain.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = _emailSettings.PrincipalDomain.UseDefaultCredentials,
                    Credentials = new NetworkCredential(_emailSettings.PrincipalDomain.Credential.Username, _emailSettings.PrincipalDomain.Credential.Password)
                };
          
        }
        private SmtpClient smtpClient(int index)
        {
            return new SmtpClient()
            {
                EnableSsl = _emailSettings.OtherDomainsConfiguration[index].EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = _emailSettings.OtherDomainsConfiguration[index].UseDefaultCredentials,
                Credentials = new NetworkCredential(_emailSettings.OtherDomainsConfiguration[index].Credential.Username,
                                                    _emailSettings.OtherDomainsConfiguration[index].Credential.Password)
            };

        }
        private MailMessage mailMessage(EmailValues values)
        {
            MailAddress from = new(values.FromEmail);
            MailAddress to = new(values.ToEmail);
            MailAddress bcc = new(values.CcEmail);
            MailAddress cc = new(values.BccEmail);
            MailMessage message = new(from, to);           
            message.Subject = values.Subject;
            message.Body = values.Body;          
            message.Bcc.Add(bcc);
            message.CC.Add(cc);
            message.IsBodyHtml = values.IsHtml;
            message.Attachments.Add(new Attachment(values.attachment[0], ""));
            return message;
        }
    }
}
