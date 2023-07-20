using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet_Backend.Models
{
    public class TaskTable
    {
       
        public int Id { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
        public string task { get; set; }
        public int hours { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set;}
        public string Activityname { get; set; }
        public projectname projectnameid { get; set; }
        public string Projectname { get; set; }
        public activities activityid { get; set; }
    }
}
