using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.DataAccess.Repository
{
    public interface IUserRepository:IRepository<AppUser>
    {
        Task<AppUser> GetByEmailAsync(string email);
        Task<AppUser> GetByUsernameAsync(string username);
        Task<IEnumerable<AppUser>> GetFamilyMembersAsync(int familyId);
    }
}
