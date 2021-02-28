using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductsApi.Services
{
    public class FileService
    {
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var path = $"wwwroot/assets/uploaded/{fileName}";

            using var stream = File.Create(path);
            await file.CopyToAsync(stream);

            return $"assets/uploaded/{fileName}";
        }
    }
}
