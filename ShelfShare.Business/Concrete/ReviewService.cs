using ShelfShare.Business.Abstract;
using ShelfShare.Business.DTOs.ReviewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.Concrete
{
    public class ReviewService : IReviewService
    {
        public Task<ReviewDto> AddReviewAsync(CreateReviewDto createReviewDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReviewAsync(int reviewId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetBookAverageRatingAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewDto>> GetBookReviewsAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReviewAsync(UpdateReviewDto updateReviewDto)
        {
            throw new NotImplementedException();
        }
    }
}
