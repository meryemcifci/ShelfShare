using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
//Kitap bilgileri
{
    public class Book: BaseEntity<int>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int FamilyId { get; set; }
        public int AddedByUserId { get; set; }

        // Navigation Properties
        public virtual Family Family { get; set; }
        public virtual AppUser AddedByUser { get; set; }
        public virtual ICollection<UserBook> UserBooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}
