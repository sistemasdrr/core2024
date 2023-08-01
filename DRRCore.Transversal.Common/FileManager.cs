using CoreFtp;
using DRRCore.Transversal.Common.Interface;
using DRRCore.Transversal.Common.JsonReader;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;
using System.Text;

namespace DRRCore.Transversal.Common
{
    public class FileManager : IFileManager
    {
        private readonly SftpSettings _sftpSettings;
       private readonly string _fileName;

        public FileManager(IOptions<SftpSettings> options)
        {           
            _sftpSettings = options.Value;          
            _fileName =string.Format("{0}/{1}", _sftpSettings.RemotePath, _sftpSettings.FileName);
        }

        public async Task<bool> DeleteFile()
        {
            try
            {
                using (var ftpClient = new FtpClient(new FtpClientConfiguration
                {
                    Host = _sftpSettings.Host,
                    Port = _sftpSettings.Port,
                    Username = _sftpSettings.UserName,
                    Password = _sftpSettings.Password
                }))
                {
                    await ftpClient.LoginAsync();
                    await ftpClient.DeleteFileAsync(_fileName);
                    return true;
                }
            }
            catch
            {
               return false;
            }
        }

        public async Task<string> DownloadFile(string remoteFilePath)
        {            
            try
            {
                using (var ftpClient = new FtpClient(new FtpClientConfiguration
            {
                Host = _sftpSettings.Host,
                Port = _sftpSettings.Port,
                Username = _sftpSettings.UserName,
                Password = _sftpSettings.Password
            }))
            {              
                await ftpClient.LoginAsync();
                using (var ftpReadStream = await ftpClient.OpenFileReadStreamAsync(_fileName))
                {                    
                    using (var fileWriteStream = new MemoryStream())
                    {
                        await ftpReadStream.CopyToAsync(fileWriteStream);
                        if (fileWriteStream != null)
                        {
                            return Encoding.UTF8.GetString((fileWriteStream).ToArray());                            
                        }
                    }
                }
            }
               
            }
            finally
            {
              
            }           
           
            return string.Empty;
        }
       

        public async Task<bool> UploadFile(MemoryStream body)
        {
            try
            {
                using (var ftpClient = new FtpClient(new FtpClientConfiguration
                {
                    Host = _sftpSettings.Host,
                    Port = _sftpSettings.Port,
                    Username = _sftpSettings.UserName,
                    Password = _sftpSettings.Password
                }))
                {                   
                    await ftpClient.LoginAsync();

                    using (var writeStream = await ftpClient.OpenFileWriteStreamAsync(_fileName))
                    {                        
                        await body.CopyToAsync(writeStream);
                       
                    }
                    return true;
                }

            }
            catch 
            {
                return false;
            }
            
        }
    }
}
