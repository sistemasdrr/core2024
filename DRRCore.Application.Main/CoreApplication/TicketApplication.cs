using AutoMapper;
using CoreFtp;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.DTO.Enum;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using DRRCore.Transversal.Common.JsonReader;

namespace DRRCore.Application.Main.CoreApplication
{
    public class TicketApplication : ITicketApplication
    {
        private readonly INumerationDomain _numerationDomain;
        private readonly ITicketDomain _ticketDomain;
        private readonly ITicketHistoryDomain _ticketHistoryDomain;       
        private IMapper _mapper;
        private ILogger _logger;
        public TicketApplication(INumerationDomain numerationDomain,ITicketDomain ticketDomain,ITicketHistoryDomain ticketHistoryDomain,IMapper mapper, ILogger logger)
        {
            _numerationDomain = numerationDomain;
            _ticketDomain = ticketDomain;
            _ticketHistoryDomain = ticketHistoryDomain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> AddTicketAsync(AddOrUpdateTicketRequestDto request)
        {
            var response = new Response<bool>();
            try
            {
                if (request == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (request.Id == 0)
                {
                    var newTicket = _mapper.Map<Ticket>(request);
                    newTicket.Status = (int?)TicketStatusEnum.Pendiente;
                    newTicket.TicketHistories.Add(new TicketHistory
                    {
                        Status= (int?)TicketStatusEnum.Pendiente,
                        UserFrom="1"
                    });
                    if( await _ticketDomain.AddAsync(newTicket))
                    {
                        await CopyReportForm(request.Number);
                        await CopyReportPerson(request.Number);                      
                        await _numerationDomain.UpdateTicketNumberAsync();
                        response.Data = true;
                    }
                }
                else
                {
                    var existingTicket = await _ticketDomain.GetByIdAsync(request.Id);
                    if (existingTicket == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataFound;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingTicket = _mapper.Map(request, existingTicket);

                    existingTicket.UpdateDate = DateTime.Now;
                    await _ticketDomain.UpdateAsync(existingTicket);
                    response.Data = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;

        }

        public async Task<Response<GetNumerationResponseDto>> GetTicketNumberAsync()
        {            
            var response = new Response<GetNumerationResponseDto>();
            try
            {
                var number = await _numerationDomain.GetTicketNumberAsync();
                if (number == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                int currentNumber = number.Number + 1 ?? 0;
                response.Data = new GetNumerationResponseDto
                {
                    IntValue = currentNumber,
                    StrValue = currentNumber.ToString("D6")
                };
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }
        private async Task CopyReportForm(int ticket)
        {
            try
            {
                using (var ftpClient = new FtpClient(GetFtpClientConfiguration()))
                {                    
                    await ftpClient.LoginAsync();
                  
                    using (var ftpReadStream = await ftpClient.OpenFileReadStreamAsync("/plantillas/Planilla_Reportero.doc"))
                    {

                        using (var stream = new MemoryStream())
                        {
                            ftpReadStream.CopyTo(stream);

                            using (var writeStream = await ftpClient.OpenFileWriteStreamAsync(string.Format("/cupones/{0}/{0}_Planilla_Reportero.doc",ticket.ToString("D6"))))
                            {
                                stream.Position = 0;
                                stream.CopyTo(writeStream);
                            }                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Messages.ExceptionMessage, ex.Message));
            }



        }
        private async Task CopyReportPerson(int ticket)
        {
            try
            {
                using (var ftpClient = new FtpClient(GetFtpClientConfiguration()))
                {
                    await ftpClient.LoginAsync();

                    using (var ftpReadStream = await ftpClient.OpenFileReadStreamAsync("/plantillas/Planilla_Negocios_Personales.doc"))
                    {

                        using (var stream = new MemoryStream())
                        {
                            ftpReadStream.CopyTo(stream);

                            using (var writeStream = await ftpClient.OpenFileWriteStreamAsync(string.Format("/cupones/{0}/{0}_Planilla_Negocios_Personales.doc", ticket.ToString("D6"))))
                            {
                                stream.Position = 0;
                                stream.CopyTo(writeStream);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Messages.ExceptionMessage, ex.Message));
            }
        }
        private FtpClientConfiguration GetFtpClientConfiguration()
        {
            return new FtpClientConfiguration
            {
                Host = "win5248.site4now.net",
                Port = 21,
                Username = "drrcore2024",
                Password = "drrti2023"
            };
       }
    }
}
