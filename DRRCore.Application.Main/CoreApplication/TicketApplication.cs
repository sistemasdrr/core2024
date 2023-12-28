using AutoMapper;
using CoreFtp;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.DTO.Enum;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Domain.Interfaces.MysqlDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Application.Main.CoreApplication
{
    public class TicketApplication : ITicketApplication
    {
        private readonly INumerationDomain _numerationDomain;
        private readonly ITicketDomain _ticketDomain;
        private readonly ITicketHistoryDomain _ticketHistoryDomain;   
        private readonly ICompanyDomain _companyDomain;
        private readonly ITCuponDomain _tCuponDomain;
        private IMapper _mapper;
        private ILogger _logger;
        public TicketApplication(INumerationDomain numerationDomain,
            ITCuponDomain tCuponDomain,ITicketDomain ticketDomain,ITicketHistoryDomain ticketHistoryDomain,ICompanyDomain companyDomain,IMapper mapper, ILogger logger)
        {
            _numerationDomain = numerationDomain;
            _ticketDomain = ticketDomain;
            _ticketHistoryDomain = ticketHistoryDomain;
            _mapper = mapper;
            _companyDomain = companyDomain;
            _tCuponDomain = tCuponDomain;
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
                        if(request.IdCompany==null)
                        {
                            await _companyDomain.AddAsync(new Company
                            {
                                Name = request.RequestedName ?? string.Empty
                            });
                        }                       
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

        public async Task<Response<GetExistingTicketResponseDto>> GetReportType(int id, string type)
        {
            var response = new Response<GetExistingTicketResponseDto>();
            var getExist = new GetExistingTicketResponseDto();
            var list = new List<GetListSameSearchedReportResponseDto>();
            try
            {
                if (type == "E")
                {
                    var company = await _companyDomain.GetByIdAsync(id);
                    if (company == null)
                    {
                        throw new Exception(Messages.MessageNoDataFound);
                    }
                    var newBd = await _ticketDomain.GetTicketByCompany(company.Id);

                    if(newBd!=null && newBd.Any())
                    {
                        list.AddRange(_mapper.Map<List<GetListSameSearchedReportResponseDto>>(newBd));
                    }

                    if (company.OldCode!=null && company.OldCode.StartsWith("E"))
                    {
                        var oldBd = await _tCuponDomain.GetTCuponExistAsync(company.OldCode);
                        if (oldBd != null && oldBd.Any())
                        {
                            list.AddRange(_mapper.Map<List<GetListSameSearchedReportResponseDto>>(oldBd));
                        }
                    }

                    if (list.Any())
                    {
                        getExist.TypeReport = "RV";
                        list = list.OrderByDescending(x => x.DispatchtDate).ToList();

                        var firstTicket = list.FirstOrDefault();

                        if((DateTime.Now - firstTicket.DispatchtDate).TotalDays <= 90)
                        {
                            getExist.TypeReport =firstTicket.IsPending?"DF":"EF";
                        }
                        getExist.LastSearchedDate = firstTicket.DispatchtDate.ToShortDateString();
                    }
                    getExist.ListSameSearched=list.Take(10).ToList();
                    response.Data = getExist;

                }
                else if (type == "P")
                {

                }
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetListTicketResponseDto>>> GetTicketListAsync()
        {
            var response = new Response<List<GetListTicketResponseDto>>();
            try
            {
                var list = await _ticketDomain.GetAllAsync();
                if(list != null)
                {
                    response.Data = _mapper.Map<List<GetListTicketResponseDto>>(list);
                }
                else
                {
                    response.Data = null;
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

        public async Task<Response<GetTicketRequestDto>> GetTicketRequestAsync(int id)
        {
            var response = new Response<GetTicketRequestDto>();
            try
            {
                var ticket = await _ticketDomain.GetByIdAsync(id);
                if (ticket != null)
                {
                    response.Data = _mapper.Map<GetTicketRequestDto>(ticket);
                }
                else
                {
                    response.Data = null;
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
                           await ftpReadStream.CopyToAsync(stream);
                          stream.Position = 0;

                            using (var writeStream = await ftpClient.OpenFileWriteStreamAsync("/cupones/"+ ticket.ToString("D6") +"/"+ ticket.ToString("D6")+"_Planilla_Reportero.doc"))
                            {
                                await stream.CopyToAsync(writeStream);                                
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
                           await ftpReadStream.CopyToAsync(stream);
                            stream.Position = 0;

                            using (var writeStream = await ftpClient.OpenFileWriteStreamAsync("/cupones/" + ticket.ToString("D6") + "/" + ticket.ToString("D6") + "Planilla_Negocios_Personales.doc"))
                            {
                                await stream.CopyToAsync(writeStream);
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
