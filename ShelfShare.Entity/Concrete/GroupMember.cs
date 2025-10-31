using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
    // Aile üyelikleri (many-to-many)
{
    public class GroupMember : BaseEntity<int>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime? JoinedDate { get; set; }
        public FamilyMemberRole Role { get; set; }

        // Navigation Properties
        public virtual Group Group { get; set; }
        public virtual AppUser User { get; set; }
    }
}
