using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.CommonDto
{
    public class TopReaderDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public int BooksRead { get; set; }
    }
}
