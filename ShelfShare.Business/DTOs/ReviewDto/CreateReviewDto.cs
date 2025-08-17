using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.ReviewDto
{
    public class CreateReviewDto
    {
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public bool IsPublic { get; set; } = true;
    }
}
