using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IService;
using Core.Entities;
using Core.Data;
using Services.Service;
using MyAPI.Filters.ActionFilter;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Core.Static;

namespace MyAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBook()
        {
            var products=await _bookService.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById([FromQuery] Guid id)
        {
            var product = await _bookService.GetBookByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Book>>> SearchFilter(string input)
        {
            var comparer = await _bookService.GetAllAsync(c=>c.Author);

            if (!string.IsNullOrEmpty(input))
            {
                
                var result = comparer.Where(c => c.Author.Name.Contains(input, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Contains(input, StringComparison.OrdinalIgnoreCase)).ToList();
                if (result!=null)
                {

                return Ok();
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [ServiceFilter(typeof(Book_CreateFilterAttribute))]
        [Authorize(Roles=Roles.Admin)]
        public async Task<IActionResult> Create(NewBook book)
        {
            await _bookService.AddNewBook(book);
            return CreatedAtAction(nameof(GetById), new { id = book.ID }, book);
        }
       [HttpPut("{id}")]
        [Authorize(Roles =Roles.Admin)]
       public async Task<IActionResult> Update(Guid id,NewBook book)
       {
            if (id!=book.ID)
            {
                return NotFound();
            }
           await _bookService.UpdateNewBook(book);
           return NoContent();
       }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
