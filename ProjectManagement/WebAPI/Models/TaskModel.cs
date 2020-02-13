using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public bool IsParentTask { get; set; }
        public int? Priority { get; set; }
        public int? ParentTaskID { get; set; }
        public string ParentTaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
    }
}