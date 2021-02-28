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
            using var binaryReader = new BinaryReader(file.OpenReadStream());
            var bytes = binaryReader.ReadBytes((int)file.Length);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var path = $"wwwroot/images/uploaded/{fileName}";

            await File.WriteAllBytesAsync(path, bytes);
            return $"images/uploaded/{fileName}";
        }
    }
}
