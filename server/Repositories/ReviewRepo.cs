using Microsoft.EntityFrameworkCore;
using server.DTO;
using server.Models.DB;

namespace server.Repositories
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly BookStoreDbContext _context;

        public ReviewRepo(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<PostReviewResponseDTO> PostUserReviewAsync(string userEmail, int bookId, int rating, string review)
        {
            try
            {
                User user = _context.Users.SingleOrDefault(u => u.Email == userEmail);
                _context.Reviews.Add(new Review
                {
                    BookId = bookId,
                    UserId = user.UserId,
                    Rating = rating,
                    Review1 = review,
                    ReviewedDate = DateOnly.FromDateTime(DateTime.Now)
                });
                await _context.SaveChangesAsync();
                return new PostReviewResponseDTO
                {
                    statusMessage = "Review posted successfully"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception occured while posting user review", ex);
                return new PostReviewResponseDTO
                {
                    statusMessage = "could not post review"
                };
            }

        }

        public async Task<List<UserReview>> GetReviews(int bookId)
        {
            try
            {
                var bookReviews = await _context.Reviews
                    .Where(r => r.BookId == bookId)
                    .Select(r => new UserReview
                    {
                        userName = r.User.Name,
                        rating = (int)r.Rating,
                        review = r.Review1,
                    })
                    .ToListAsync();
                return bookReviews;
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception occured while getting user reviews", ex);
                return [];
            }

        }
    }
}