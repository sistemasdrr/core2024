﻿namespace DRRCore.Transversal.Common.Interface
{
    public interface IFileManager
    {       
        Task<bool> UploadFile(MemoryStream body);
        Task<string> DownloadFile(string remoteFilePath);
        Task<bool> DeleteFile();
    }
}
