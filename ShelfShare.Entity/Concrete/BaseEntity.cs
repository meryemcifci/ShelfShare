using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
    
    public enum UserRole
    {
        Member = 1,
        FamilyAdmin = 2,
        SystemAdmin = 3
    }
    public enum FamilyMemberRole
    {
        Member = 1,
        Admin = 2,
        Owner = 3
    }
    public enum ReadingStatus
    {
        WantToRead = 1,
        Reading = 2,
        Completed = 3,
        Paused = 4,
        Abandoned = 5
    }
}
