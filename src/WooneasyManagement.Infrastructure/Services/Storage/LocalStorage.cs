using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WooneasyManagement.Application.Interfaces.Storage;

namespace WooneasyManagement.Infrastructure.Services.Storage
{
    public class LocalStorage(IWebHostEnvironment webHostEnvironment) : ILocalStorage
    {
        public async Task<List<(string fileName, string destination)>> UploadAsync(string destination, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, destination);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string destination)> values = new();
            foreach (IFormFile file in files)
            {
                string fileName = "new-file" + Path.GetExtension(file.FileName);
                await CopyFileAsync(Path.Combine(uploadPath, fileName), file);
                values.Add((fileName, uploadPath));

            }

            return values;
        }

        public async Task DeleteAsync(string destination, string fileName)
            => File.Delete($"{destination}\\{fileName}");

        public List<string> GetFiles(string destination)
        {
            DirectoryInfo directory = new (destination);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string destination, string fileName)
            => File.Exists($"{destination}\\{fileName}");

        public async Task<bool> CopyFileAsync(string destination, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(destination, FileMode.Create, FileAccess.Write, FileShare.None,
                    1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
