using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.BookDto
{
    public record BookDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public string CoverImageUrl { get; init; }
        public int PageCount { get; init; }
        public double AverageRating { get;  init; }
        public int ReviewCount { get; init; }
        public List<string> Categories { get; set; }
        public ReadingStatus? UserReadingStatus { get; set; } // Kullanıcının okuma durumu
    }
}