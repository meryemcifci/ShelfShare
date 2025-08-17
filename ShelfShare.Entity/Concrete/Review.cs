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
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; } // 1-5 yıldız
        public string Comment { get; set; }
        public bool IsPublic { get; set; }
        public DateTime ReviewDate { get; set; }

        // Navigation Properties
        public virtual AppUser AppUser { get; set; }
        public virtual Book Book { get; set; }
    }
}
