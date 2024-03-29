﻿using AspNetCore.Reporting;
using AutoMapper;
using CoreFtp;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.DTO.Enum;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Application.Interfaces.EmailApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Domain.Interfaces.MysqlDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Crypto.Engines;

namespace DRRCore.Application.Main.CoreApplication
{
    public class TicketApplication : ITicketApplication
    {
        private readonly INumerationDomain _numerationDomain;
        private readonly ICountryDomain _countryDomain;
        private readonly ITicketDomain _ticketDomain;
        private readonly ITicketHistoryDomain _ticketHistoryDomain;   
        private readonly ICompanyDomain _companyDomain;
        private readonly ITCuponDomain _tCuponDomain;
        private readonly ITicketAssignationDomain _ticketAssignationDomain;
        private readonly ITicketReceptorDomain _ticketReceptorDomain;
        private readonly IUserLoginDomain _userLoginDomain;
        private readonly IEmailApplication _emailApplication;
        private readonly IReportingDownload _reportingDownload;
        private readonly IPersonDomain _personDomain;
        private readonly ISubscriberDomain _subscriberDomain;
        private readonly IPersonalDomain _personalDomain;
        private readonly IAgentDomain _agentDomain;
        private readonly ICompanyApplication _companyApplication;
        private IMapper _mapper;
        private ILogger _logger;
       
