namespace DRRCore.Transversal.Common.Interface
{
    public interface IFileManager
    {       
        Task<bool> UploadFile(MemoryStream body);
        Task<string> UploadFile(MemoryStream body,string fileName);
        Task<MemoryStream> DownloadFile(string remoteFilePath);
        Task<bool> DeleteFile();
    }
}
