using Microsoft.EntityFrameworkCore;
using ShelfShare.DataAccess.Abstract;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.Entity.Concrete;

namespace ShelfShare.DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly Context _context;

        public BookRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetFamilyBooksAsync(int familyId)
        {
            return await _context.Books
                .Include(b => b.AddedByUser)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .Include(b => b.Reviews)
                .Where(b => b.FamilyId == familyId && !b.IsDeleted)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm, int familyId)
        {
            var query = _context.Books
                .Include(b => b.AddedByUser)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .Where(b => b.FamilyId == familyId && !b.IsDeleted);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(searchTerm) ||
                    b.Author.ToLower().Contains(searchTerm) ||
                    b.ISBN.Contains(searchTerm) ||
                    b.Publisher.ToLower().Contains(searchTerm)
                );
            }

            return await query
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByStatusAsync(int familyId, ReadingStatus status)
        {
            return await _context.Books
                .Include(b => b.UserBooks.Where(ub => ub.Status == status))
                    .ThenInclude(ub => ub.AppUser)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .Where(b => b.FamilyId == familyId &&
                           !b.IsDeleted &&
                           b.UserBooks.Any(ub => ub.Status == status))
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Book> GetBookWithDetailsAsync(int bookId)
        {
            return await _context.Books
                .Include(b => b.AddedByUser)
                .Include(b => b.Family)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.AppUser)
                .Include(b => b.UserBooks)
                    .ThenInclude(ub => ub.AppUser)
                .FirstOrDefaultAsync(b => b.Id == bookId && !b.IsDeleted);
        }

        public async Task<bool> IsBookExistsInFamilyAsync(string isbn, int familyId)
        {
            return await _context.Books
                .AnyAsync(b => b.ISBN == isbn &&
                              b.FamilyId == familyId &&
                              !b.IsDeleted);
        }

        public async Task<IEnumerable<Book>> GetPopularBooksAsync(int familyId, int count = 10)
        {
            return await _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.UserBooks)
                .Where(b => b.FamilyId == familyId && !b.IsDeleted)
                .OrderByDescending(b => b.Reviews.Count)
                .ThenByDescending(b => b.Reviews.Any() ? b.Reviews.Average(r => r.Rating) : 0)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetRecentlyAddedBooksAsync(int familyId, int count = 10)
        {
            return await _context.Books
                .Include(b => b.AddedByUser)
                .Where(b => b.FamilyId == familyId && !b.IsDeleted)
                .OrderByDescending(b => b.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetBookStatisticsAsync(int familyId)
        {
            var books = await _context.Books
                .Include(b => b.UserBooks)
                .Where(b => b.FamilyId == familyId && !b.IsDeleted)
                .ToListAsync();

            return new Dictionary<string, int>
            {
                { "TotalBooks", books.Count },
                { "CurrentlyReading", books.Count(b => b.UserBooks.Any(ub => ub.Status == ReadingStatus.Reading)) },
                { "CompletedBooks", books.Count(b => b.UserBooks.Any(ub => ub.Status == ReadingStatus.Completed)) },
                { "WantToRead", books.Count(b => b.UserBooks.Any(ub => ub.Status == ReadingStatus.WantToRead)) }
            };
        }
    }
}
