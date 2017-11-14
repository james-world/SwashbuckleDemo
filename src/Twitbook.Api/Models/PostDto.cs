using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitbook.Api.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public DateTime PostedAt { get; set; }
        public string Content { get; set; }

    }
}
