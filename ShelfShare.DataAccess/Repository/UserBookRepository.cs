using Microsoft.EntityFrameworkCore;
using ShelfShare.DataAccess.Abstract;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.Entity.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelfShare.DataAccess.Repository
{
    public class UserBookRepository : Repository<UserBook>, IUserBookRepository
    {
        private readonly Context _context;

        public UserBookRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserBook>> GetUserReadingListAsync(int userId)
        {
            return await _context.UserBooks
                .Include(ub => ub.Book)
                .Where(ub => ub.UserId == userId && !ub.IsDeleted)
                .ToListAsync();
        }

        public async Task<UserBook> GetUserBookAsync(int userId, int bookId)
        {
            return await _context.UserBooks
                .Include(ub => ub.Book)
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId && !ub.IsDeleted);
        }

        public async Task<IEnumerable<UserBook>> GetCurrentlyReadingAsync(int familyId)
        {
            return await _context.UserBooks
                .Include(ub => ub.AppUser)
                .Include(ub => ub.Book)
                .Where(ub => ub.Status == ReadingStatus.Reading && ub.Book.FamilyId == familyId && !ub.IsDeleted)
                .ToListAsync();
        }
    }
}
