using DRRCore.Application.Interfaces;
using DRRCore.Domain.Interfaces;
using DRRCore.Transversal.Common;
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

        public TokenValidationApplication(IApiUserDomain apiUserDomain, IConfiguration configuration)
        {
            _apiUserDomain = apiUserDomain;
            _configuration = configuration;
        }

        public async Task<Response<bool>> ValidationTokenAsync(string encriptedToken)
        {
            var response = new Response<bool>();
            try
            {
                var tokenDesencriptado = Decrypt(encriptedToken);//Desencripta el token

                var comprobarToken = await _apiUserDomain.GetApiUserByTokenAsync(tokenDesencriptado); //obtiene el apiuser mediante el token
                if(comprobarToken != null) //comprueba si existe
                {
                    response.IsWarning = false;
                    response.IsSuccess = true;
                    response.Message = "Token Valido";
                }
                return response;
            }catch (Exception ex)
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
                var tokenEncriptado = Encrypt(token);//Desencripta el token
                response.IsSuccess = true;
                response.Data = tokenEncriptado;
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
                return null;
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
                return null;
            }
        }

        private byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] jwtKeyBytes)
        {
            byte[] encryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream stream = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
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
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] jwtKeyBytes)
        {
            byte[] decryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream stream = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
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
            }
            return decryptedBytes;
        }

    }
}

