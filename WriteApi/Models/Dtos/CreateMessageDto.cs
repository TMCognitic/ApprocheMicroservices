using System.ComponentModel.DataAnnotations;

namespace WriteApi.Models.Dtos
{
    public class CreateMessageDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public required string Nom { get; set; }
        [Required]
        public required string Message { get; set; }
    }
}
