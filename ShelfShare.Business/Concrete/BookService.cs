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
        private readonly Context _context;//Dependency Injection ile Context nesnesi alınır.
        public BookService(Context db) { _context = db; }

        public async Task<Book> GetAsync(int id) =>//Id ile kitap getirir ve Include ile ilişkili verileri de yükler.
         await _context.Books
            .Include(b => b.Category)//Kitabın kategorisi
            .Include(b => b.Reviews)//Kitabın yorumları
            .Include(b => b.Notifications)//Kitabın bildirimleri
            .Include(b => b.Readings)//Kitabın okuma geçmişi
            .FirstOrDefaultAsync(b => b.Id == id);//eşleşen ilk kitabı getirir; yoksa null döner.

        public async Task<PaginatedResult<Book>> SearchAsync(string q, int page)
        {
            var query = _context.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(b => b.Title.Contains(q));
            int pageSize = 20;//her sayfada 20 kitap göstereceğim..
            var total = await query.CountAsync();
            var items = await query.OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<Book>(items, total, page, pageSize);
        }

        public async Task<int> CreateAsync(CreateBookDto dto)
        {
            var book = new Book { Title = dto.Title, Description = dto.Description, PublishDate = dto.PublishDate };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateAsync(int id, UpdateBookDto dto)
        {
            var b = await _context.Books.FindAsync(id);
            b.Title = dto.Title; b.Description = dto.Description; b.PublishDate = dto.PublishDate;
            b.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var b = await _context.Books.FindAsync(id);
            _context.Remove(b);
            await _context.SaveChangesAsync();
        }


        public async Task<double> GetAverageRatingAsync(int bookId)
        {
            var ratings = await _context.Reviews
                .Where(r => r.BookId == bookId)   // Rating zaten int olduğu için null kontrolüne gerek yok
                .Select(r => r.Rating)
                .ToListAsync();

            return ratings.Count == 0 ? 0 : ratings.Average();//hiç yorum yoksa 0 döner.
        }

        public List<BookDto> GetAllBooks()
        {
            return _context.Books
               .Include(b => b.Category)
               .Select(b => new BookDto
               {
                   Id = b.Id,
                   Title = b.Title,
                   Author = b.Author,
                   CoverImageUrl = b.CoverImageUrl,
                   PageCount = b.PageCount
               })
               .ToList();
        }
    }

}
