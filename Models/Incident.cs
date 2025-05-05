namespace FormulaHQ.API.Models
{
    public class Incident
    {
        public Guid IncidentID { get; set; }
        public Guid RaceID { get; set; }
        public Guid DriverID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }

        public bool isDeleted { get; set; }

        public Race Race { get; set; }
        public Driver Driver { get; set; }
    }
    public class IncidentParamModel
    {
        public Guid IncidentID { get; set; }
    }
    public class IncidentResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public Incident? Incident { get; set; }
    }
    public class IncidentListResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public List<Incident>? Incidents { get; set; }
    }

}
