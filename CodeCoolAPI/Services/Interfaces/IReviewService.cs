using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;

namespace CodeCoolAPI.Services
{
    public interface IReviewService
    {
        Task<ReviewReadDto> ReadReviewById(int id);
        Task<IEnumerable<ReviewReadDto>> ReadAllReviews();
        Task<ReviewReadDto> CreateReviewReadDto(ReviewUpsertDto reviewUpsertDto, int userId);
        Task UpdateReview(int id, ReviewUpsertDto reviewUpsertDto, int userId, string userRole);
        Task DeleteReview(int id);
    }
}