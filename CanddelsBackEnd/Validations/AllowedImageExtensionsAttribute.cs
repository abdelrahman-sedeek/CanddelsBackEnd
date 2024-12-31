using System.ComponentModel.DataAnnotations;

namespace CanddelsBackEnd.Validations
{
    public class AllowedImageExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedImageExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult(ErrorMessage ?? $"Invalid file type. Allowed types are: {string.Join(", ", _extensions)}");
                }
            }
            
            return ValidationResult.Success;
        }

    }
}
