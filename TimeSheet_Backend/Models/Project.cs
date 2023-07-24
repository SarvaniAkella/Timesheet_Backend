namespace TimeSheet_Backend.Models
{
    public class Project
    {
        public int ProjectId { get; set; } 
        public string  ProjectName { get; set; }

        public List<TimeSheet> TimeSheets { get; set; }
    }

   /* public enum projectname
    {
        PersonaNutrition=1,
        Puritains,
        NestleHealthSciences,
        MarketCentral,
        FamilyCentral,
        InternalPOC,
        ExternalPOC,
        MarketingandSales

    }*/
}
