using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.BookDto
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int FamilyId { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
