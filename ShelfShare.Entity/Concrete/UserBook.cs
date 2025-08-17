using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
//Kullanıcı-kitap ilişkileri ve okuma durumu
{
    public class UserBook : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public ReadingStatus Status { get; set; } // WantToRead, Reading, Completed
        public DateTime? StartDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int CurrentPage { get; set; }
        public string Notes { get; set; }

        // Navigation Properties
        public virtual AppUser AppUser { get; set; }
        public virtual Book Book { get; set; }
    }
}
