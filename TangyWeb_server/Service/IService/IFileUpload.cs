using Microsoft.AspNetCore.Components.Forms;

namespace TangyWeb_server.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile browserFile);
        bool DeleteFile(string filePath);
    }
}
