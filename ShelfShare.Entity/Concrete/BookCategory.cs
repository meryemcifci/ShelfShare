using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
  //Kitap-kategori ilişkileri
{
    public class BookCategory : BaseEntity<int>
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }

        // Navigation Properties
        public virtual Book Book { get; set; }
        public virtual Category Category { get; set; }
    }
}
