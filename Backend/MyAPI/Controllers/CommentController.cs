using Core.Entities;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.IService;
using System.Security.Claims;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CommentController> _logger;
        public CommentController(ICommentService commentService, UserManager<User> userManager, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("{bookId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByBook(Guid bookId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByBookAsync(bookId);

                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching comments for book with ID {BookId}", bookId);
                return StatusCode(500, "Internal server error");
            }
          
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] AddCommentModel model)
        {
    
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("Unauthorized access attempt.");
                    return Unauthorized();
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning($"User not found: {userId}");
                    return Unauthorized();
                }
                var comment = new Comment
                {
                    Description = model.Comment,
                    PostedDate = DateTime.UtcNow,
                    UserID = user.Id,
                    BookID = model.BookID
                };

                _logger.LogInformation($"Adding comment by user {user.UserName} for book {comment.BookID}");
                await _commentService.AddCommentAsync(comment);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding comment.");
                return StatusCode(500, "Internal server error");
            }
        }
      
    }
}
