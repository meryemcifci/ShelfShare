using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.BookDto
{
    public class UserBookDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfileImage { get; set; }
        public BookDto Book { get; set; }
        public ReadingStatus Status { get; set; }
        public int CurrentPage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public double ReadingProgress { get; set; }
    }
}
