using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.DataAccess.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetFamilyBooksAsync(int familyId);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm, int familyId);
        Task<IEnumerable<Book>> GetBooksByStatusAsync(int familyId, ReadingStatus status);
    }
}
