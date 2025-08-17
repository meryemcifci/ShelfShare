using ShelfShare.Business.DTOs.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.FamilyDto
{
    public class FamilyDto
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string FamilyCode { get; set; }
        public string Description { get; set; }
        public int MemberCount { get; set; }
        public int BookCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto Owner { get; set; }
    }
}
