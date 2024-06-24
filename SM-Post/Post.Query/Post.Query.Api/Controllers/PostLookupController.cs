using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Common.DTOs;
using Post.Query.Api.DTOs;
using Post.Query.Api.Queries;
using Post.Query.Domain.Entities;

namespace Post.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostLookupController : ControllerBase
    {
        private readonly ILogger<PostLookupController> _logger;
        private readonly IQueryDispatcher<PostEntity> _queryDispatcher;

        public PostLookupController(IQueryDispatcher<PostEntity> queryDispatcher, ILogger<PostLookupController> logger)
        {
            _queryDispatcher = queryDispatcher;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPostAsync()
        {
            try
            {

                var posts = await _queryDispatcher.SendAsync(new FindAllPostQuery());

                if (posts == null || !posts.Any())
                {
                    return NoContent();
                }

                var count = posts.Count;

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned {count} post{(count > 1 ? "s" : string.Empty)}"
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all posts!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE,
                });
            }
        }

        [HttpGet("byId/{postId}")]
        public async Task<ActionResult> GetPostByIdAsyn(Guid postId)
        {
            try
            {

                var posts = await _queryDispatcher.SendAsync(new FindPostByIdQuery { Id = postId });

                if (posts == null || !posts.Any())
                {
                    return NoContent();
                }

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned post!"
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve post by id!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE,
                });
            }
        }
    }
}