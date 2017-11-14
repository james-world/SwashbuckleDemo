using System.ComponentModel.DataAnnotations;

namespace Twitbook.Api.Models
{
    public class PostForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Content { get; set; }
    }
}
