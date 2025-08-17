using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
    //Kitap kategorileri
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ColorCode { get; set; }

        // Navigation Properties
        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}
