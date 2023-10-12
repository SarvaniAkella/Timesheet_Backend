using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TimeSheet_Backend.Models
{
    public class Activity
    {     
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public int ProjectId { get; set; }
        public List<TimeSheet> TimeSheets { get; set; }
        public List<Project> Projects { get; set; }
    }
}
