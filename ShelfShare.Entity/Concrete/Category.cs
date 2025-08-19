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
        public string Description { get; set; } = string.Empty;
        public string ColorCode { get; set; }

        // Navigation Properties
        public virtual ICollection<Book> Books { get; set; }= new List<Book>();
    }
}
