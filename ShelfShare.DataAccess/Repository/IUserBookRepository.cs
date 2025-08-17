using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.DataAccess.Repository
{
    public interface IUserBookRepository : IRepository<UserBook>
    {
        Task<IEnumerable<UserBook>> GetUserReadingListAsync(int userId);
        Task<UserBook> GetUserBookAsync(int userId, int bookId);
        Task<IEnumerable<UserBook>> GetCurrentlyReadingAsync(int familyId);
    }
}
