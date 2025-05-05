namespace FormulaHQ.API.Models
{
    public class RaceResult
    {
        public Guid ResultID { get; set; }
        public Guid RaceID { get; set; }
        public Guid DriverID { get; set; }
        public double Points { get; set; }
        public int Position { get; set; }
        public bool isDeleted { get; set; }

        //Navigation properties
        public Race Race { get; set; }
        public Driver Driver { get; set; }

    }

    public class RaceResultResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public RaceResult? RaceResult { get; set; }
    }
    public class RaceResultListResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public List<RaceResult>? RaceResults { get; set; }
    }
    public class RaceResultParamModel
    {
        public Guid RaceID { get; set; }
    }
}

