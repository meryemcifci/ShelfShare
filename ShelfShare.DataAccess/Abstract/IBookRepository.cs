using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.DataAccess.Abstract
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetFamilyBooksAsync(int familyId);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm, int familyId);
        Task<IEnumerable<Book>> GetBooksByStatusAsync(int familyId, ReadingStatus status);
        Task<Book> GetBookWithDetailsAsync(int bookId);
        Task<bool> IsBookExistsInFamilyAsync(string isbn, int familyId);
        Task<IEnumerable<Book>> GetPopularBooksAsync(int familyId, int count = 10);
        Task<IEnumerable<Book>> GetRecentlyAddedBooksAsync(int familyId, int count = 10);
        Task<Dictionary<string, int>> GetBookStatisticsAsync(int familyId);
    }
}
 