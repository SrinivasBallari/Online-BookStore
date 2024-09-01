using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Policies;
using server.Services;
using server.ActionFilters;
using server.Models.DB;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookService> _logger;

        public BookController(IBookService bookService, ILogger<BookService> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("books")]
        [TokenValidationFilter]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error Occured at GetAllBooksAsync in Controller Layer");
                return BadRequest(new { Message = ex.Message });
            }

        }
    }
}
