namespace FormulaHQ.API.Models
{
    public class Circuit
    {
        public Guid CircuitID { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public double Length { get; set; }
        public double MaxSpeed { get; set; }
        public bool isDeleted { get; set; }
    }

    public class CircuitResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public Circuit? Circuit { get; set; }
    }
    public class CircuitListResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public List<Circuit>? Circuits { get; set; }
    }

    public class CircuitParamModel
    {
        public Guid CircuitID { get; set; }
    }
}

