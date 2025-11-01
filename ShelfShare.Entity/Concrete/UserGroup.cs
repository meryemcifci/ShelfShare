using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public bool IsAdmin { get; set; } // Grubu oluşturan kişi mi?
    }
}
