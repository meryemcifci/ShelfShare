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
            AppUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login");
            }
            UserDto userDto = _mapper.Map<UserDto>(user);
            return View(userDto);
        }

        public IActionResult MyBooks()
        {
            return View();
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
