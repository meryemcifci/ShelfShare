using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.ReviewDto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserProfileImage { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
