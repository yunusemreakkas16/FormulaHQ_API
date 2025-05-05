namespace FormulaHQ.API.Models
{
    public class FIARegulation
    {
        public Guid RegulationID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string AffectedTable { get; set; }
    }
    public class FIARegulationResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public FIARegulation? Regulation { get; set; }
    }
    public class FIARegulationListResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public List<FIARegulation>? Regulations { get; set; }
    }
    public class FIARegulationParamModel
    {
        public Guid RegulationID { get; set; }
    }
}
