using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.FamilyDto
{
    public class FamilyMemberDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public FamilyMemberRole Role { get; set; }
        public DateTime JoinedDate { get; set; }
        public int BooksRead { get; set; }
        public int CurrentlyReading { get; set; }
    }
}
