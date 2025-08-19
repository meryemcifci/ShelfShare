using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
    //Yıllık okuma hedefleri
{
    public class ReadingGoal : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int Year { get; set; }
        public int TargetBooksCount { get; set; }
        public int CompletedBooksCount { get; set; }

        // Navigation Properties
        public virtual AppUser User { get; set; }
    }
}
