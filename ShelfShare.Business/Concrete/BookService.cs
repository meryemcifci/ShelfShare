using Microsoft.EntityFrameworkCore;
using ShelfShare.Business.DTOs.BookDto;
using ShelfShare.Business.Abstract;
using ShelfShare.Business.Utilities;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.Entity.Concrete;

namespace ShelfShare.Business.Concrete
{
    public class BookService : IBookService
    {
        private readonly Context _db;
        public BookService(Context db) { _db = db; }

        public async Task<Book> GetAsync(int id) =>
         await _db.Books
            .Include(b => b.Category)
            .Include(b => b.Reviews)
            .Include(b => b.Notifications)
            .Include(b => b.Readings)
            .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<PaginatedResult<Book>> SearchAsync(string q, int page)
        {
            var query = _db.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(b => b.Title.Contains(q));
            int pageSize = 20;
            var total = await query.CountAsync();
            var items = await query.OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<Book>(items, total, page, pageSize);
        }

        public async Task<int> CreateAsync(CreateBookDto dto)
        {
            var book = new Book { Title = dto.Title, Description = dto.Description, PublishDate = dto.PublishDate };
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateAsync(int id, UpdateBookDto dto)
        {
            var b = await _db.Books.FindAsync(id);
            b.Title = dto.Title; b.Description = dto.Description; b.PublishDate = dto.PublishDate;
            b.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var b = await _db.Books.FindAsync(id);
            _db.Remove(b);
            await _db.SaveChangesAsync();
        }


        public async Task<double> GetAverageRatingAsync(int bookId)
        {
            var ratings = await _db.Reviews
                .Where(r => r.BookId == bookId)   // Rating zaten int olduğu için null kontrolüne gerek yok
                .Select(r => r.Rating)
                .ToListAsync();

            return ratings.Count == 0 ? 0 : ratings.Average();
        }
    }
}
