using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet_Backend.Models
{
    public class TimeSheet1
    {
        public string task { get; set; }
        public float hours { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }
        public int ActivityId { get; set; }
    }
}
