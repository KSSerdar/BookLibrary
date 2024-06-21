using Core.Entities;
using Core.Static;
using DAL.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Filters.ActionFilter;
using Services.IService;

namespace MyAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            var products = await _authorService.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById([FromQuery]Guid id)
        {
            var product = await _authorService.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetAuthorNameById([FromQuery]Guid id)
        {
            var author= await _authorService.GetAuthorNameByID(id);
            if (author==null)
            {
                return NotFound();
            }
            return Ok(author);
        }
        [HttpPost]
        [Authorize(Roles =Roles.Admin)]
        [ServiceFilter(typeof(Author_CreateFilterAttribute))]
        public async Task<IActionResult> Create(Author author)
        {
            await _authorService.AddAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = author.ID }, author);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(Guid id, Author author)
        {
            if (id != author.ID)
            {
                return BadRequest();
            }

            await _authorService.UpdateAsync(id, author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
