
using Microsoft.AspNetCore.Http;
using Movie.Core.Constants;
using Movie.Core.Interfaces;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Extensions
{
    public interface IFileEvent
    {
        Task<string> UploadStreamAsync(IFormFile file, string folder);
    }
    public class FileEvent : IFileEvent
    {
        private IFirebaseManager _manager;
        private string _rootPath;
        public FileEvent(IFirebaseManager manager, 
            string rootPath)
        {
            _manager = manager;
            _rootPath = rootPath;
        }
        public async Task<string> UploadStreamAsync(IFormFile file, string folder)
        {
            if(file != null)
            {
                string fileDownloadFromStorage, imagePath = Path.Combine(_rootPath, "images");
                string filePath = Path.Combine(imagePath, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    await _manager.Storage().Child(folder).Child(file.FileName).PutAsync(fs, new CancellationToken());
                    fileDownloadFromStorage = await _manager.Storage().Child(folder).Child(file.FileName).GetDownloadUrlAsync();
                    fs.Close();
                }
                File.Delete(filePath);
                return fileDownloadFromStorage;
            }
            else
            {
                return "";
            }
        }
    }
}
