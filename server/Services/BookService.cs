using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.DTO;
using server.Models.DB;
using server.Repositories;

namespace server.Services{
    

    public class BookService : IBookService
{
    private readonly IBookRepo _bookRepo;
    private readonly IAuthorRepo _authorRepo;
    private readonly IPublisherRepo _publisherRepo;
    private readonly ITagRepo _tagRepo;

    public BookService(IBookRepo bookRepo, IAuthorRepo authorRepo, IPublisherRepo publisherRepo, ITagRepo tagRepo)
    {
        _bookRepo = bookRepo;
        _authorRepo = authorRepo;
        _publisherRepo = publisherRepo;
        _tagRepo = tagRepo;
    }

    public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
    {
        var books = await _bookRepo.GetAllBooksAsync();
        return books.Select(book => ConvertToBookDTO(book)).ToList();
    }


    public async Task<BookDTO> GetBookByIdAsync(int bookId)
    {
        var book = await _bookRepo.GetBookByIdAsync(bookId);
        return book != null ? ConvertToBookDTO(book) : null;
    }


    public async Task<IEnumerable<BookDTO>> SearchBooksAsync(string searchString)
    {
        var books = await _bookRepo.SearchBooksAsync(searchString);
        return books.Select(book => ConvertToBookDTO(book)).ToList();
    }


    public async Task<IEnumerable<Tag>> GetAllCategoriesAsync()
    {
        return await _tagRepo.GetAllTagsAsync();
    }

    public async Task<IEnumerable<BookDTO>> GetBooksByCategoryAsync(int tagId)
    {
        var books = await _tagRepo.GetBooksByTagIdAsync(tagId);
        return books.Select(book => ConvertToBookDTO(book)).ToList();
    }

   public async Task<IEnumerable<BookDTO>> GetSimilarBooksAsync(int bookId)
    {
        var book = await _bookRepo.GetBookByIdAsync(bookId);
        if (book == null) return Enumerable.Empty<BookDTO>();

        var similarBooks = await _tagRepo.GetBooksByTagIdAsync(book.Tags.Select(t => t.TagId).FirstOrDefault());
        return similarBooks.Select(b => ConvertToBookDTO(b)).ToList();
    }


    public async Task<BookResponseDTO> AddBookAsync(BookDTO bookDTO)
{
   
    var existingBook = await _bookRepo.GetBookByAttributesAsync(
        bookDTO.Title,
        bookDTO.AuthorId,
        bookDTO.PublisherId,
        bookDTO.PublishedDate
    );

    if (existingBook != null)
    {
        throw new Exception("A book with the same title, author, publisher, and published date already exists.");
    }

   
    var book = await BuildBookEntityAsync(bookDTO);
    await _bookRepo.AddBookAsync(book);
    return new BookResponseDTO { Success = true, BookId = book.BookId };
}

    public async Task<BookResponseDTO> UpdateBookAsync(int bookId, BookDTO bookDTO)
    {
        var existingBook = await _bookRepo.GetBookByIdAsync(bookId);
        if (existingBook == null)
        {
            return new BookResponseDTO { Success = false, Message = "Book not found" };
        }

        var updatedBook = await BuildBookEntityAsync(bookDTO, existingBook);

        await _bookRepo.AddBookAsync(updatedBook);
        return new BookResponseDTO { Success = true, BookId = updatedBook.BookId };
    }

    public async Task<bool> DeleteBookAsync(int bookId)
    {
        return await _bookRepo.DeleteBookAsync(bookId);
    }

    // Private helper method to build the Book entity from DTO
    private async Task<Book> BuildBookEntityAsync(BookDTO bookDTO, Book existingBook = null)
    {
        var book = existingBook ?? new Book();

        book.Title = bookDTO.Title;
        book.PagesCount = bookDTO.PagesCount;
        book.Language = bookDTO.Language;
        book.PublishedDate = bookDTO.PublishedDate;
        book.PublishedVersion = bookDTO.PublishedVersion;
        book.Price = bookDTO.Price;
        book.Description = bookDTO.Description;
        book.ImageUrl = bookDTO.ImageUrl;

       
        if (bookDTO.AuthorId.HasValue)
        {
            book.AuthorId = bookDTO.AuthorId.Value;
        }
        else if (!string.IsNullOrEmpty(bookDTO.AuthorName) && !string.IsNullOrEmpty(bookDTO.AuthorBio))
        {
            var author = new Author { AuthorName = bookDTO.AuthorName, Bio = bookDTO.AuthorBio };
            await _authorRepo.AddAsync(author);
            book.AuthorId = author.AuthorId;
        }

        
        if (bookDTO.PublisherId.HasValue)
        {
            book.PublisherId = bookDTO.PublisherId.Value;
        }
        else if (!string.IsNullOrEmpty(bookDTO.PublisherName) && !string.IsNullOrEmpty(bookDTO.PublisherAddress))
        {
            var publisher = new Publisher { PublisherName = bookDTO.PublisherName, PublisherAddress = bookDTO.PublisherAddress };
            await _publisherRepo.AddAsync(publisher);
            book.PublisherId = publisher.PublisherId;
        }

       
        book.Tags = new List<Tag>();
        if (bookDTO.TagIds != null && bookDTO.TagIds.Any())
        {
            foreach (var tagId in bookDTO.TagIds)
            {
                int nonNullableTagId = tagId.Value;
                var tag = await _tagRepo.GetByIdAsync(nonNullableTagId);
                if (tag != null)
                {
                    book.Tags.Add(tag);
                }
            }
        }
        if (bookDTO.TagNames != null && bookDTO.TagNames.Any())
        {
            foreach (var tagName in bookDTO.TagNames)
            {
                var existingTag = await _tagRepo.GetByNameAsync(tagName);

                Tag tag;
                if (existingTag != null)
                {
                    tag = existingTag;
                }
                else
                {
                    tag = new Tag { Tag1 = tagName };
                    await _tagRepo.AddAsync(tag);
                }

                book.Tags.Add(tag);
            }
        }

        return book;
    }

    public async Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepo.GetAllAuthorsAsync();
        return authors.Select(author => new AuthorDTO
        {
            AuthorId = author.AuthorId,
            Name = author.AuthorName,
            Bio = author.Bio
        }).ToList();
    }

    public async Task<IEnumerable<PublisherDTO>> GetAllPublishersAsync()
    {
        var publishers = await _publisherRepo.GetAllPublishersAsync();
        return publishers.Select(publisher => new PublisherDTO
        {
            PublisherId = publisher.PublisherId,
            Name = publisher.PublisherName,
            Address = publisher.PublisherAddress
        }).ToList();
    }
    private static BookDTO ConvertToBookDTO(Book book)
{
    return new BookDTO
    {
        BookId = book.BookId,
        Title = book.Title,
        AuthorId = book.AuthorId,
        AuthorName = book.Author?.AuthorName,
        AuthorBio = book.Author?.Bio,
        PagesCount = book.PagesCount,
        Language = book.Language,
        PublisherId = book.PublisherId,
        PublisherName = book.Publisher?.PublisherName,
        PublisherAddress = book.Publisher?.PublisherAddress,
        PublishedDate = book.PublishedDate,
        PublishedVersion = book.PublishedVersion,
        Price = book.Price,
        Description = book.Description,
        ImageUrl = book.ImageUrl,
        TagIds = book.Tags?.Select(t => (int?)t.TagId).ToList(),
        TagNames = book.Tags?.Select(t => t.Tag1).ToList()
    };
}
}




}