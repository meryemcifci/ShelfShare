using Microsoft.EntityFrameworkCore;
using ShelfShare.Business.DTOs.BookDto;
using ShelfShare.Business.Abstract;
using ShelfShare.Business.Utilities;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.Entity.Concrete;
using System.Linq;

namespace ShelfShare.Business.Concrete
{
    
    
    public class BookService : IBookService
    {
        private readonly Context _context;//Dependency Injection ile Context nesnesi alınır.
        public BookService(Context db) { _context = db; }

    }

}