        public TicketApplication(INumerationDomain numerationDomain, ITicketAssignationDomain ticketAssignationDomain,
            ITCuponDomain tCuponDomain,ITicketDomain ticketDomain, IPersonalDomain personalDomain, IAgentDomain agentDomain,
            ITicketReceptorDomain ticketReceptorDomain,ITicketHistoryDomain ticketHistoryDomain, ICountryDomain countryDomain,
            ICompanyDomain companyDomain,IMapper mapper, ILogger logger,IReportingDownload reportingDownload,
            IEmailApplication emailApplication,IUserLoginDomain userLoginDomain,IPersonDomain personDomain,ISubscriberDomain subscriberDomain,ICompanyApplication companyApplication)
        {
            _numerationDomain = numerationDomain;
            _companyApplication= companyApplication;
            _ticketDomain = ticketDomain;
            _ticketHistoryDomain = ticketHistoryDomain;
            _mapper = mapper;
            _companyDomain = companyDomain;
            _tCuponDomain = tCuponDomain;
            _ticketReceptorDomain= ticketReceptorDomain;
            _logger = logger;           
            _userLoginDomain = userLoginDomain;
            _emailApplication = emailApplication;
            _reportingDownload = reportingDownload;
            _subscriberDomain = subscriberDomain;
            _personDomain= personDomain;
            _ticketAssignationDomain = ticketAssignationDomain;
            _personalDomain = personalDomain;
            _agentDomain = agentDomain;
            _countryDomain = countryDomain;
        }
        public async Task<Response<List<GetTicketFileResponseDto>>> GetTicketFilesByIdTicket(int idTicket)
        {
            var response = new Response<List<GetTicketFileResponseDto>>();
            try
            {
                var ticketFiles = await _ticketDomain.GetFilesByIdTicket(idTicket);
                response.Data = _mapper.Map<List<GetTicketFileResponseDto>>(ticketFiles);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response<GetFileDto>> DownloadFileByPath(string path)
        {
            var response = new Response<GetFileDto>();
            MemoryStream ms = new MemoryStream();
            try
            {
                var file = await _ticketDomain.GetFileByPath(path);
                if(file != null)
                {
                    ms.Position = 0;
                    ms = await DescargarArchivo(path);
                    var documentDto = new GetFileDto();
                    documentDto.File = ms;
                    documentDto.ContentType = GetContentType(file.Extension);
                    documentDto.FileName = file.Name;
                    response.Data = documentDto;
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ex.Message;
            }
            return response;
        }
        
        private string GetContentType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                case ".docx":
                    return "application/msword";
                case ".xls":
                case ".xlsx":
                    return "application/vnd.ms-excel";
                case ".txt":
                    return "text/plain";
                case ".zip":
                    return "application/zip";
                case ".rar":
                    return "application/x-rar-compressed";
                case ".xml":
                    return "application/xml";
                case ".json":
                    return "application/json";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                default:
                    return "application/octet-stream"; 
            }
        }
        private async Task<MemoryStream> DescargarArchivo(string? path)
        {
            using (var ftpClient = new FtpClient(GetFtpClientConfiguration()))
            {
                await ftpClient.LoginAsync();

                using (var ftpReadStream = await ftpClient.OpenFileReadStreamAsync(path))
                {
                    using (var stream = new MemoryStream())
                    {
                        await ftpReadStream.CopyToAsync(stream);
                        return stream;
                    }
                }
            }
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
                    newTicket.IdStatusTicket = (int?)TicketStatusEnum.Pendiente;
                    newTicket.TicketHistories.Add(new TicketHistory
                    {
                        IdStatusTicket= (int?)TicketStatusEnum.Pendiente,
                        UserFrom="1"
                    });
                    newTicket.TicketAssignation = new TicketAssignation
                    {
                        IdEmployee = await GetReceptorDefault(request.IdCountry??0, request.ReportType,request.IdSubscriber)
                    };
                  
                    if ( await _ticketDomain.AddAsync(newTicket))
                    {
                      //  await CopyReportForm(request.Number);
                       // await CopyReportPerson(request.Number);
                        await _numerationDomain.UpdateTicketNumberAsync();
                        if(request.About == "E" && newTicket.IdCompany==null  )
                        {
                          var company=  await _companyDomain.AddCompanyAsync(new Company
                            {
                                Name = request.RequestedName ?? string.Empty,
                                Language=request.Language

                            });
                            var ticket=await _ticketDomain.GetByIdAsync(newTicket.Id);
                            ticket.IdCompany = company;
                            await _ticketDomain.UpdateAsync(ticket);
                           

                        }
                        if (request.About == "P" && newTicket.IdPerson == null )
                        {
                           var person= await _personDomain.AddPersonAsync(new Person
                            {
                                Fullname = request.RequestedName ?? string.Empty,
                                Language= request.Language
                            });
                            var ticket = await _ticketDomain.GetByIdAsync(newTicket.Id);
                            ticket.IdPerson = person;
                            await _ticketDomain.UpdateAsync(ticket);
                        }
                        if (request.About == "E" && request.ReportType != "OR")
                        {
                            var ticket = await _ticketDomain.GetByIdAsync(newTicket.Id);
                            var doc= await _companyApplication.DownloadF8(ticket.IdCompany??0, "ESP", "pdf");
                            if(doc!=null && doc.Data != null)
                            {
                                var data = doc.Data;
                                var path = await UploadF1(ticket.Id,"RV_"+ticket.RequestedName,data.File);
                                using(var context= new SqlCoreContext())
                                {
                                    await context.TicketFiles.AddAsync(new TicketFile
                                    {
                                        IdTicket = ticket.Id,
                                        Path = path,
                                        Name= "RV_"+DateTime.Now.ToString("ddMMyy")+"_"+request.Number.ToString("D6"),
                                        Extension=".pdf"  
                                    }) ;
                                    await context.SaveChangesAsync();
                                }
                               
                            }
                            
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

        private async Task<int?> GetReceptorDefault(int idCountry, string reportType,int? idSubscriber)
        {
            var getReceptor=new TicketReceptor();
            var subscriber = await _subscriberDomain.GetSubscriberById(idSubscriber??0);
            if((subscriber != null && subscriber.Code=="0107" )|| reportType=="BC")
            {
                return 42;
            }
            if (reportType == "DF")
            {
                getReceptor = await _ticketReceptorDomain.GetReceptorDoubleDate(idCountry);
                return getReceptor.IdEmployee ?? null;
            }
            if(reportType == "EF")
            {
                getReceptor = await _ticketReceptorDomain.GetReceptorInDate(idCountry);
                return getReceptor.IdEmployee ?? null;
            }
            getReceptor = await _ticketReceptorDomain.GetReceptorOtherCase(idCountry);
            return getReceptor.IdEmployee ?? null;
        }

        public async Task<Response<bool>> DeleteTicket(int id)
        {
            var response = new Response<bool>();
            try
            {
                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = await _ticketDomain.DeleteAsync(id);
            }
            catch(Exception ex) 
            { 
                response.IsSuccess = false;
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
                        var oldBd = await _ticketDomain.GetOldTicketByCompany(company.OldCode);
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
                    getExist.ListSameSearched=list.ToList();
                    response.Data = getExist;

                }
                else if (type == "P")
                {
                    var person = await _personDomain.GetByIdAsync(id);
                    if (person == null)
                    {
                        throw new Exception(Messages.MessageNoDataFound);
                    }
                    var newBd = await _ticketDomain.GetTicketByPerson(person.Id);

                    if (newBd != null && newBd.Any())
                    {
                        list.AddRange(_mapper.Map<List<GetListSameSearchedReportResponseDto>>(newBd));
                    }

                    if (person.OldCode != null && person.OldCode.StartsWith("P"))
                    {
                        var oldBd = await _ticketDomain.GetOldTicketByPerson(person.OldCode);
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

                        if ((DateTime.Now - firstTicket.DispatchtDate).TotalDays <= 90)
                        {
                            getExist.TypeReport = firstTicket.IsPending ? "DF" : "EF";
                        }
                        getExist.LastSearchedDate = firstTicket.DispatchtDate.ToShortDateString();
                    }
                    getExist.ListSameSearched = list.ToList();
                    response.Data = getExist;
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

        public async Task<Response<List<GetListTicketResponseDto>>> GetTicketListPendingAsync()
        {
            var response = new Response<List<GetListTicketResponseDto>>();
            try
            {
                var tickets = await _ticketDomain.GetAllPendingTickets();
                response.Data= _mapper.Map<List<GetListTicketResponseDto>>(tickets);

                return response;
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
        private async Task<string> UploadF1(int ticket,string fileName, byte[] byteArray)
        {
            try
            {
                var path = "/cupones/" + ticket.ToString("D6") + "/" + fileName+".pdf";
                using (var ftpClient = new FtpClient(GetFtpClientConfiguration()))
                {
                    await ftpClient.LoginAsync();

                    MemoryStream memoryStream = new MemoryStream(byteArray);
                    memoryStream.Position = 0;
                         
                            using (var writeStream = await ftpClient.OpenFileWriteStreamAsync(path))
                            {
                                await memoryStream.CopyToAsync(writeStream);
                            }
                        
                    
                }
                return path;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Messages.ExceptionMessage, ex.Message));
            }
        }
        private async Task<string> CopyReportForm(int ticket)
        {
            try
            {
                var path = "/cupones/" + ticket.ToString("D6") + "/" + ticket.ToString("D6") + "_Planilla_Reportero.doc";
                using (var ftpClient = new FtpClient(GetFtpClientConfiguration()))
                {                    
                    await ftpClient.LoginAsync();
                 
                    using (var ftpReadStream = await ftpClient.OpenFileReadStreamAsync("/plantillas/Planilla_Reportero.doc"))
                    {

                        using (var stream = new MemoryStream())
                        {
                           await ftpReadStream.CopyToAsync(stream);
                          stream.Position = 0;

                            using (var writeStream = await ftpClient.OpenFileWriteStreamAsync(path)) 
                            {
                                await stream.CopyToAsync(writeStream);                                   
                            }                           
                        }
                    }
                }
                return path;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Messages.ExceptionMessage, ex.Message));
            }
        }
        private async Task<string> CopyReportPerson(int ticket)
        {
            try
            {
                var path = "/cupones/" + ticket.ToString("D6") + "/" + ticket.ToString("D6") + "_Planilla_Negocios_Personales.doc";
                using (var ftpClient = new FtpClient(GetFtpClientConfiguration()))
                {
                    await ftpClient.LoginAsync();

                    using (var ftpReadStream = await ftpClient.OpenFileReadStreamAsync("/plantillas/Planilla_Negocios_Personales.doc"))
                    {

                        using (var stream = new MemoryStream())
                        {
                           await ftpReadStream.CopyToAsync(stream);
                            stream.Position = 0;

                            using (var writeStream = await ftpClient.OpenFileWriteStreamAsync(path))
                            {
                                await stream.CopyToAsync(writeStream);
                            }
                        }
                    }
                }
                return path;
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

        public async Task<Response<List<GetListTicketResponseDto>>> GetTicketListByAsync(string ticket, string name, string subscriber, string type, string procedure)
        {
            var response = new Response<List<GetListTicketResponseDto>>();
            try
            {
                var list = await _ticketDomain.GetAllByAsync(ticket,name, subscriber,type,procedure);
                if (list != null)
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

        public async Task<Response<GetTicketQueryResponseDto>> GetTicketQuery(int idTicket)
        {
            var response = new Response<GetTicketQueryResponseDto>();
            try
            {
                var query = await _ticketDomain.GetTicketQuery(idTicket);

               
                if (query.Status == 0)
                {
                    var ticket = await _ticketDomain.GetByIdAsync(idTicket);
                    response.Data = new GetTicketQueryResponseDto
                    {
                        IdTicket=ticket.Id,
                        QueryDate = DateTime.Now,
                        IdSubscriber = ticket.IdSubscriber,
                        Report=ticket.RequestedName??string.Empty,
                        Email=ticket.IdSubscriberNavigation.Email,
                        SubscriberName = ticket.IdSubscriberNavigation == null ? string.Empty :ticket.IdSubscriberNavigation.Code+"||"+ ticket.IdSubscriberNavigation.Name ?? string.Empty
                       
                    };
                }
                else {
                    response.Data = _mapper.Map<GetTicketQueryResponseDto>(query);
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

        public async Task<Response<bool>> AnswerTicket(int idTicket)
        {
            var response = new Response<bool>();
            try
            {
                var ticket = await _ticketDomain.GetByIdAsync(idTicket);
                ticket.IdStatusTicket = (int?)TicketStatusEnum.Pendiente;

                await _ticketDomain.UpdateAsync(ticket);

                response.Data= await _ticketDomain.TicketQueryAnswered(idTicket);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> SendTicketQuery(SendTicketQueryRequestDto request)
        {
            var listEmailTo=new List<string>();
            var response = new Response<bool>();
            try
            {
                var query = _mapper.Map<TicketQuery>(request);
                query.IdEmployee = 1;
                response.Data = await _ticketDomain.AddTicketQuery(query);

                var user = await _userLoginDomain.GetByIdAsync(19);

                var ticket = await _ticketDomain.GetByIdAsync(request.IdTicket);

                listEmailTo.AddRange(request.Email.Split(';'));
                listEmailTo.Add(user.IdEmployeeNavigation.Email);

                string subject;
                if (request.Language == "I")
                {
                    subject = "Requirement query : " + ticket.RequestedName + "| Order Date : " + ticket.OrderDate;
                }
                else
                {
                    subject = "Consulta al requerimiento : " + ticket.RequestedName + "| Fecha : " + ticket.OrderDate.ToString("dd/MM/yyyy");
                }
                var responseEmail=await _emailApplication.SendMailAsync(new DTO.Email.EmailDataDTO
                {
                    BeAuthenticated = true,
                    UserName=user.IdEmployeeNavigation.Email,
                    Password=user.EmailPassword,
                    EmailKey = request.Language == "I" ? Constants.DRR_EECORE_ENG_QUERYTICKET : Constants.DRR_EECORE_ESP_QUERYTICKET,
                    From = user.IdEmployeeNavigation.Email,
                    To = listEmailTo,
                    DisplayName = user.IdEmployeeNavigation.FirstName + ' ' + user.IdEmployeeNavigation.LastName,
                    User = "19",
                    IsBodyHTML = true,
                    Subject = subject,
                    Parameters = new List<string>
                    {
                        ticket.IdSubscriberNavigation.Name,
                        ticket.RequestedName,
                        ticket.OrderDate.ToString("dd/MM/yyyy"),
                        request.Language=="I"? ticket.IdCountryNavigation.EnglishName:ticket.IdCountryNavigation.Name,
                        ticket.ReferenceNumber,
                        request.Message,
                        user.IdEmployeeNavigation.Email
                    }
                });
                if (responseEmail.Data)
                {
                    ticket.IdStatusTicket = (int?)TicketStatusEnum.En_Consulta;
                    await _ticketDomain.UpdateAsync(ticket);
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

        public async Task<Response<byte[]>> DownloadReport()
        {
            var response = new Response<byte[]>();
            try
            {
                response.Data= await _reportingDownload.GenerateReportAsync("reporteTicket", ReportRenderType.Excel, null);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> SavePreAsignTicket(List<SavePreAsignTicketDto> lista)
        {
            var response = new Response<bool>();
            try
            {
                foreach (var item in lista)
                {
                    var ticketAssignation = await _ticketAssignationDomain.GetByIdAsync(item.Id);
                    if (ticketAssignation != null)
                    {
                        ticketAssignation.Commentary = item.Commentary;
                        ticketAssignation.IdEmployee = item.IdReceptor;
                        await _ticketAssignationDomain.UpdateAsync(ticketAssignation);
                    }
                }
                response.Data = true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Data = false;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Response<bool>> SendPreAsignTicket(List<SavePreAsignTicketDto> lista)
        {
            var response = new Response<bool>();
            try
            {
                foreach (var item in lista)
                {
                    var ticket = await _ticketDomain.GetByIdAsync(item.Id);
                    var ticketAssignation = await _ticketAssignationDomain.GetByIdAsync(item.Id);
                    if (ticket != null)
                    {
                        ticket.IdStatusTicket = 12;
                        await _ticketDomain.UpdateAsync(ticket);
                        var listTicketHistory = await _ticketHistoryDomain.GetAllByIdTicket(item.Id);
                        foreach (var item1 in listTicketHistory)
                        {
                            item1.Flag = false;
                            await _ticketHistoryDomain.UpdateAsync(item1);
                        }
                        var newTicketHistory = new TicketHistory
                        {
                            Id = 0,
                            IdTicket = item.Id,
                            UserFrom = item.IdEmisor.ToString(),
                            UserTo = item.IdReceptor.ToString(),
                            IdStatusTicket = ticket.IdStatusTicket
                        };
                        await _ticketHistoryDomain.AddAsync(newTicketHistory);
                        await _numerationDomain.AddAsync(new Numeration
                        {
                            Id = 0,
                            Name = "PRE_ASSIGN",
                            Description = "Pre-Asinación del Cupón",
                            Number = 1,
                        });
                        if (ticketAssignation != null)
                        {
                            ticketAssignation.Commentary = item.Commentary;
                            ticketAssignation.IdEmployee = item.IdReceptor;
                            await _ticketAssignationDomain.UpdateAsync(ticketAssignation);
                            
                        }
                    }

                }
                response.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Data = false;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Response<List<GetListTicketResponseDto>>> GetTicketsToUser(string userTo)
        {
            var response = new Response<List<GetListTicketResponseDto>>();
            try
            {
                var list = await _ticketHistoryDomain.GetTicketsPreAssignedToUser(userTo);
                if (list != null)
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

        public async Task<Response<List<GetPersonalAssignationResponseDto>>> GetPersonalAssignation()
        {
            var response = new Response<List<GetPersonalAssignationResponseDto>>();
            try
            {
                var list = await _personalDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetPersonalAssignationResponseDto>>(list);
            }catch(Exception ex)
            {
                response.IsSuccess = false;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetPersonalAssignationResponseDto>>> GetAgentAssignation()
        {
            var response = new Response<List<GetPersonalAssignationResponseDto>>();
            try
            {
                var list = await _agentDomain.GetAllAgentsAsync("","","A");
                response.Data = _mapper.Map<List<GetPersonalAssignationResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> UploadFile(int idTicket, string numCupon, IFormFile file)
        {
            var response = new Response<bool>();
            try
            {
                using (var ftpClient = new FtpClient(GetFtpClientConfiguration()))
                {
                    await ftpClient.LoginAsync();
                    string filePath = "/cupones/" + numCupon + "/" + file.FileName;
                    using (var writeStream = await ftpClient.OpenFileWriteStreamAsync(filePath))
                    {
                        file.CopyTo(writeStream);
                    }
                    response.Data = true;
                    string fileExtension = Path.GetExtension(file.FileName);
                    if (response.Data == true)
                    {
                        await _ticketDomain.AddTicketFile(new TicketFile
                        {
                            Id = 0,
                            IdTicket = idTicket,
                            Name = file.FileName,
                            Path = filePath,
                            Extension = fileExtension
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsWarning = true;
                throw new Exception(ex.Message);
            }
            return response;
        }

        public async Task<Response<bool?>> DeleteFile(int id)
        {
            var response = new Response<bool?>();
            try
            {
                var ticketFile = await _ticketDomain.GetTicketFileById(id);
                response.Data = await _ticketDomain.DeleteTicketFile(id);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                throw new Exception(ex.Message);
            }
            return response;
        }
        private async Task<bool> EliminarArchivo(string path)
        {
            using (var ftpClient = new FtpClient(GetFtpClientConfiguration()))
            {
                await ftpClient.LoginAsync();

                try
                {
                    await ftpClient.DeleteFileAsync(path);
                    return true; 
                }
                catch (Exception ex)
                {
                    return false; 
                }
            }
        }

        public async Task<Response<bool>> AddTicketHistory(List<AddOrUpdateAssignationsRequestDto> obj)
        {
            var response = new Response<bool>();
            try
            {
                foreach (var item in obj)
                {
                    if(item.Type == "PA")
                    {

                    }
                }
            }
            catch(Exception ex)
            {

            }
            return response;
        }

        public async Task<Response<string?>> GetNumCuponById(int idTicket)
        {
            var response = new Response<string?>();
            try
            {
                var ticket = await _ticketDomain.GetByIdAsync(idTicket);
                if(ticket !=  null)
                {
                    response.Data = ticket.Number.ToString("D6");
                }
            }catch(Exception ex)
            {
                response.Data = "";
                _logger.LogError(ex.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetListTicketResponseDto>>> GetTicketsByIdSubscriber(int idSubscriber, string? company, DateTime from, DateTime until, int idCountry)
        {
            var response = new Response<List<GetListTicketResponseDto>>();
            try
            {
                var tickets = await _ticketDomain.GetTicketsByIdSubscriber(idSubscriber, company, from, until, idCountry);
                if(tickets.Count > 0)
                {
                    response.Data = _mapper.Map<List<GetListTicketResponseDto>>(tickets);
                }
                else
                {
                    response.Data = null;
                    response.IsSuccess = false;
                }
            }catch(Exception ex)
            {
                response.Data = null;
                response.IsSuccess = false;
                _logger?.LogError(ex.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> AddTicketByWeb(AddOrUpdateTicketRequestDto request)
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

                var newTicket = _mapper.Map<Ticket>(request);
                newTicket.Web = true;
                var country = await _countryDomain.GetByIdAsync((int)newTicket.IdCountry);
                if(country != null)
                {
                    newTicket.IdContinent = country.IdContinent;
                    newTicket.TaxType = country.TaxTypeName;
                }
                newTicket.ReportType = "OR";
                newTicket.About = "E";
                var number = await _numerationDomain.GetTicketNumberAsync();
                int currentNumber = number.Number + 1 ?? 0;
                newTicket.Number = currentNumber;
                newTicket.IdStatusTicket = (int?)TicketStatusEnum.Pendiente;
                newTicket.TicketHistories.Add(new TicketHistory
                {
                    IdStatusTicket = (int?)TicketStatusEnum.Pendiente,
                    UserFrom = "1"
                });
                newTicket.TicketAssignation = new TicketAssignation
                {
                    IdEmployee = await GetReceptorDefault(request.IdCountry ?? 0, request.ReportType, request.IdSubscriber)
                };

                if (await _ticketDomain.AddAsync(newTicket))
                {
                    //  await CopyReportForm(request.Number);
                    // await CopyReportPerson(request.Number);
                    await _numerationDomain.UpdateTicketNumberAsync();
                    if (request.About == "E" && newTicket.IdCompany == null)
                    {
                        var company = await _companyDomain.AddCompanyAsync(new Company
                        {
                            Name = request.RequestedName ?? string.Empty,
                            Language = request.Language,
                            TaxTypeCode = request.TaxCode,
                            IdCountry = request.IdCountry,
                            Address = request.Address,
                            Place = request.City
                        });
                        var ticket = await _ticketDomain.GetByIdAsync(newTicket.Id);
                        ticket.IdCompany = company;
                        await _ticketDomain.UpdateAsync(ticket);
                    }
                    if (request.About == "P" && newTicket.IdPerson == null)
                    {
                        var person = await _personDomain.AddPersonAsync(new Person
                        {
                            Fullname = request.RequestedName ?? string.Empty,
                            Language = request.Language
                        });
                        var ticket = await _ticketDomain.GetByIdAsync(newTicket.Id);
                        ticket.IdPerson = person;
                        await _ticketDomain.UpdateAsync(ticket);
                    }
                    if (request.About == "E" && request.ReportType != "OR")
                    {
                        var ticket = await _ticketDomain.GetByIdAsync(newTicket.Id);
                        var doc = await _companyApplication.DownloadF8(ticket.IdCompany ?? 0, "ESP", "pdf");
                        if (doc != null && doc.Data != null)
                        {
                            var data = doc.Data;
                            var path = await UploadF1(ticket.Id, "RV_" + ticket.RequestedName, data.File);
                            using (var context = new SqlCoreContext())
                            {
                                await context.TicketFiles.AddAsync(new TicketFile
                                {
                                    IdTicket = ticket.Id,
                                    Path = path,
                                    Name = "RV_" + DateTime.Now.ToString("ddMMyy") + "_" + request.Number.ToString("D6"),
                                    Extension = ".pdf"
                                });
                                await context.SaveChangesAsync();
                            }
                        }
                    }
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

        public async Task<Response<bool>> AddTicketOnline(AddOrUpdateTicketRequestDto request, string rubro, string sendTo)
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

                var newTicket = _mapper.Map<Ticket>(request);
                newTicket.Web = true;
                var country = await _countryDomain.GetByIdAsync((int)newTicket.IdCountry);
                if (country != null)
                {
                    newTicket.IdContinent = country.IdContinent;
                }
                var number = await _numerationDomain.GetTicketNumberAsync();
                int currentNumber = number.Number + 1 ?? 0;
                newTicket.Number = currentNumber;
                newTicket.IdStatusTicket = (int?)TicketStatusEnum.Despachado;
                newTicket.DispatchtDate = DateTime.Now;
                
                response.Data = await _ticketDomain.AddAsync(newTicket);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetTicketHistorySubscriberResponseDto>>> GetTicketHistoryByIdSubscriber(int idSubscriber, string? name, DateTime? from, DateTime? until, int? idCountry)
        {
            var response = new Response<List<GetTicketHistorySubscriberResponseDto>>();
            try
            {
                var tickets = await _ticketDomain.GetTicketHistoryByIdSubscriber(idSubscriber, name, from, until, idCountry);
                if(tickets != null)
                {
                    response.Data = _mapper.Map<List<GetTicketHistorySubscriberResponseDto>>(tickets);
                }
                else
                {
                    response.Data = null;
                    response.IsSuccess = false;
                    response.Message = Messages.BadQuery;
                }
            }catch(Exception ex)
            {
                response.Data = null;
                response.IsSuccess = false;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }
    }
}
