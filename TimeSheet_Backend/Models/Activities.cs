namespace TimeSheet_Backend.Models
{
    public class Activities
    {
        public int id { get; set; }
        public activities  activity { get; set; }
    }

    public enum activities
    {
        UnitTesting=1,
        AcceptanceTesting,
        WarrantyMC,
        SystemTesting,
        CodingImplementation,
        Design,
        Support,
        IntegrationTesting,
        RequirementsDevelopment,
        Planning,
        PTO

    }
}
