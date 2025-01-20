using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class ImagesUploadRequestDto
    {
        [Required]

        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
