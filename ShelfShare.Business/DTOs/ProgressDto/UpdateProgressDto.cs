using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.DTOs.ProgressDto
{
    public class UpdateProgressDto
    {
        public int CurrentPage { get; set; }
        public string Notes { get; set; }
    }
}
