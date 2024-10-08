using System;
using System.Collections.Generic;
namespace server.DTO
{
 
    public class BookDTO
    {
        public int? BookId { get; set; }          // Nullable Book ID
        public string Title { get; set; }
        public int? AuthorId { get; set; }        // Nullable Author ID
        public string? AuthorName { get; set; }   // Author Name
        public string? AuthorBio { get; set; }    // Author Bio
        public int? PagesCount { get; set; }
        public string? Language { get; set; }
        public int? PublisherId { get; set; }        // Nullable Publisher ID
        public string? PublisherName { get; set; }      // Publisher Name
        public string? PublisherAddress { get; set; } // Publisher Address
        public DateOnly? PublishedDate { get; set; }
        public double? PublishedVersion { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<int?>? TagIds { get; set; }    // List of nullable Tag IDs
        public List<string>? TagNames { get; set; }// List of Tag Names
    }

    public class PagedBooksResponse<T>
    {
        public List<T> Books { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }

    public class BooksAddedResponseDTO{
    
        public required string message { get; set; }
        public required string status { get; set; }
    }

    public class BookResponseDTO
    {
        public bool Success { get; set; }          // Indicates whether the operation was successful
        public string Message { get; set; }        // Provides additional details or error messages
        public int? BookId { get; set; }           // The ID of the book that was added/updated (optional)
    }

    public class AuthorDTO
    {
        public int? AuthorId { get; set; }  // Nullable Author ID
        public string Name { get; set; }    // Author Name
        public string Bio { get; set; }     // Author Bio
    }

    public class PublisherDTO
    {
        public int? PublisherId { get; set; }  // Nullable Publisher ID
        public string Name { get; set; }       // Publisher Name
        public string Address { get; set; }    // Publisher Address
    }

    public class BookUpdateDTO
    {
        public int BookId { get; set; }          
        public string Title { get; set; }
        public int? AuthorId { get; set; }        // Nullable Author ID
        public string? AuthorName { get; set; }   // Author Name
        public string? AuthorBio { get; set; }    // Author Bio
        public int? PagesCount { get; set; }
        public string? Language { get; set; }
        public int? PublisherId { get; set; }        // Nullable Publisher ID
        public string? PublisherName { get; set; }      // Publisher Name
        public string? PublisherAddress { get; set; } // Publisher Address
        public DateOnly? PublishedDate { get; set; }
        public double? PublishedVersion { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<int?>? TagIds { get; set; }    // List of nullable Tag IDs
        public List<string>? TagNames { get; set; }// List of Tag Names
    }

    public class PaginatedBooksDTO
    {
        public string ImageUrl {get; set;}
        public int? BookId { get; set; }
        public string Title { get; set; }
        public string? AuthorName { get; set; }
        public string? Language { get; set; }
        public decimal? Price { get; set; }
        public List<string>? TagNames { get; set; }
    }

    public class PostReviewRequestDTO{
        public int bookId {get; set;}
        public int rating {get; set;}
        public string review {get; set;}
    }

    public class PostReviewResponseDTO{
        public string statusMessage {get; set;}
    }

    public class UserReview{
        public string userName {get; set;}
        public int rating {get; set;}
        public string review {get; set;}
    }
}