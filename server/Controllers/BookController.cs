using Microsoft.AspNetCore.Mvc;
using server.DTO;
using server.Models.DB;
using server.Services;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using server.Policies;
using server.ActionFilters;


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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves books page wise and genre filter
        /// </summary>
        /// <returns>A list of books for a page with any specified genres</returns>
        [HttpGet("{page}/{pageSize}/{genres}/{authors}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetPageWiseBooks(int page,int pageSize,string genres,string authors)
        {
            try
            {
                List<string> genresList = genres=="none" ? new List<string>() : genres.Split(',').ToList();
                List<string> authorsList = authors=="none" ? new List<string>() : authors.Split(',').ToList();
                var books = await _bookService.GetPageWiseBooksAsync(page, pageSize,genresList,authorsList);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        /// <summary>
        /// Retrieves a specific book by its ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to retrieve.</param>
        /// <returns>The book with the specified ID.</returns>
        [HttpGet("{bookId}")]
        public async Task<ActionResult<BookDTO>> GetBookById(int bookId)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(bookId);
                if (book == null)
                {
                    return NotFound("Book not found.");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Searches for books based on a search string.
        /// </summary>
        /// <param name="searchString">The search string to look for in titles, tags, author names, publisher names, or language.</param>
        /// <returns>A list of books matching the search criteria.</returns>
        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooks(string searchString)
        {
            try
            {
                var books = await _bookService.SearchBooksAsync(searchString);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all categories (tags).
        /// </summary>
        /// <returns>A list of all categories (tags).</returns>
        [HttpGet("categories")]
        [SwaggerOperation(Summary = "Retrieves all categories", Description = "Gets a list of all book categories (tags) in the system.")]
        [SwaggerResponse(200, "Returns a list of categories", typeof(IEnumerable<Tag>))]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAllCategories()
        {
            try
            {
                var categories = await _bookService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves books by a specific category (tag).
        /// </summary>
        /// <param name="tagId">The ID of the category (tag).</param>
        /// <returns>A list of books in the specified category.</returns>
        [HttpGet("category/{categoryId}")]
        [SwaggerOperation(Summary = "Retrieves books by category", Description = "Gets a list of books that belong to a specific category (tag).")]
        [SwaggerResponse(200, "Returns a list of books in the specified category", typeof(IEnumerable<BookDTO>))]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByCategory(int tagId)
        {
            try
            {
                var books = await _bookService.GetBooksByCategoryAsync(tagId);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves books similar to the specified book.
        /// </summary>
        /// <param name="bookId">The ID of the book for which to find similar books.</param>
        /// <returns>A list of books similar to the specified book.</returns>
        [HttpGet("getSimilarBooks/{bookId}")]
        [SwaggerOperation(Summary = "Retrieves similar books", Description = "Gets a list of books similar to the specified book based on category (tag).")]
        [SwaggerResponse(200, "Returns a list of similar books", typeof(IEnumerable<BookDTO>))]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetSimilarBooks(int bookId)
        {
             try
            {
                var books = await _bookService.GetSimilarBooksAsync(bookId);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
             try
            {
                var response = await _bookService.AddBookAsync(bookDTO);
                if (response.Success)
                {
                    return CreatedAtAction(nameof(GetBookById), new { bookId = response.BookId }, response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
            try
            {
                var success = await _bookService.DeleteBookAsync(bookId);
                if (success)
                {
                    return NoContent();
                }
                return NotFound("Book not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        /// <returns>A list of authors.</returns>
        [HttpGet("authors")]
        [SwaggerOperation(Summary = "Retrieves all authors", Description = "Gets a list of all authors in the system.")]
        [SwaggerResponse(200, "Returns a list of authors", typeof(IEnumerable<AuthorDTO>))]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            try
            {
                var authors = await _bookService.GetAllAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all publishers.
        /// </summary>
        /// <returns>A list of publishers.</returns>
        [HttpGet("publishers")]
        [SwaggerOperation(Summary = "Retrieves all publishers", Description = "Gets a list of all publishers in the system.")]
        [SwaggerResponse(200, "Returns a list of publishers", typeof(IEnumerable<PublisherDTO>))]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetAllPublishers()
        {   
            try
             {
                var publishers = await _bookService.GetAllPublishersAsync();
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Posts a review for a book.
        /// </summary>
        /// <returns>Corresponding success or failure message</returns>
        [HttpPost("review")]
        [Authorize]
        [JwtEmailClaimExtractorFilter]
        public async Task<ActionResult<PostReviewResponseDTO>> PostUserReviewAsync([FromBody] PostReviewRequestDTO userReview){
            try{
                string userEmail = HttpContext.Items["userEmail"] as string;
                var response = await _bookService.PostUserReviewAsync(userEmail,userReview.bookId,userReview.rating,userReview.review);
                return Ok(new PostReviewResponseDTO{
                    statusMessage = "Review Posted Successfully"
                });
            }
            catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets all reviews for a book.
        /// </summary>
        /// <returns>a list of reviews for a book</returns>
        [HttpGet("reviews/{bookId}")]
        public async Task<ActionResult<List<UserReview>>> GetReviews(int bookId){
            try{
                var reviews = await _bookService.GetReviews(bookId);
                return Ok(reviews);
            }
            catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}

