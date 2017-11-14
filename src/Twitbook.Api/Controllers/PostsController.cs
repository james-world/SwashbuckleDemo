using System;
using Microsoft.AspNetCore.Mvc;
using Twitbook.Api.Models;

namespace Twitbook.Api.Controllers
{
    [Route("api/v1/posts")]
    public class PostsController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(PostDto[]),200)]
        public IActionResult Get()
        {
            var posts = new[]
            {
                new PostDto
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Content = "Twit!",
                    PostedAt = new DateTime(2017, 11, 01, 11, 10, 0)
                },
                new PostDto
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Content = "Twoo!",
                    PostedAt = new DateTime(2017, 11, 01, 11, 13, 0)
                }
            };
            return Ok(posts);
        }
        
        [HttpGet("{id}", Name = "GetPost")]
        [ProducesResponseType(typeof(PostDto), 200)]
        public IActionResult Get(Guid id)
        {
            return Ok(new PostDto
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Content = "Twoo!",
                PostedAt = new DateTime(2017, 11, 01, 11, 13, 0)
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostDto), 201)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public IActionResult Post([FromBody]PostForCreationDto newPost)
        {
            if (newPost.Content.Contains("bugger"))
            {
                return BadRequest(new ErrorDto {Reason = "Unacceptable language"});
            }

            var post = new PostDto
            {
                Id = Guid.NewGuid(),
                Content = newPost.Content,
                PostedAt = DateTime.UtcNow
            };

            return CreatedAtRoute("GetPost", new {id = post.Id}, post);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        public IActionResult Put(Guid id, [FromBody]PostForUpdateDto editedPost)
        {
            return NoContent();
        }
    }
}
