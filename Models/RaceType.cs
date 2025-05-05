namespace FormulaHQ.API.Models
{
    public class RaceType
    {
        public Guid TypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isDeleted { get; set; }
    }

    public class RaceTypeParamModel
    {
        public Guid TypeID { get; set; }
    }

    public class RacetypeListResponseModel
    {
        public int MessageID { get; set; }
        public string MessageDescription { get; set; }
        public List<RaceType>? RaceTypes { get; set; }
    }
    public class RaceTypeResponseModel
    {
        public int MessageID { get; set; }
        public string MessageDescription { get; set; }
        public RaceType? RaceType { get; set; }
    }
}
