namespace FormulaHQ.API.Models
{
    public class Race
    {
        public Guid RaceID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid CircuitID { get; set; }
        public Guid TypeID { get; set; }
        public bool isDeleted { get; set; }

        //Navigation Properties
        public Circuit Circuit { get; set; }
        public RaceType RaceType { get; set; }
    }
    public class RaceResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public Race? Race { get; set; }
    }
    public class RaceResponseListModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public List<Race>? Races { get; set; }
    }
    public class RaceParamModel
    {
        public Guid RaceID { get; set; }
    }
}
