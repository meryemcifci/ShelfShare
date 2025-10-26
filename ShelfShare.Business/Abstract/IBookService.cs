using Microsoft.EntityFrameworkCore;
using ShelfShare.Business.DTOs.BookDto;
using ShelfShare.Business.Utilities;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.Abstract
{
    public interface IBookService// Controller bu sınıfla ilgilenir BookService iş mantığını içerir.
    {
        Task<Book> GetAsync(int id);
        Task<PaginatedResult<Book>> SearchAsync(string q, int page);
        Task<int> CreateAsync(CreateBookDto dto);
        Task UpdateAsync(int id, UpdateBookDto dto);
        Task DeleteAsync(int id);
        Task<double> GetAverageRatingAsync(int bookId);
        List<BookDto> GetAllBooks();
    }

}
