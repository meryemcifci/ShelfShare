using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
{
    public class AppUser: IdentityUser<int>
    //Buraya guid yazmadım çünkü küçük çaplı bir proje ypıyorum. Guid yazarak veritabanımda çok yer kaplasın(guid 16 byte, int sadece 4 byte) istemiyorum. Eğer büyük çaplı bir proje olsaydı guid kullanırdım.

    {
        // BaseEntity özellikleri
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRole Role { get; set; }

        // Navigation Properties
        public virtual ICollection<UserBook> UserBooks { get; set; }
        public virtual ICollection<Book> AddedBooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<FamilyMember> FamilyMemberships { get; set; }
        public virtual Family OwnedFamily { get; set; }
    }
}

