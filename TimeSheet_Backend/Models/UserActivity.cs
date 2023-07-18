using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet_Backend.Models
{
    public class UserActivity
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Activity { get; set; }
        public string Task { get; set; }

        public int Hours { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOnly { get; set; }

    }
}
