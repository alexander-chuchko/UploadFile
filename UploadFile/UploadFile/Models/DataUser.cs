using System.ComponentModel.DataAnnotations;

namespace UploadFile.Models
{
    public class DataUser
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public IFormFile? Picture { get; set; }
    }
}
