using DRRCore.Application.Interfaces;
using DRRCore.Domain.Interfaces;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.JsonReader;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace DRRCore.Application.Main
{
    public class EmailUserApplication : IEmailUserApplication
    {
        private readonly IEmailUserDomain _emailUserDomain;
        private readonly EmailSettings _emailSettings;

        public EmailUserApplication(IOptions<EmailSettings> options, IEmailUserDomain emailUserDomain)
        {
            _emailUserDomain = emailUserDomain;
            _emailSettings = options.Value;
        }
        public Response<bool> Add()
        {
            var xxx = _emailSettings.OtherDomainsConfiguration;
            var response = new Response<bool>();

            var options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.RepeatableRead,
                Timeout = TimeSpan.FromTicks(Constants.TimeoutTransaccion),
            };
            using (var transaccion = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {    
                    if (_emailUserDomain != null)
                    _emailUserDomain?.Add();
                    response.Data = true;
                    transaccion.Complete();
                }
                catch (Exception exception)
                {
                    response.IsSuccess = false;
                    response.Message = exception.Message;                   
                }
            }

            return response;
        }
    }
}
