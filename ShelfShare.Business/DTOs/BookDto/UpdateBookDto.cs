using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.BookDto
{
    public record UpdateBookDto
    {
        public string Title { get; init; }
        public string Author { get; init; }
        public string ISBN { get; init; }
        public string Description { get; init; }
        public string CoverImageUrl { get; init; }
        public int PageCount { get; init; }
        public DateTime PublishDate { get; init; }
        public string Publisher { get; init; }
        public string Language { get; init; }
        public int FamilyId { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();

    }
}
