using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelfShare.Business.DTOs.CommonDto;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.Entity.Concrete;
using System.Security.Claims;

namespace ShelfShare.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserController(ILogger<UserController> logger, Context context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        //<li><a class="dropdown-item" href="/User/Profile">Profilim</a></li>
        //<li><a class="dropdown-item" href="/User/MyBooks">Kitaplarım</a></li>
        //<li><a class="dropdown-item" href="/User/c">Yorumlarım</a></li>
        //<li><a class="dropdown-item" href="/User/ReadingList">Okuma Listem</a></li>

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.Users
                .Include(u => u.ReadingGoals) 
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user == null)
                return RedirectToAction("Login");

            // Grup bilgileri
            var userGroups = await _context.UserGroups
                .Include(ug => ug.Group)
                .Where(ug => ug.UserId == user.Id)
                .Select(ug => ug.Group.Name)
                .ToListAsync();

            // Kullanıcının ilk veya aktif okuma hedefi
            var readingGoal = user.ReadingGoals?.FirstOrDefault();

            // DTO oluşturdum.
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Birthdate=user.BirthDate,
                ProfileImageUrl=user.ProfileImageUrl,
                TargetBooksCount = readingGoal?.TargetBooksCount ?? 0,
                CompletedBooksCount = readingGoal?.CompletedBooksCount ?? 0,
                GroupNames = userGroups
            };

            return View(userDto);
        }


        public async Task<IActionResult> MyBooks()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            // string'i int'e çeviriyoruz
            if (!int.TryParse(userIdString, out int userId))
                return BadRequest("Kullanıcı ID geçersiz.");

            var reviews = await _context.Readings
                .Include(r => r.Book)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return View(reviews);
        }

        public async Task<IActionResult> MyReviews()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            // string'i int'e çeviriyoruz
            if (!int.TryParse(userIdString, out int userId))
                return BadRequest("Kullanıcı ID geçersiz.");

            var reviews = await _context.Reviews
                .Include(r => r.Book)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return View(reviews);
        }

        public IActionResult ReadingList()
        {
            return View();
        }
    }
}
