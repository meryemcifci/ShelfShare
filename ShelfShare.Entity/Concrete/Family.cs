using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
    //Aile grupları
{
    public class Family : BaseEntity<int>
    {
        public string FamilyName { get; set; }
        public string FamilyCode { get; set; } // Aileye katılmak için kod
        public int OwnerId { get; set; }
        public string Description { get; set; }

        // Navigation Properties
        public virtual AppUser Owner { get; set; }
        public virtual ICollection<FamilyMember> Members { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }

}
