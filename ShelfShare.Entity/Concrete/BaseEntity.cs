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
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
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
    public enum BookStatus
    {
        Available = 1,
        Borrowed = 2,
        Reserved = 3,
        Lost = 4
    }
    public enum GroupType
    {
        Family = 1,
        Friends = 2,
        Custom = 3
    }
    public enum NotificationType
    {
        NewBookAdded = 1,
        BookBorrowed = 2,
        BookReturned = 3,
        ReviewPosted = 4,
        UserJoined = 5,
        UserLeft = 6
    }
    public enum NotificationStatus
    {
        Unread = 1,
        Read = 2,
        Archived = 3
    }
    //NotificationType → olayın türü(kitap eklendi, iade edildi, inceleme yapıldı, kullanıcı katıldı vs.).
    //NotificationStatus → alıcının bildirimi hangi durumda(okunmadı, okundu, arşivlendi).
}
