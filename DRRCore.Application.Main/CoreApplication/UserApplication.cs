using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DRRCore.Application.Main.CoreApplication
{
    public class UserApplication : IUserApplication
    {
        private readonly IProcessDomain _processDomain; 
        private readonly IConfiguration _configuration;
        private readonly IUserLoginDomain _userLoginDomain;
        private readonly IUserProcessDomain _userProcessDomain;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsIdentity claims;

        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public UserApplication(IProcessDomain processDomain, IUserLoginDomain userLoginDomain, ILogger logger, IMapper mapper
            , IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUserProcessDomain userProcessDomain)
        {
            _processDomain = processDomain;
            _userLoginDomain = userLoginDomain;
            _httpContextAccessor = httpContextAccessor;
            claims = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _userProcessDomain = userProcessDomain;
        }
        public async Task<Response<List<GetProcessResponseDto>>> GetProcessAsync()
        {
            var response = new Response<List<GetProcessResponseDto>>();
            try
            {
                var list = await _processDomain.GetAllAsync();
                if(list != null)
                {
                    response.Data = _mapper.Map<List<GetProcessResponseDto>>(list);
                }
                else
                {
                    response.IsSuccess = false;
                }
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetUserProcessResponseDto>> getProcessByIdEmployee(int idEmployee)
        {
            var response = new Response<GetUserProcessResponseDto>();
            var data = new GetUserProcessResponseDto();
            var listGerencia = new List<UserProcessDto>();
            var listProduccion = new List<UserProcessDto>();
            var listAdministracion = new List<UserProcessDto>();
            try
            {
                var idUser = await _userLoginDomain.GetIdUserByIdEmployee(idEmployee);
                if(idUser != 0 && idUser != null)
                {
                    var permissions = await _userProcessDomain.GetProcessByIdUser((int)idUser);
                    if (permissions != null && permissions.Count != 0)
                    {
                        foreach (var item in permissions)
                        {
                            if (item.IdProcessNavigation.Menu == "Gerencia")
                            {
                                listGerencia.Add(_mapper.Map<UserProcessDto>(item));
                            }
                            else if (item.IdProcessNavigation.Menu == "Administración")
                            {
                                listAdministracion.Add(_mapper.Map<UserProcessDto>(item));
                            }
                            else if (item.IdProcessNavigation.Menu == "Producción")
                            {
                                listProduccion.Add(_mapper.Map<UserProcessDto>(item));
                            }
                        }
                    }
                    else
                    {
                        var addProcess = await _userProcessDomain.AddAllProcess((int)idUser);
                        if(addProcess == true)
                        {
                            permissions = await _userProcessDomain.GetProcessByIdUser((int)idUser);
                            if (permissions != null && permissions.Count != 0)
                            {
                                foreach (var item in permissions)
                                {
                                    if (item.IdProcessNavigation.Menu == "Gerencia")
                                    {
                                        listGerencia.Add(_mapper.Map<UserProcessDto>(item));
                                    }
                                    else if (item.IdProcessNavigation.Menu == "Administración")
                                    {
                                        listAdministracion.Add(_mapper.Map<UserProcessDto>(item));
                                    }
                                    else if (item.IdProcessNavigation.Menu == "Producción")
                                    {
                                        listProduccion.Add(_mapper.Map<UserProcessDto>(item));
                                    }
                                }
                            }
                        }
                    }
                    data.Gerencia = listGerencia;
                    data.Administracion = listAdministracion;
                    data.Produccion = listProduccion;
                    response.Data = data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(response.Message, ex);
                return null;
            }
            return response;
        }

        public async Task<Response<GetUserResponseDto>> getUserById(int id)
        {
            var response = new Response<GetUserResponseDto>();
            try
            {
                var user = await _userLoginDomain.GetByIdAsync(id);
                if(user != null)
                {
                    response.Data = _mapper.Map<GetUserResponseDto>(user);
                }
                else
                {
                    response.Data = null;
                }
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                _logger.LogError(ex.Message, ex);
            }
            return response;
        }

        public async Task<Response<string>> LoginUser(string username, string password)
        {
            var response = new Response<string>();
            try
            {
                var user = await _userLoginDomain.UserLogin(username, password);

                if (user == null)
                {
                    response.Data = null;
                    response.IsSuccess = false;
                }
                else
                {
                    response.Data = await GenerateToken(user);
                }            
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> UpdateProcess(AddOrUpdateUserProcessRequestDto obj)
        {
            var response = new Response<bool>();
            try
            {
                foreach (var item in obj.Gerencia)
                {
                    var process = await _userProcessDomain.GetByIdAsync(item.Id);
                    if(process != null)
                    {
                        await _userProcessDomain.UpdateAsync(_mapper.Map(item, process));
                    }
                }
                foreach (var item in obj.Produccion)
                {
                    var process = await _userProcessDomain.GetByIdAsync(item.Id);
                    if (process != null)
                    {
                        await _userProcessDomain.UpdateAsync(_mapper.Map(item, process));
                    }
                }
                foreach (var item in obj.Administracion)
                {
                    var process = await _userProcessDomain.GetByIdAsync(item.Id);
                    if (process != null)
                    {
                        await _userProcessDomain.UpdateAsync(_mapper.Map(item, process));
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

        private async Task<string> GenerateToken(UserLogin user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim("IdUser",user.Id.ToString()),
                new Claim("IdEmployee",user.IdEmployee.ToString()),
            };
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
