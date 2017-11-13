using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Twitbook.Api.Models
{
    public class PostForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Content { get; set; }
    }
}
