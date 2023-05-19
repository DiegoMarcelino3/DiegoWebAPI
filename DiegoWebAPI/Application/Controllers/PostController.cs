using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DiegoWebAPI.Infraestructure.Data.Contexts;
using DiegoWebAPI.Domain.Models;
using DiegoWebAPI.Infraestructure.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using DiegoWebAPI.Domain.Services;

namespace DiegoWebAPI.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet("list-posts")]
        public async Task<ActionResult> ListPosts()
        {
            try
            {
                List<Post> list = await _postService.ListPosts();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("list-meus-posts")]
        public async Task<ActionResult> ListPostsMeusPosts()
        {
            try
            {
                List<Post> list = await _postService.ListMeusPosts();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-post")]
        public async Task<ActionResult> GetPost([FromQuery] int postId)
        {
            try
            {
                Post post = await _postService.GetPost(postId);

                return Ok(post);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpPost("novo-post")]
        public async Task<ActionResult> NovoPost([FromBody] Post post)
        {
            try
            {
                post = await _postService.NovoPost(post);
                
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("update-post")]
        public async Task<ActionResult> UpdatePost([FromBody] Post post)
        {
           try
            {
                return Ok(await _postService.UpdatePost(post));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete-post")]
        public async Task<ActionResult> DeletePostAsync([FromBody] int id)
        {
            try
            {
                return Ok(await _postService.DeletePostAsync(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
