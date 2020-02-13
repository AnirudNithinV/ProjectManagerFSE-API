using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ProjectDBModel
    {
        public int? ProjectID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Priority { get; set; }

        public string ProjectTitle { get; set; }

        public int? UserID { get; set; }

        
    }
}