using ShelfShare.Business.DTOs.ReviewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.Abstract
{
    public interface IReviewService
    {
        Task<ReviewDto> AddReviewAsync(CreateReviewDto createReviewDto);
        Task<IEnumerable<ReviewDto>> GetBookReviewsAsync(int bookId);
        Task<double> GetBookAverageRatingAsync(int bookId);
        Task UpdateReviewAsync(UpdateReviewDto updateReviewDto);
        Task DeleteReviewAsync(int reviewId, int userId);
    }

}
