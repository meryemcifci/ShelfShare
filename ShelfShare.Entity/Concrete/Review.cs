using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
    // Kitap değerlendirmeleri
{
    public class Review : BaseEntity<int>
    {
        public int UserId { get; set; }        // AppUser FK
        public int BookId { get; set; }           // Book FK
        public int Rating { get; set; }           // 1-5 yıldız
        public string? Comment { get; set; }       // yorum metni
        public bool IsPublic { get; set; }
        public DateTime? ReviewDate { get; set; }

        public int? ReadingId { get; set; }       // Reading FK
        public int? SuggestedToUserId { get; set; } // önerilen kullanıcı FK
        // Navigation Properties

        public virtual AppUser SuggestedToUser { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Book Book { get; set; }
        public virtual Reading Reading { get; set; } // Eğer okuma üzerinden yorum yapıldıysa

    }
}
