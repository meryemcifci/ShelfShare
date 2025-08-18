using Microsoft.EntityFrameworkCore;
using ShelfShare.DataAccess.Abstract;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShelfShare.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            return await _context.Set<AppUser>()
                .Where(u => !u.IsDeleted && u.Email.ToLower() == email.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            return await _context.Set<AppUser>()
                .Where(u => !u.IsDeleted && u.UserName.ToLower() == username.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetFamilyMembersAsync(int familyId)
        {
            return await _context.Set<AppUser>()
                .Include(u => u.FamilyMemberships)
                .Where(u => !u.IsDeleted && u.FamilyMemberships.Any(fm => fm.FamilyId == familyId))
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();
        }

        public async Task<AppUser> GetUserWithFamiliesAsync(int userId)
        {
            return await _context.Set<AppUser>()
                .Include(u => u.FamilyMemberships)
                .Include(u => u.OwnedFamily)
                .Where(u => !u.IsDeleted && u.Id == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetUserWithReadingDataAsync(int userId)
        {
            return await _context.Set<AppUser>()
                .Include(u => u.UserBooks)
                .ThenInclude(ub => ub.Book)
                .Include(u => u.Reviews)
                .ThenInclude(r => r.Book)
                .Include(u => u.AddedBooks)
                .Where(u => !u.IsDeleted && u.Id == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsEmailExistsAsync(string email, int excludeUserId = 0)
        {
            return await _context.Set<AppUser>()
                .Where(u => !u.IsDeleted &&
                           u.Email.ToLower() == email.ToLower() &&
                           u.Id != excludeUserId)
                .AnyAsync();
        }

        public async Task<bool> IsUsernameExistsAsync(string username, int excludeUserId = 0)
        {
            return await _context.Set<AppUser>()
                .Where(u => !u.IsDeleted &&
                           u.UserName.ToLower() == username.ToLower() &&
                           u.Id != excludeUserId)
                .AnyAsync();
        }

        public async Task<Dictionary<string, object>> GetUserStatisticsAsync(int userId)
        {
            var user = await _context.Set<AppUser>()
                .Where(u => !u.IsDeleted && u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return new Dictionary<string, object>();

            var statistics = new Dictionary<string, object>();

            // Temel kullanıcı bilgileri
            statistics.Add("MemberSince", user.CreatedAt.ToString("MMMM yyyy"));
            statistics.Add("FullName", $"{user.FirstName} {user.LastName}");

            var userWithData = await _context.Set<AppUser>()
                .Include(u => u.UserBooks)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var totalBooks = userWithData?.UserBooks?.Count(ub => !ub.IsDeleted) ?? 0;
            var totalReviews = userWithData?.Reviews?.Count(r => !r.IsDeleted) ?? 0;
            statistics.Add("TotalBooks", totalBooks);
            statistics.Add("TotalReviews", totalReviews);

            return statistics;
        }

        public async Task<IEnumerable<AppUser>> GetTopReadersAsync(int familyId, int count = 5)
        {
            return await _context.Set<AppUser>()
                .Select(u => new
                {
                    User = u,
                    CompletedCount = u.UserBooks
                        .Where(ub => !ub.IsDeleted && ub.Status == ReadingStatus.Completed && ub.Book.FamilyId == familyId)
                        .Count()
                })
                .OrderByDescending(x => x.CompletedCount)
                .ThenBy(x => x.User.FirstName)
                .ThenBy(x => x.User.LastName)
                .Take(count)
                .Select(x => x.User)
                .ToListAsync();
        }

        // IRepository<AppUser> metodları
        public async Task<AppUser> GetByIdAsync(int id)
        {
            return await _context.Set<AppUser>()
                .Where(u => !u.IsDeleted && u.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _context.Set<AppUser>()
                .Where(u => !u.IsDeleted)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();
        }

        public async Task<AppUser> AddAsync(AppUser entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Set<AppUser>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(AppUser entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<AppUser>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AppUser>> FindAsync(Expression<Func<AppUser, bool>> predicate)
        {
            return await _context.Set<AppUser>().Where(predicate).ToListAsync();
        }

        public async Task<AppUser> FirstOrDefaultAsync(Expression<Func<AppUser, bool>> predicate)
        {
            return await _context.Set<AppUser>().FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<AppUser, bool>> predicate)
        {
            return await _context.Set<AppUser>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<AppUser, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Set<AppUser>().CountAsync();
            }
            return await _context.Set<AppUser>().CountAsync(predicate);
        }

        public async Task AddRangeAsync(IEnumerable<AppUser> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }
            await _context.Set<AppUser>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<AppUser> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
            _context.Set<AppUser>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public IQueryable<AppUser> GetQueryable()
        {
            return _context.Set<AppUser>().AsQueryable();
        }
    }
}