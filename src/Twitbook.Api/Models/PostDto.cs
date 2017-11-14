using System;

namespace Twitbook.Api.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public DateTime PostedAt { get; set; }
        public string Content { get; set; }

    }
}
