using AutoMapper;
using ShelfShare.Business.DTOs.BookDto;
using ShelfShare.Business.Interfaces;
using ShelfShare.DataAccess.Abstract;
using ShelfShare.Entity.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelfShare.Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserBookRepository _userBookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IUserBookRepository userBookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _userBookRepository = userBookRepository;
            _mapper = mapper;
        }

        public async Task<BookDto> AddBookToFamilyAsync(CreateBookDto createBookDto, int userId)
        {
            // Aynı ISBN ailede var mı kontrolü
            var exists = await _bookRepository.IsBookExistsInFamilyAsync(createBookDto.ISBN, createBookDto.FamilyId);
            if (exists)
            {
                throw new System.InvalidOperationException("Bu ISBN ailede zaten mevcut.");
            }

            var book = _mapper.Map<Book>(createBookDto);
            book.AddedByUserId = userId;

            if (createBookDto.CategoryIds != null && createBookDto.CategoryIds.Any())
            {
                book.BookCategories = createBookDto.CategoryIds
                    .Distinct()
                    .Select(categoryId => new BookCategory
                    {
                        CategoryId = categoryId
                    }).ToList();
            }

            var savedBook = await _bookRepository.AddAsync(book);
            var dto = _mapper.Map<BookDto>(savedBook);
            return dto;
        }

        public async Task ChangeReadingStatusAsync(int userId, int bookId, ReadingStatus status)
        {
            var userBook = await _userBookRepository.FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);
            if (userBook == null)
            {
                userBook = new UserBook
                {
                    UserId = userId,
                    BookId = bookId,
                    Status = status,
                    StartDate = status == ReadingStatus.Reading ? System.DateTime.UtcNow : null,
                    CompletedDate = status == ReadingStatus.Completed ? System.DateTime.UtcNow : null,
                    CurrentPage = 0
                };
                await _userBookRepository.AddAsync(userBook);
                return;
            }

            userBook.Status = status;
            if (status == ReadingStatus.Reading && userBook.StartDate == null)
            {
                userBook.StartDate = System.DateTime.UtcNow;
            }
            if (status == ReadingStatus.Completed)
            {
                userBook.CompletedDate = System.DateTime.UtcNow;
            }
            await _userBookRepository.UpdateAsync(userBook);
        }

        public async Task<BookDto> GetBookDetailsAsync(int bookId, int userId)
        {
            var book = await _bookRepository.GetBookWithDetailsAsync(bookId);
            if (book == null)
            {
                return null;
            }

            var dto = _mapper.Map<BookDto>(book);
            var userBook = book.UserBooks?.FirstOrDefault(ub => ub.UserId == userId && !ub.IsDeleted);
            dto.UserReadingStatus = userBook?.Status;
            return dto;
        }

        public async Task<IEnumerable<UserBookDto>> GetCurrentlyReadingAsync(int familyId)
        {
            var userBooks = await _userBookRepository.GetCurrentlyReadingAsync(familyId);
            return userBooks.Select(ub => new UserBookDto
            {
                UserId = ub.UserId,
                UserName = ub.AppUser?.UserName,
                UserProfileImage = ub.AppUser?.ProfileImageUrl,
                Book = _mapper.Map<BookDto>(ub.Book),
                Status = ub.Status,
                CurrentPage = ub.CurrentPage,
                StartDate = ub.StartDate,
                CompletedDate = ub.CompletedDate,
                ReadingProgress = ub.Book?.PageCount > 0 ? (double)ub.CurrentPage / ub.Book.PageCount * 100 : 0
            }).ToList();
        }

        public async Task<IEnumerable<BookDto>> GetFamilyBooksAsync(int familyId, int userId)
        {
            var books = await _bookRepository.GetFamilyBooksAsync(familyId);
            var result = books.Select(book => _mapper.Map<BookDto>(book)).ToList();

            // Kullanıcının okuma durumunu set et
            var userReadingList = await _userBookRepository.GetUserReadingListAsync(userId);
            var bookIdToStatus = userReadingList.ToDictionary(ub => ub.BookId, ub => ub.Status);
            foreach (var dto in result)
            {
                if (bookIdToStatus.TryGetValue(dto.Id, out var status))
                {
                    dto.UserReadingStatus = status;
                }
            }

            return result;
        }

        public async Task UpdateReadingProgressAsync(int userId, int bookId, int currentPage)
        {
            var userBook = await _userBookRepository.FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);
            if (userBook == null)
            {
                userBook = new UserBook
                {
                    UserId = userId,
                    BookId = bookId,
                    Status = ReadingStatus.Reading,
                    StartDate = System.DateTime.UtcNow,
                    CurrentPage = currentPage
                };
                await _userBookRepository.AddAsync(userBook);
                return;
            }

            userBook.CurrentPage = currentPage;
            await _userBookRepository.UpdateAsync(userBook);
        }
    }
}
