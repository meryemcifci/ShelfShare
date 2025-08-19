using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
{
    public class Group:BaseEntity<int>
    {
        public string Name { get; set; }
        public GroupType GroupType { get; set; } // Family, Friendly, Custom
        public string JoinCode { get; set; } // Davet kodu


        public ICollection<GroupMember> Members { get; set; }
        
    }
}
