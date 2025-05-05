namespace FormulaHQ.API.Models
{
    public class Driver
    {
        public Guid DriverID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string InternationalCode { get; set; }
        public Guid TeamID { get; set; }
        public bool isDeleted { get; set; }

        // Navigation properties
        public Team Team { get; set; }
    }

    public class DriverResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public Driver? Driver { get; set; }
    }
    public class DriverListResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public List<Driver>? Drivers { get; set; }
    }
    public class DriverParamModel
    {
        public Guid DriverID { get; set; }
    }
}
