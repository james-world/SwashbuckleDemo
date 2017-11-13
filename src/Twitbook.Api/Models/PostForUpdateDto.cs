using System.ComponentModel.DataAnnotations;

namespace Twitbook.Api.Models
{
    public class PostForUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Content { get; set; }
    }
}
