using ShelfShare.Business.DTOs.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.FamilyDto
{
    public class FamilyStatisticsDto
    {
        public int TotalBooks { get; set; }
        public int TotalMembers { get; set; }
        public int BooksReadThisMonth { get; set; }
        public int BooksCurrentlyReading { get; set; }
        public double AverageRating { get; set; }
        public List<TopReaderDto> TopReaders { get; set; } = new List<TopReaderDto>();
        public List<PopularBookDto> PopularBooks { get; set; } = new List<PopularBookDto>();
    }
}
