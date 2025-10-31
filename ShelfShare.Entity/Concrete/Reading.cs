using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
{
    public class Reading : BaseEntity<int>
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } // Şu an okunuyor mu?


        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
