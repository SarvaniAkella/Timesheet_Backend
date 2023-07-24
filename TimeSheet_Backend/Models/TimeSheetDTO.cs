using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet_Backend.Models
{
    public class TimeSheetDTO
    {
        public int TimeSheetId { get; set; }

        public string task { get; set; }
        public int hours { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }
        public int ActivityId { get; set; }
    }
}
