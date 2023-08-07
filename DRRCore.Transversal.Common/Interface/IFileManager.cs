namespace DRRCore.Transversal.Common.Interface
{
    public interface IFileManager
    {       
        Task<bool> UploadFile(MemoryStream body);
        Task<string> UploadFile(MemoryStream body,string fileName);
        Task DownloadFile(string remoteFilePath, MemoryStream stream);
        Task<bool> DeleteFile(string fileName);
    }
}
