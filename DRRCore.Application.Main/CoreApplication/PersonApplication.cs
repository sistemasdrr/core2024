using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Application.Main.CoreApplication
{
    public class PersonApplication : IPersonApplication
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPersonDomain _personDomain;
        private readonly IPersonHomeDomain _personHomeDomain;
        private readonly IPersonJobDomain _personJobDomain;
        private readonly IPersonActivitiesDomain _personActivitiesDomain;
        private readonly IPersonPropertyDomain _personPropertyDomain;
        private readonly IPersonSBSDomain _personSBSDomain;
        private readonly IPersonHistoryDomain _personHistoryDomain;
        private readonly IPersonGeneralInfoDomain _personGeneralInfoDomain;
       
        public PersonApplication(IMapper mapper, ILogger logger, IPersonDomain personDomain, IPersonHomeDomain personHomeDomain,
            IPersonJobDomain personJobDomain, IPersonSBSDomain personSBSDomain,
            IPersonActivitiesDomain personActivitiesDomain, IPersonPropertyDomain personPropertyDomain, 
            IPersonHistoryDomain personHistoryDomain, IPersonGeneralInfoDomain personGeneralInfoDomain)
        {
            _mapper = mapper;
            _logger = logger;
            _personDomain = personDomain;
            _personHomeDomain = personHomeDomain;
            _personActivitiesDomain = personActivitiesDomain;
            _personPropertyDomain = personPropertyDomain;
            _personHistoryDomain = personHistoryDomain;
            _personGeneralInfoDomain = personGeneralInfoDomain;
            _personJobDomain = personJobDomain;
            _personSBSDomain = personSBSDomain;
        }

        public async Task<Response<bool>> ActivateWebPerson(int id)
        {
            var response = new Response<bool>();
            try
            {
                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                }
                response.Data = await _personDomain.ActivateWebAsync(id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<int>> AddOrUpdatePerson(AddOrUpdatePersonRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newPerson = _mapper.Map<Person>(obj);
                    newPerson.Traductions = traductions;
                    response.Data = await _personDomain.AddPersonAsync(newPerson);
                }
                else
                {
                    var existingPerson = await _personDomain.GetByIdAsync(obj.Id);
                    if (existingPerson == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataFoundEmployee;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingPerson = _mapper.Map(obj, existingPerson);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingPerson.Traductions = traductions;
                    existingPerson.UpdateDate = DateTime.Now;
                    await _personDomain.UpdateAsync(existingPerson);
                    response.Data = existingPerson.Id;
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

        public async Task<Response<int?>> AddOrUpdatePersonActivitiesAsync(AddOrUpdatePersonActivitiesRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int?>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newPersonActivities = _mapper.Map<PersonActivity>(obj);
                    response.Data = await _personActivitiesDomain.AddPersonActivitiesAsync(newPersonActivities, traductions);
                }
                else
                {
                    var existingPersonActivities = await _personActivitiesDomain.GetByIdPersonAsync((int)obj.IdPerson);
                    if (existingPersonActivities == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingPersonActivities = _mapper.Map(obj, existingPersonActivities);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingPersonActivities.UpdateDate = DateTime.Now;
                    response.Data = await _personActivitiesDomain.UpdatePersonActivitiesAsync(existingPersonActivities, traductions);
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

        public async Task<Response<int?>> AddOrUpdatePersonGeneralInfoAsync(AddOrUpdatePersonGeneralInfoRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int?>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newObj = _mapper.Map<PersonGeneralInformation>(obj);
                    response.Data = await _personGeneralInfoDomain.AddPersonGeneralInfoAsync(newObj, traductions);
                }
                else
                {
                    var existingObj = await _personGeneralInfoDomain.GetByIdPersonAsync((int)obj.IdPerson);
                    if (existingObj == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingObj = _mapper.Map(obj, existingObj);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingObj.UpdateDate = DateTime.Now;
                    response.Data = await _personGeneralInfoDomain.UpdatePersonGeneralInfoAsync(existingObj, traductions);
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

        public async Task<Response<int?>> AddOrUpdatePersonHistoryAsync(AddOrUpdatePersonHistoryRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int?>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newObj = _mapper.Map<PersonHistory>(obj);
                    response.Data = await _personHistoryDomain.AddPersonHistoryAsync(newObj, traductions);
                }
                else
                {
                    var existingObj= await _personHistoryDomain.GetByIdPersonAsync((int)obj.IdPerson);
                    if (existingObj == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingObj = _mapper.Map(obj, existingObj);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingObj.UpdateDate = DateTime.Now;
                    response.Data = await _personHistoryDomain.UpdatePersonHistoryAsync(existingObj, traductions);
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

        public async Task<Response<int?>> AddOrUpdatePersonHomeAsync(AddOrUpdatePersonHomeRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int?>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newPersonHome = _mapper.Map<PersonHome>(obj);
                    response.Data = await _personHomeDomain.AddPersonHomeAsync(newPersonHome, traductions);
                }
                else
                {
                    var existingPersonHome = await _personHomeDomain.GetByIdPersonAsync((int)obj.IdPerson);
                    if (existingPersonHome == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingPersonHome = _mapper.Map(obj, existingPersonHome);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingPersonHome.UpdateDate = DateTime.Now;
                    response.Data = await _personHomeDomain.UpdatePersonHomeAsync(existingPersonHome, traductions);
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

        public async Task<Response<int?>> AddOrUpdatePersonJobAsync(AddOrUpdatePersonJobRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int?>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newObj= _mapper.Map<PersonJob>(obj);
                    response.Data = await _personJobDomain.AddPersonJobAsync(newObj, traductions);
                }
                else
                {
                    var existingObj= await _personJobDomain.GetByIdPersonAsync((int)obj.IdPerson);
                    if (existingObj == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingObj = _mapper.Map(obj, existingObj);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingObj.UpdateDate = DateTime.Now;
                    response.Data = await _personJobDomain.UpdatePersonJobAsync(existingObj, traductions);
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

        public async Task<Response<int?>> AddOrUpdatePersonPropertyAsync(AddOrUpdatePersonPropertyRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int?>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newPersonProperty= _mapper.Map<PersonProperty>(obj);
                    response.Data = await _personPropertyDomain.AddPersonPropertiesAsync(newPersonProperty, traductions);
                }
                else
                {
                    var existingPersonProperty = await _personPropertyDomain.GetByIdPersonAsync((int)obj.IdPerson);
                    if (existingPersonProperty == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingPersonProperty = _mapper.Map(obj, existingPersonProperty);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingPersonProperty.UpdateDate = DateTime.Now;
                    response.Data = await _personPropertyDomain.UpdatePersonPropertiesAsync(existingPersonProperty, traductions);
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

        public async Task<Response<int?>> AddOrUpdatePersonSBSAsync(AddOrUpdatePersonSbsRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int?>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newObj= _mapper.Map<PersonSb>(obj);
                    response.Data = await _personSBSDomain.AddPersonSBS(newObj, traductions);
                }
                else
                {
                    var existingObj= await _personSBSDomain.GetByIdPerson((int)obj.IdPerson);
                    if (existingObj == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingObj = _mapper.Map(obj, existingObj);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingObj.UpdateDate = DateTime.Now;
                    response.Data = await _personSBSDomain.UpdatePersonSBS(existingObj, traductions);
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

        public async Task<Response<bool>> DeletePerson(int id)
        {
            var response = new Response<bool>();
            try
            {
                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                }
                response.Data = await _personDomain.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DesactivateWebPerson(int id)
        {
            var response = new Response<bool>();
            try
            {
                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                }
                response.Data = await _personDomain.DesactivateWebAsync(id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetListPersonResponseDto>>> GetListPerson(string fullname, string form, int idCountry, bool haveReport)
        {
            var response = new Response<List<GetListPersonResponseDto>>();
            try
            {
                var list = await _personDomain.GetAllByAsync(fullname, form, idCountry, haveReport);
                if (list == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                }
                response.Data = _mapper.Map<List<GetListPersonResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetPersonActivitiesResponseDto>> GetPersonActivitiesByIdPerson(int idPerson)
        {
            var response = new Response<GetPersonActivitiesResponseDto>();
            try
            {
                var personActivity = await _personActivitiesDomain.GetByIdPersonAsync(idPerson);
                if (personActivity == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetPersonActivitiesResponseDto>(personActivity);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetPersonResponseDto>> GetPersonById(int id)
        {
            var response = new Response<GetPersonResponseDto>();
            try
            {
                var person = await _personDomain.GetByIdAsync(id);
                if (person == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetPersonResponseDto>(person);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetPersonGeneralInfoResponseDto>> GetPersonGeneralInfoByIdPerson(int idPerson)
        {
            var response = new Response<GetPersonGeneralInfoResponseDto>();
            try
            {
                var obj = await _personGeneralInfoDomain.GetByIdPersonAsync(idPerson);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetPersonGeneralInfoResponseDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetPersonHistoryResponseDto>> GetPersonHistoryByIdPerson(int idPerson)
        {
            var response = new Response<GetPersonHistoryResponseDto>();
            try
            {
                var obj = await _personHistoryDomain.GetByIdPersonAsync(idPerson);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetPersonHistoryResponseDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetPersonHomeResponseDto>> GetPersonHomeByIdPerson(int idPerson)
        {
            var response = new Response<GetPersonHomeResponseDto>();
            try
            {
                var personHome = await _personHomeDomain.GetByIdPersonAsync(idPerson);
                if (personHome == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetPersonHomeResponseDto>(personHome);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetPersonJobResponseDto>> GetPersonJobByIdPerson(int idPerson)
        {
            var response = new Response<GetPersonJobResponseDto>();
            try
            {
                var obj = await _personJobDomain.GetByIdPersonAsync(idPerson);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetPersonJobResponseDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetPersonPropertyResponseDto>> GetPersonPropertyByIdPerson(int idPerson)
        {
            var response = new Response<GetPersonPropertyResponseDto>();
            try
            {
                var personProperty = await _personPropertyDomain.GetByIdPersonAsync(idPerson);
                if (personProperty == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetPersonPropertyResponseDto>(personProperty);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetPersonSbsResponseDto>> GetPersonSBSById(int idPerson)
        {
            var response = new Response<GetPersonSbsResponseDto>();
            try
            {
                var obj = await _personSBSDomain.GetByIdPerson(idPerson);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetPersonSbsResponseDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }
    }
}
