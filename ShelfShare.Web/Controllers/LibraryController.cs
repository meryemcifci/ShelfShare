using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelfShare.Business.Abstract;
using ShelfShare.Business.DTOs.BookDto;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.Entity.Concrete;
using ShelfShare.Web.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ShelfShare.Web.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public LibraryController(ILogger<HomeController> logger, Context context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var groupIds = await _context.UserGroups
                .Where(ug => ug.UserId == user.Id)
                .Select(ug => ug.GroupId)
                .ToListAsync();

            var books = await _context.Books
                .Where(b => groupIds.Contains(b.GroupId ?? 0))
                .ToListAsync();

            var bookDtos = await _context.Books
                .Include(b => b.Category)
                .Select(b => new ShelfShare.Business.DTOs.BookDto.BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    CoverImageUrl = b.CoverImageUrl,
                    Description = b.Description,
                    PageCount = b.PageCount,
                    Categories = b.Category != null && !string.IsNullOrWhiteSpace(b.Category.Name)
                        ? new List<string> { b.Category.Name }
                        : new List<string>()
                })
                .ToListAsync();
            var activeReadingBookIds = await _context.Readings
                .Where(r => r.IsActive)
                .Select(r => r.BookId)
                .Distinct()
                .ToListAsync();

            foreach (var dto in bookDtos)
            {
                if (activeReadingBookIds.Contains(dto.Id))
                {
                    dto.UserReadingStatus = ShelfShare.Entity.Concrete.ReadingStatus.Reading;
                }
            }

            return View(bookDtos);
        }
     

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Groups)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return NotFound();

            ViewBag.Groups = await _context.Group.ToListAsync();
            



            // Cover image boşsa varsayılan görsel ata
            var coverImageUrl = string.IsNullOrWhiteSpace(book.CoverImageUrl)
                ? Url.Content("~/images/book1.jpg")
                : book.CoverImageUrl;

            // Category bilgisi
            var categories = book.Category != null && !string.IsNullOrWhiteSpace(book.Category.Name)
                ? new List<string> { book.Category.Name }
                : new List<string>();

            // DTO oluşturma
            var dto = new ShelfShare.Business.DTOs.BookDto.BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                PageCount = book.PageCount,
                Categories = categories,
                CoverImageUrl = coverImageUrl
            };

            // Kitap aktif okuma durumunda mı?
            bool isReading = await _context.Readings
                .AnyAsync(r => r.BookId == id && r.IsActive);

            if (isReading)
                dto.UserReadingStatus = ShelfShare.Entity.Concrete.ReadingStatus.Reading;

            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> AddToGroups(int bookId, List<int> groupIds)
        {
            var book = await _context.Books
                .Include(b => b.Groups)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
                return NotFound();

            var groups = await _context.Group
                .Where(g => groupIds.Contains(g.Id))
                .ToListAsync();

            // Kitabın mevcut grup bağlantılarını güncelle
            book.Groups = groups;

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = bookId });
        }


    }
}

