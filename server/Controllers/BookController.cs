using Microsoft.AspNetCore.Mvc;
using server.DTO;
using server.Models.DB;
using server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using server.Policies;


namespace server.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>A list of all books.</returns>
        [Authorize(Policy = SecurityPolicy.Customer)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Retrieves a specific book by its ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to retrieve.</param>
        /// <returns>The book with the specified ID.</returns>
        [Authorize(Policy = SecurityPolicy.Customer)]
        [HttpGet("{bookId}")]
        public async Task<ActionResult<BookDTO>> GetBookById(int bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(book);
        }

        /// <summary>
        /// Searches for books based on a search string.
        /// </summary>
        /// <param name="searchString">The search string to look for in titles, tags, author names, publisher names, or language.</param>
        /// <returns>A list of books matching the search criteria.</returns>
        [Authorize(Policy = SecurityPolicy.Customer)]
        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooks(string searchString)
        {
            var books = await _bookService.SearchBooksAsync(searchString);
            return Ok(books);
        }

        /// <summary>
        /// Retrieves all categories (tags).
        /// </summary>
        /// <returns>A list of all categories (tags).</returns>
        [Authorize]
        [HttpGet("categories")]
        [SwaggerOperation(Summary = "Retrieves all categories", Description = "Gets a list of all book categories (tags) in the system.")]
        [SwaggerResponse(200, "Returns a list of categories", typeof(IEnumerable<Tag>))]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAllCategories()
        {
            var categories = await _bookService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Retrieves books by a specific category (tag).
        /// </summary>
        /// <param name="tagId">The ID of the category (tag).</param>
        /// <returns>A list of books in the specified category.</returns>
        [Authorize(Policy = SecurityPolicy.Customer)]
        [HttpGet("category/{categoryId}")]
        [SwaggerOperation(Summary = "Retrieves books by category", Description = "Gets a list of books that belong to a specific category (tag).")]
        [SwaggerResponse(200, "Returns a list of books in the specified category", typeof(IEnumerable<BookDTO>))]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByCategory(int tagId)
        {
            var books = await _bookService.GetBooksByCategoryAsync(tagId);
            return Ok(books);
        }

        /// <summary>
        /// Retrieves books similar to the specified book.
        /// </summary>
        /// <param name="bookId">The ID of the book for which to find similar books.</param>
        /// <returns>A list of books similar to the specified book.</returns>
        [Authorize(Policy = SecurityPolicy.Customer)]
        [HttpGet("getSimilarBooks/{bookId}")]
        [SwaggerOperation(Summary = "Retrieves similar books", Description = "Gets a list of books similar to the specified book based on category (tag).")]
        [SwaggerResponse(200, "Returns a list of similar books", typeof(IEnumerable<BookDTO>))]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetSimilarBooks(int bookId)
        {
            var books = await _bookService.GetSimilarBooksAsync(bookId);
            return Ok(books);
        }

        /// <summary>
        /// Adds a new book.
        /// </summary>
        /// <remarks>
        /// To add a new book, you can provide:
        /// 
        /// - An existing author by specifying `AuthorId`, or create a new author by providing `AuthorName` and `AuthorBio`.
        /// - An existing publisher by specifying `PublisherId`, or create a new publisher by providing `PublisherName` and `PublisherAddress`.
        /// - Tags by specifying `TagIds` for existing tags, or create new tags by providing `TagNames`.
        /// 
        /// Example JSON for adding a new book:
        /// 
        /// ```
        /// {
        ///   "title": "New Book Title",
        ///   "authorId": 1,
        ///   "authorName": "New Author Name",
        ///   "authorBio": "Bio of the new author",
        ///   "pagesCount": 350,
        ///   "language": "English",
        ///   "publisherId": 1,
        ///   "publisherName": "New Publisher",
        ///   "publisherAddress": "123 Publisher St.",
        ///   "publishedDate": "2024-01-01",
        ///   "publishedVersion": 1.0,
        ///   "price": 29.99,
        ///   "description": "A description of the book",
        ///   "imageUrl": "http://example.com/image.jpg",
        ///   "tagIds": [1, 2],
        ///   "tagNames": ["New Tag 1", "New Tag 2"]
        /// }
        /// ```
        /// 
        /// Only provide either `AuthorId` or `AuthorName`/`AuthorBio`. Similarly, provide either `PublisherId` or `PublisherName`/`PublisherAddress`.
        /// </remarks>
        /// <param name="bookDTO">The book details to add.</param>
        /// <returns>The ID of the newly created book.</returns>
        [HttpPost]
        [Authorize(Policy = SecurityPolicy.Admin)]
        // [SwaggerOperation(Summary = "Adds a new book", Description = "Creates a new book with the specified details. Handles cases where new authors, publishers, or tags need to be created. ")]
        // [SwaggerResponse(201, "Book created successfully", typeof(BookResponseDTO))]
        // [SwaggerResponse(400, "Invalid input")]
        public async Task<ActionResult<BookResponseDTO>> AddBook([FromBody] BookDTO bookDTO)
        {
            var response = await _bookService.AddBookAsync(bookDTO);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetBookById), new { bookId = response.BookId }, response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{bookId}")]
        [Authorize(Policy = SecurityPolicy.Admin)]
        [SwaggerOperation(Summary = "Deletes a book", Description = "Deletes a book by its ID.")]
        [SwaggerResponse(204, "Book deleted successfully")]
        [SwaggerResponse(404, "Book not found")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var success = await _bookService.DeleteBookAsync(bookId);
            if (success)
            {
                return NoContent();
            }
            return NotFound("Book not found.");
        }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        /// <returns>A list of authors.</returns>
        [HttpGet("authors")]
        [Authorize]
        [SwaggerOperation(Summary = "Retrieves all authors", Description = "Gets a list of all authors in the system.")]
        [SwaggerResponse(200, "Returns a list of authors", typeof(IEnumerable<AuthorDTO>))]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            var authors = await _bookService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        /// <summary>
        /// Retrieves all publishers.
        /// </summary>
        /// <returns>A list of publishers.</returns>
        [HttpGet("publishers")]
        [Authorize]
        [SwaggerOperation(Summary = "Retrieves all publishers", Description = "Gets a list of all publishers in the system.")]
        [SwaggerResponse(200, "Returns a list of publishers", typeof(IEnumerable<PublisherDTO>))]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetAllPublishers()
        {
            var publishers = await _bookService.GetAllPublishersAsync();
            return Ok(publishers);
        }
    }
}

