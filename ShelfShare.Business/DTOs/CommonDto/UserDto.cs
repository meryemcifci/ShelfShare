using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.CommonDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Role{ get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime? Birthdate { get; set; }
        public List<string> GroupNames { get; set; }
        public int? TargetBooksCount { get; set; }   
        public int? CompletedBooksCount { get; set; }
    }
}
