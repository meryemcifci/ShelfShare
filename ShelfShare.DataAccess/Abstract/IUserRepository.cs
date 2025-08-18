using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.DataAccess.Abstract
{
    public interface IUserRepository:IRepository<AppUser>
    {
        Task<AppUser> GetByEmailAsync(string email);
        Task<AppUser> GetByUsernameAsync(string username);
        Task<IEnumerable<AppUser>> GetFamilyMembersAsync(int familyId);
        Task<AppUser> GetUserWithFamiliesAsync(int userId);
        Task<AppUser> GetUserWithReadingDataAsync(int userId);
        Task<bool> IsEmailExistsAsync(string email, int excludeUserId = 0);
        Task<bool> IsUsernameExistsAsync(string username, int excludeUserId = 0);
        Task<Dictionary<string, object>> GetUserStatisticsAsync(int userId);
        Task<IEnumerable<AppUser>> GetTopReadersAsync(int familyId, int count = 5);
        Task DeleteAsync(int id);
    }
}
