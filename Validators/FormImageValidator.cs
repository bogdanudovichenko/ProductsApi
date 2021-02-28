using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ProductsApi.Validators
{
    public class FormImageValidator : ValidationAttribute
    {
        private readonly static string[] _extensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                return ValidationResult.Success;
            }

            var extension = Path.GetExtension(file.FileName);
            if (_extensions.Contains(extension))
            {
                return ValidationResult.Success;
            }

            var error = $"Invalid extension {extension}, please use 1 of: {string.Join(',', _extensions)}";
            return new ValidationResult(error);
        }
    }
}
