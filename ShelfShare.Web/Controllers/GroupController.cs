using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.Entity.Concrete;

namespace ShelfShare.Web.Controllers
{

    public class GroupController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public GroupController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _context.Group.ToListAsync();
            return View(groups);
        }


        // Grup oluşturma
        [HttpPost]
        public async Task<IActionResult> Create(string groupName, string description)
        {
            var user = await _userManager.GetUserAsync(User);

            var group = new Group
            {
                Name = groupName,
                JoinCode = Guid.NewGuid().ToString().Substring(0, 6)
            };

            _context.Group.Add(group);
            await _context.SaveChangesAsync();

            _context.UserGroups.Add(new UserGroup
            {
                UserId = user.Id,
                GroupId = group.Id,
                IsAdmin = true
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = group.Id });
        }

        // Gruba katılma
        [HttpPost]
        public async Task<IActionResult> Join(string inviteCode)
        {
            var user = await _userManager.GetUserAsync(User);
            var group = await _context.Group.FirstOrDefaultAsync(g => g.JoinCode == inviteCode);
            if (group == null) return NotFound();

            if (!_context.UserGroups.Any(ug => ug.UserId == user.Id && ug.GroupId == group.Id))
            {
                _context.UserGroups.Add(new UserGroup
                {
                    UserId = user.Id,
                    GroupId = group.Id
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = group.Id });
        }

        // Grup detay sayfası
        public async Task<IActionResult> Details(int id)
        {
            var group = await _context.Group
                .Include(g => g.Books)
                .Include(g => g.UserGroups).ThenInclude(ug => ug.User)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null) return NotFound();

            return View(group);
        }
    }

}
