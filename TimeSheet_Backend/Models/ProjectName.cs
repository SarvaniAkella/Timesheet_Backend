namespace TimeSheet_Backend.Models
{
    public class ProjectName
    {
        public int Id { get; set; } 
        public projectname projectname { get; set; }
    }

    public enum projectname
    {
        PersonaNutrition=1,
        Puritains,
        NestleHealthSciences,
        MarketCentral,
        FamilyCentral,
        InternalPOC,
        ExternalPOC,
        MarketingandSales

    }
}
