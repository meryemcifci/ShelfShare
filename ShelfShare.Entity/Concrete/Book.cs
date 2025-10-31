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
        public string Title { get; set; }//burası var
        public string Author { get; set; }//burası var
        public string? Description { get; set; }//burası var (!)
        public string? CoverImageUrl { get; set; }
        public int? PageCount { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? Publisher { get; set; }//burası var
        public string? Language { get; set; }//burası var
        public int? FamilyId { get; set; }
        public int? AddedByUserId { get; set; }
        public int? CategoryId { get; set; }

        // Navigation Properties

        public virtual AppUser AddedByUser { get; set; } = new AppUser();
        public virtual Category Category { get; set; }= new Category();
        public virtual ICollection<Review> Reviews { get; set; }= new List<Review>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<Reading> Readings { get; set; } = new List<Reading>();
    }
}
