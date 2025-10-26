using ShelfShare.Business.DTOs.BookDto;

namespace ShelfShare.Web.ViewModels
{
    public class BookViewModel
    {
        public List<BookDto> Books { get; set; }
        public string SearchTerm { get; set; }
    }
}
