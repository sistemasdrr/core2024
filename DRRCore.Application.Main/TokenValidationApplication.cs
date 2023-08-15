using DRRCore.Application.Interfaces;
using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Transversal.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI.Common;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DRRCore.Application.Main
{
    public class TokenValidationApplication : ITokenValidationApplication
    {
        public readonly IApiUserDomain _apiUserDomain;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenValidationApplication(IApiUserDomain apiUserDomain, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _apiUserDomain = apiUserDomain;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> ValidationTokenAsync()
        {
            var response = new Response<bool>();
            string tokenEncriptado = GetTokenByHeader();
            try
            {
                response.IsWarning = true;
                response.IsSuccess = false;

                if (tokenEncriptado == Messages.TokenNotSend)
                {
                    response.Message = Messages.TokenNotSend;
                    return response;
                }
                else
                {
                    var tokenDesencriptado = Decrypt(tokenEncriptado);//Desencripta el token
                    if (tokenDesencriptado == Messages.UnauthorizedUser)
                    {
                        response.Message = Messages.UnauthorizedUser;
                        return response;
                    }
                    else
                    {
                        var comprobarToken = await _apiUserDomain.GetApiUserByTokenAsync(tokenDesencriptado); //obtiene el apiuser mediante el token
                        if (comprobarToken != null && comprobarToken.Enable == true && comprobarToken.Active == true) //comprueba si existe
                        {
                            response.IsWarning = false;
                            response.IsSuccess = true;
                            response.Message = Messages.AuthorizedUser;
                        }
                        else
                        {
                            response.Message = Messages.UserNotActive;
                        }
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsWarning = true;
                response.IsSuccess = false;
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Response<string>> EncryptTokenAsync(string token)
        {
            var response = new Response<string>();
            try
            {
                var tokenEncriptado = Encrypt(token);//Encripta el token
                response.IsSuccess = true;
                response.Data = tokenEncriptado;
                response.Message = "Token Encriptado";
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public string Encrypt(string token)
        {
            if (token == null)
            {
                return null;
            }
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(token);
            var jwtKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            jwtKey = SHA512.Create().ComputeHash(jwtKey);

            var bytesEncrypted = Encrypt(bytesToBeEncrypted, jwtKey);

            return Convert.ToBase64String(bytesEncrypted);
        }

        public string Decrypt(string encryptedToken)
        {
            if (encryptedToken == null)
            {
                return Messages.UnauthorizedUser;
            }

            try
            {
                var bytesToBeDecrypted = Convert.FromBase64String(encryptedToken);
                var jwtKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                jwtKey = SHA512.Create().ComputeHash(jwtKey);
                var bytesDecrypted = Decrypt(bytesToBeDecrypted, jwtKey);
                return Encoding.UTF8.GetString(bytesDecrypted);
            }
            catch (Exception ex)
            {
                return Messages.UnauthorizedUser;
                throw new Exception(ex.Message, ex);
            }
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] jwtKeyBytes)
        {
            byte[]? encryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream stream = new MemoryStream())
            {
                using RijndaelManaged AES = new();
                var key = new Rfc2898DeriveBytes(jwtKeyBytes, saltBytes, 1000);

                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(stream, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }

                encryptedBytes = stream.ToArray();
            }
            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] jwtKeyBytes)
        {
            byte[]? decryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream stream = new MemoryStream())
            {
                using RijndaelManaged AES = new RijndaelManaged();
                var key = new Rfc2898DeriveBytes(jwtKeyBytes, saltBytes, 1000);

                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(stream, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }
                decryptedBytes = stream.ToArray();
            }
            return decryptedBytes;
        }

        public string GetTokenByHeader()
        {
            var rqt = _httpContextAccessor.HttpContext.Request;
            string token;
            if (rqt.Headers.TryGetValue("Authorization", out var authHeaders) &&
                authHeaders.ToString().StartsWith("Bearer "))
            {
                token = authHeaders.ToString()["Bearer ".Length..];
            }
            else
            {
                token = Messages.TokenNotSend;
            }
            return token;
        }

        public async Task<Response<string>> decryptTokenAsync(string token)
        {
            var response = new Response<string>();
            if (token != null)
            {
                string tokenDesencriptado = Decrypt(token);
                response.IsSuccess = true;
                response.Message = "Token Desencriptado";
                response.Data = tokenDesencriptado;
            }
            return response;
        }

        public async Task<Response<bool>> ValidationTokenAsync(string token)
        {
            var response = new Response<bool>();
            string tokenEncriptado = GetTokenByHeader();
            try
            {
                response.IsWarning = true;
                response.IsSuccess = false;

                if (tokenEncriptado == Messages.TokenNotSend)
                {
                    response.Message = Messages.TokenNotSend;
                    return response;
                }
                else
                {
                    var tokenDesencriptado = Decrypt(tokenEncriptado);//Desencripta el token
                    if (tokenDesencriptado == Messages.UnauthorizedUser)
                    {
                        response.Message = Messages.UnauthorizedUser;
                        return response;
                    }
                    else
                    {
                        var comprobarToken = await _apiUserDomain.GetApiUserByTokenAsync(tokenDesencriptado); //obtiene el apiuser mediante el token
                        if (comprobarToken != null && comprobarToken.Enable == true && comprobarToken.Active == true) //comprueba si existe
                        {
                            response.IsWarning = false;
                            response.IsSuccess = true;
                            response.Message = Messages.AuthorizedUser;
                        }
                        else
                        {
                            response.Message = Messages.UserNotActive;
                        }
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsWarning = true;
                response.IsSuccess = false;
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Response<string>> EncriptTokenAsync(string token)
        {
            var response = new Response<string>();
            try
            {
                var tokenEncriptado = Encrypt(token);//Encripta el token
                response.IsSuccess = true;
                response.Data = tokenEncriptado;
                response.Message = "Token Encriptado";
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

