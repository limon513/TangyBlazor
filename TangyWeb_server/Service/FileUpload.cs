using Microsoft.AspNetCore.Components.Forms;
using TangyWeb_server.Service.IService;

namespace TangyWeb_server.Service
{
    public class FileUpload : IFileUpload
    {
        public readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool DeleteFile(string filePath)
        {
            if (File.Exists(_webHostEnvironment.WebRootPath + filePath))
            {
                File.Delete(_webHostEnvironment.WebRootPath + filePath);
                return true;
            }
            return false;
        }

        public async Task<string> UploadFile(IBrowserFile browserFile)
        {
            FileInfo fileInfo = new(browserFile.Name);
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\productimage";
            if(!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }
            var filePath = Path.Combine(folderDirectory,fileName);
            await using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await browserFile.OpenReadStream().CopyToAsync(fileStream);
            var fullpath = $"/productimage/{fileName}";
            return  fullpath;
        }
    }
}
