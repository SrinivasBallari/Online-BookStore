using server.Models.DB;
using server.DTO;

namespace server.Repositories
{
    public interface IReviewRepo
    {
        Task<PostReviewResponseDTO> PostUserReviewAsync(string userEmail,int bookId,int rating,string review);
        Task<List<UserReview>> GetReviews(int bookId);
    }
}
